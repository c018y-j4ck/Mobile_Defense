/*
 * Copyright (C) 2020 Tilt Five, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// A pointer that can interact with interactable objects by sending information from the wand, including movement, rotation, and touches. 
    /// Also uses a line renderer to display a line from the want to the object.
    /// </summary>
    public class GesturePointer : MonoBehaviour
    {
        /// <summary>
        /// The transform of the wand.
        /// </summary>
        [SerializeField]
        private Transform _wandTransform;

        /// <summary>
        /// The transform of the pointer at the tip of the wand.
        /// </summary>
        [SerializeField]
        private Transform _pointerTransform;

        /// <summary>
        /// The transform of the point where the wand performs the raycasting to find interactable objects.
        /// </summary>
        [SerializeField]
        private Transform _pointOrigin;

        /// <summary>
        /// The layer mask for the interactable objects.
        /// </summary>
        [SerializeField]
        private LayerMask _interactableLayerMask;

        /// <summary>
        /// The line renderer.
        /// </summary>
        [SerializeField]
        private LineRenderer _line;

        /// <summary>
        /// The normal material for the line when it's not touching an interactable.
        /// </summary>
        [SerializeField]
        private Material _normalLineMaterial;

        /// <summary>
        /// The active material for the line when it's touching an interactable.
        /// </summary>
        [SerializeField]
        private Material _activeLineMaterial;

        /// <summary>
        /// The previous rotation of the wand.
        /// </summary>
        private Vector3 _previousWandRotation = Vector3.zero;

        /// <summary>
        /// The previous position of the wand.
        /// </summary>
        private Vector3 _previousWandPosition = Vector3.zero;

        /// <summary>
        /// The current rotation delta of the wand.
        /// </summary>
        private Vector3 _rotationDelta = Vector3.zero;

        /// <summary>
        /// The current position delta of the wand.
        /// </summary>
        private Vector3 _positionDelta = Vector3.zero;

        /// <summary>
        /// Flag if we're current touching an interactable.
        /// </summary>
        private bool _doingTouch = false;

        /// <summary>
        /// Flag if we're currently holding the ray on an interactable (pressing the trigger)
        /// </summary>
        private bool _holdingRay = false;

        /// <summary>
        /// Flag if we're currently holding the touch on an interactable (pressing the trigger)
        /// </summary>
        private bool _holdingTouch = false;

        /// <summary>
        /// Flag if we pressed the trigger.
        /// </summary>
        private bool _pressedTrigger = false;

        /// <summary>
        /// The current interactable.
        /// </summary>
        private GestureInteractable _currentInteractable = null;

        /// <summary>
        /// The last point of the pointer.
        /// </summary>
        private Vector3 _lastPoint = Vector3.zero;

        private void Start()
        {
            _line.material = _normalLineMaterial;
        }

        private void Update()
        {
            // Set the initial line settings
            _line.gameObject.SetActive(true);
            _line.positionCount = 2;
            _line.SetPosition(0, _pointOrigin.position);
            _line.SetPosition(1, _pointOrigin.position + (_pointOrigin.forward * 12f)); // Set the line to look forward

            // Assign the rotation delta by comparing the previous rotation to the new rotation. Use Mathf.DeltaAngle to find the closest rotation distance.
            _rotationDelta = new Vector3(Mathf.DeltaAngle(_wandTransform.eulerAngles.x, _previousWandRotation.x),
                                        Mathf.DeltaAngle(_wandTransform.eulerAngles.y, _previousWandRotation.y),
                                        Mathf.DeltaAngle(_wandTransform.eulerAngles.z, _previousWandRotation.z));

            // Assign the position delta by finding the difference between the current position and the previous position
            _positionDelta = _wandTransform.localPosition - _previousWandPosition;

            // Make sure that the trigger isn't being pressed at the start.
            if (TiltFive.Input.TryGetTrigger(out float pPressedTriggerValue) && pPressedTriggerValue < 0.5f) _pressedTrigger = false;

            // If we're holding the touch, send the gestures, set the last position of the pointer to the position of the interactable object and finish the update.
            if (_holdingTouch)
            {
                _line.gameObject.SetActive(false);
                _lastPoint = _currentInteractable.transform.position;

                if (_currentInteractable != null)
                {
                    SendGestures(_currentInteractable);
                }

                if (!_pressedTrigger)
                {
                    _holdingTouch = false;
                    _lastPoint = Vector3.zero;
                    _currentInteractable.OnTriggerReleased(); // Call OnTriggerReleased() when the trigger is released.
                }

                _line.material = _normalLineMaterial;
                return;
            }

            // Disable the line when touching an interactable.
            if (_doingTouch)
            {
                _line.gameObject.SetActive(false);
                return;
            }

            bool rayFoundInteractable = false;

            // If holding the ray, send the gestures and set the end of the ray to the position of the interactable and finish the update.
            if (_holdingRay)
            {
                if (_currentInteractable != null)
                {
                    _lastPoint = _currentInteractable.transform.position;
                    SendGestures(_currentInteractable);

                    _line.material = _activeLineMaterial;

                    _line.positionCount = 2;
                    _line.SetPosition(0, _pointOrigin.position);
                    _line.SetPosition(1, _lastPoint);
                }

                if (!_pressedTrigger)
                {
                    _currentInteractable.OnTriggerReleased(); // Call OnTriggerReleased() when the trigger is released.
                    _line.material = _normalLineMaterial;
                    _holdingRay = false;
                    _lastPoint = Vector3.zero;
                }

                return;
            }

            Ray ray = new Ray(_pointOrigin.position, _pointOrigin.forward);

            // Perform the raycast using the interactable layer mask
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 12f, _interactableLayerMask))
            {
                GestureInteractable rayInteractable = hitInfo.collider.GetComponent<GestureInteractable>();

                // Make sure the interactable allows rays.
                if (rayInteractable != null && rayInteractable.AllowRay)
                {
                    if (_currentInteractable != rayInteractable)
                    {
                        // Deselect the current interactable if it's not the same as the new one.
                        if (_currentInteractable != null) _currentInteractable.OnDeselect();
                        _currentInteractable = rayInteractable;
                        _currentInteractable.OnSelected();
                    }

                    rayFoundInteractable = true;

                    _line.material = _activeLineMaterial;

                    _line.positionCount = 2;
                    _line.SetPosition(0, _pointOrigin.position);
                    _line.SetPosition(1, hitInfo.point);

                    // If we pressed the trigger while selecting an interactable, set it to hold.
                    if (TiltFive.Input.TryGetTrigger(out float pTriggerValue) && pTriggerValue > 0.5f)
                    {
                        if (!_pressedTrigger)
                        {
                            _currentInteractable.OnTriggerPressed();
                            _pressedTrigger = true;
                        }

                        if (rayInteractable.Holdable)
                        {
                            _lastPoint = hitInfo.point;
                            _holdingRay = true;
                            SendGestures(rayInteractable);
                        }
                    }
                }
            }

            // Deselect the current interactable if the raycast didn't find an interactable.
            if (!rayFoundInteractable && _currentInteractable != null)
            {
                _line.material = _normalLineMaterial;
                _currentInteractable.OnDeselect();
                _currentInteractable = null;
            }
        }

        /// <summary>
        /// On LateUpdate, set the position of the pointer and the previous position and rotation of the wand.
        /// </summary>
        private void LateUpdate()
        {
            _previousWandRotation = _wandTransform.eulerAngles;
            _previousWandPosition = _wandTransform.localPosition;

            if (_holdingTouch)
            {
                _pointerTransform.position = _lastPoint;
            }
        }

        /// <summary>
        /// Send the gestures to the interactable.
        /// </summary>
        /// <param name="pInteractable">The interactable object</param>
        private void SendGestures(GestureInteractable pInteractable)
        {
            pInteractable.WandRotationDeltaGesture(_rotationDelta);
            pInteractable.WandRotationGesture(_wandTransform.eulerAngles);
            pInteractable.WandPositionGesture(_wandTransform.position);
            pInteractable.WandPositionDeltaGesture(_positionDelta);
            pInteractable.OnSelected();
        }

        /// <summary>
        /// Find if we're touching the interactable on OnTriggerEnter().
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable") && !_holdingRay)
            {
                GestureInteractable touchInteractable = other.GetComponent<GestureInteractable>();

                if (touchInteractable != null && touchInteractable.AllowTouch)
                {
                    touchInteractable.OnSelected();
                    touchInteractable.OnTouchEnter();

                    _doingTouch = true;
                }
            }
        }

        /// <summary>
        /// Send gestures on OnTriggerStay() if the interactable object allows it.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Interactable") && !_holdingRay)
            {
                GestureInteractable touchInteractable = other.GetComponent<GestureInteractable>();

                if (touchInteractable != null && touchInteractable.AllowTouch)
                {
                    // Set the current interactable to hold if the player presses the trigger.
                    if (TiltFive.Input.TryGetTrigger(out float pTriggerValue) && pTriggerValue > 0.5f && !_holdingTouch)
                    {
                        if (!_pressedTrigger)
                        {
                            touchInteractable.OnTriggerPressed();
                            _pressedTrigger = true;
                        }

                        if (touchInteractable.Holdable)
                        {
                            _holdingTouch = true;
                            _currentInteractable = touchInteractable;
                            _lastPoint = _pointerTransform.position;
                        }
                    }

                    // Send gestures if the current interactable accepts gestures without holding.
                    if (!_holdingTouch && touchInteractable.GesturesOnTouch)
                    {
                        SendGestures(touchInteractable);
                    }

                    touchInteractable.OnTouchStay();

                    _doingTouch = true;
                }
            }
        }

        /// <summary>
        /// Send final gestures on OnTriggerExit().
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                GestureInteractable touchInteractable = other.GetComponent<GestureInteractable>();

                if (touchInteractable != null)
                {
                    _doingTouch = false;

                    if (touchInteractable.AllowTouch)
                    {
                        touchInteractable.OnDeselect();
                        touchInteractable.OnTouchExit();
                    }
                }
            }
        }
    }
}
