using System.Collections;
using System.Collections.Generic;
using TiltFiveDemos;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Base class for the wand pointer.
    /// Contains all properties that the pointer needs and the graphic raycasting function to determine the wand touch.
    /// In the children classes, the canvas is found through raycasting or another physics check for a collider present in the canvas.
    /// </summary>
    public class WandPointer : BaseDemoInput
    {
        /// <summary>
        /// The camera from the glasses.
        /// </summary>
        [SerializeField]
        protected Camera _glassesCamera;

        /// <summary>
        /// The origin of the pointer (the wand tip)
        /// </summary>
        [SerializeField]
        protected Transform _pointerOrigin;

        /// <summary>
        /// The line renderer to draw the line.
        /// </summary>
        [SerializeField]
        protected LineRenderer _line;

        /// <summary>
        /// A LayerMask containing the definition of the UI's layers (typically layer "UI")
        /// </summary>
        [SerializeField]
        protected LayerMask _uILayers;

        /// <summary>
        /// The representation of the pointer.
        /// </summary>
        [SerializeField]
        protected GameObject _pointer;

        /// <summary>
        /// The current event system in the scene.
        /// </summary>
        protected EventSystem _eventSystem;

        /// <summary>
        /// Flag to check if we already performed the click.
        /// </summary>
        protected bool _doingClick = false;

        /// <summary>
        /// The current selectable element.
        /// </summary>
        protected Selectable _currentSelectable = null;

        protected virtual void Start()
        {
            _eventSystem = EventSystem.current; // Find the current event system.
        }

        protected virtual void Update()
        {
            if (Input.GetButtonDown("CursorClick")) DoClick();
        }

        /// <summary>
        /// Start performing the raycast.
        /// </summary>
        protected virtual void StartRaycast() { }

        /// <summary>
        /// Perform the raycast using the direction provided towards a physics collider added to the canvas.
        /// Then call the graphic raycaster if a canvas was found.
        /// </summary>
        /// <param name="pDirection">The direction to perform the raycast.</param>
        protected virtual void DoRaycast(Vector3 pDirection)
        {
            if (!_line.enabled) return;

            Ray ray = new Ray(_pointerOrigin.position, pDirection);

            RaycastHit hit;

            Vector3 hitPoint = _pointerOrigin.position + (pDirection * 12f); // Default length of the line.

            bool foundCanvas = false;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _uILayers))
            {
                hitPoint = hit.point;

                GraphicRaycaster raycaster = hit.collider.GetComponent<GraphicRaycaster>();

                if (raycaster != null)
                {
                    foundCanvas = true;
                    if (_pointer != null)
                    {
                        _pointer.SetActive(true);
                        _pointer.transform.position = hit.point;
                        _pointer.transform.rotation = raycaster.transform.rotation;
                    }

                    CheckCanvas(raycaster, hitPoint);
                }
            }

            // If no elements were found, deselect the current found selectable.
            if (!foundCanvas)
            {

                if (_pointer != null)  _pointer.SetActive(false);
                DeselectCurrentSelectable();
            }

            // Draw the line from the tip to the hit point.
            DrawLine(_pointerOrigin.position, hitPoint);
        }

        /// <summary>
        /// Draw the line from the origin to the hit point
        /// </summary>
        /// <param name="pOrigin">The origin (usually the wand tip.)</param>
        /// <param name="pHit">The hit point.</param>
        protected virtual void DrawLine(Vector3 pWoldOriginPoint, Vector3 pWorldHitPoint)
        { 
            // Get the local positions in the line's transform to draw the positions.
            Vector3 localOriginPoint = _line.transform.InverseTransformPoint(pWoldOriginPoint);
            Vector3 localHitPoint = _line.transform.InverseTransformPoint(pWorldHitPoint);

            _line.positionCount = 2;

            _line.SetPosition(0, localOriginPoint);
            _line.SetPosition(1, localHitPoint);
        }

        private Coroutine _waitForDeselectCoroutine;

        /// <summary>
        /// Check the canvas.
        /// </summary>
        /// <param name="pGraphicsRaycaster">The graphics raycaster of the canvas found through physics checking.</param>
        /// <param name="pPosition">The position of the collision.</param>
        protected virtual void CheckCanvas(GraphicRaycaster pGraphicsRaycaster, Vector3 pPosition)
        {
            //Set up the new Pointer Event
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);

            //Set the Pointer Event Position to that of the virtual cursor position
            pointerEventData.position = _glassesCamera.WorldToScreenPoint(pPosition);

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pGraphicsRaycaster.Raycast(pointerEventData, results);

            bool foundSelectable = false;

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    Selectable selectable = results[i].gameObject.GetComponent<Selectable>();

                    if (selectable != null)
                    {
                        foundSelectable = true;

                        // Deselect the current selectable if the current selectable is not the same as the one touched this frame.
                        if (selectable != _currentSelectable)
                        {
                            selectable.Select();
                            _currentSelectable = selectable;
                        }

                        // Wait for input click or outside input.
                        if(selectable is Button && _doingClick)
                        {
                            Debug.Log("Clicking button: " + selectable.gameObject, selectable.gameObject);
                            
                            Button button = selectable as Button;

                            _doingClick = false;

                            // Invoke the button event
                            button.onClick.Invoke();
                        }
                    }
                }
            }
            
            // If no selectable was found, deselect the current selectable as well.
            if (!foundSelectable)
            {
                DeselectCurrentSelectable();
            }

        }

        protected void DeselectCurrentSelectable()
        {
            StartCoroutine(DeselectCurrentSelectableCoroutine());
        }
        
        /// <summary>
        /// Deselect the current selectable.
        /// </summary>
        protected IEnumerator DeselectCurrentSelectableCoroutine()
        {
            if (EventSystem.current != null)
            {
                yield return new WaitForEndOfFrame();
                if (_currentSelectable != null) _currentSelectable.OnDeselect(null);
                yield return new WaitForEndOfFrame();
                EventSystem.current.SetSelectedGameObject(null);
                _currentSelectable = null;
            }
        }

        /// <summary>
        /// Perform the click (Called from outside events.)
        /// </summary>
        public void DoClick()
        {
            Debug.Log("Doing click");
            _doingClick = true;
        }
    }
}
