using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TiltFive;

namespace TiltFiveDemos
{
    /// <summary>
    /// Class that handles a view pointer, a cursor that appears fixed to the center of the glasses.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class ViewPointer : BaseDemoInput
    {
        /// <summary>
        /// The reference camera.
        /// </summary>
        private Camera _referenceCamera;

        /// <summary>
        /// A layer mask for the interactables layer.
        /// </summary>
        [SerializeField]
        private LayerMask _interactablesLayerMask;

        /// <summary>
        /// Normal Color.
        /// </summary>
        [SerializeField]
        private Color _normalColor;

        /// <summary>
        /// Highlighted Color.
        /// </summary>
        [SerializeField]
        private Color _highlightedColor;

        /// <summary>
        /// The pointer image.
        /// </summary>
        private SpriteRenderer _pointerImage;

        /// <summary>
        /// The current interactable.
        /// </summary>
        private ViewPointerInteractableBase _currentInteractable = null;

        /// <summary>
        /// Flag to perform click from alternative input methods.
        /// </summary>
        private bool _doingClick = false;

        private void Start()
        {
            _pointerImage = GetComponent<SpriteRenderer>();
            _pointerImage.color = _normalColor;
        }

        private bool CameraIsReady()
        {
            if (_referenceCamera == null)
            {
                _referenceCamera = TiltFive.Glasses.GetLeftEye(PlayerIndex.One);
            }

            return _referenceCamera != null;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_active) return;

            if (!CameraIsReady()) return;

            RaycastCamera();

            // Always look at player
            transform.LookAt(_referenceCamera.transform);
        }

        /// <summary>
        /// Perform the physics raycasting for interaction objects
        /// </summary>
        private void RaycastCamera()
        {
            Ray ray = new Ray(_referenceCamera.transform.position, _referenceCamera.transform.forward);

            RaycastHit hit;

            // Perform a raycast for all objects in the desired layer ("Interactable")
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _interactablesLayerMask))
            {
                transform.position = hit.point;

                GameObject hitObject = hit.collider.gameObject;

                // Get ViewPointerInteractable from the object.
                ViewPointerInteractableBase interactable = hitObject.GetComponent<ViewPointerInteractableBase>();

                if (interactable != null)
                {
                    _pointerImage.color = _highlightedColor;

                    // Check if there's an interactable already selected and highlight the new one, while unhighlighting the older one.
                    if(_currentInteractable != null)
                    {
                        if(_currentInteractable != interactable)
                        {
                            _currentInteractable.StopHighlight();
                            _currentInteractable = interactable;
                            _currentInteractable.DoHighlight();
                        }
                    }
                    else
                    {
                        _currentInteractable = interactable;
                        _currentInteractable.DoHighlight();
                    }

                    // If there's a click, perform the interaction on ViewPointerInteractacble.
                    if (UnityEngine.Input.GetButtonDown("CursorClick") || _doingClick)
                    {
                        _doingClick = false;
                        interactable.DoInteraction();
                    }
                }
                else
                {
                    // Return to normal color
                    _pointerImage.color = _normalColor;

                    // If there was an interactable selected, disable the highlighting.
                    if (_currentInteractable != null)
                    {
                        _currentInteractable.StopHighlight();
                        _currentInteractable = null;
                    }
                }
            }
        }

        /// <summary>
        /// Receive click from alternative input methods.
        /// </summary>
        public void DoClick()
        {
            _doingClick = true;
        }
    }
}
