using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Interact with the UI by touching the UI physically with the wand.
    /// </summary>
    public class WandPointerTouch : WandPointer
    {
        /// <summary>
        /// The UI tag on the canvases that are interactable with the wand.
        /// </summary>
        private const string UI_TAG = "WandUI";

        private SphereCollider _collider;

        protected override void Start()
        {
            _collider = GetComponent<SphereCollider>();
            _line.gameObject.SetActive(false);
            _pointer.SetActive(false);

            base.Start();
        }

        /// <summary>
        /// On trigger enter, make sure get the canvas' raycaster.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(UI_TAG))
            {                
                GraphicRaycaster raycaster = other.GetComponent<GraphicRaycaster>();

                if (raycaster != null)
                {
                    CheckCanvas(raycaster, _pointerOrigin.position);
                }
            }
        }

        /// <summary>
        /// Override check canvas since highlighting selectable elements is not necessary with this mode of interaction.
        /// </summary>
        /// <param name="pGraphicsRaycaster"></param>
        /// <param name="pPosition"></param>
        protected override void CheckCanvas(GraphicRaycaster pGraphicsRaycaster, Vector3 pPosition)
        {
            //Set up the new Pointer Event
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);

            //Set the Pointer Event Position to that of the virtual cursor position
            pointerEventData.position = _glassesCamera.WorldToScreenPoint(pPosition);

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pGraphicsRaycaster.Raycast(pointerEventData, results);

            bool hitButton = false;

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    Selectable selectable = results[i].gameObject.GetComponent<Selectable>();

                    if (selectable != null && selectable is Button)
                    {
                        // If selectable is button, proceed to click
                        Button button = selectable as Button;

                        // Invoke the button event
                        button.onClick.Invoke();

                        hitButton = true;
                    }
                }
            }

            // If we hit the button, start a coroutine to disable the collider for a short time.
            if(hitButton)
            {
                StartCoroutine(WaitDisableCollider());
            }
        }

        /// <summary>
        /// Coroutine to disable the collider for a short time, to avoid accidentally hitting the object again.
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitDisableCollider()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(0.4f);
            _collider.enabled = true;
        }
    }
}
