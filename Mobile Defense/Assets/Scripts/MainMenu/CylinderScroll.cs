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
using UnityEngine.EventSystems;

namespace TiltFiveDemos
{
    /// <summary>
    /// A cylinder UI that scrolls depending on the selected button.
    /// The UI is arranged in a separate canvas, which is then projected onto the cylinder mesh using a render texture.
    /// </summary>
    public class CylinderScroll : MonoBehaviour
    {
        /// <summary>
        /// The start rotation with the first button, may change depending on the mesh's UV mapping.
        /// </summary>
        [SerializeField]
        private float _startRotation;

        /// <summary>
        /// The transform of the cylinder.
        /// </summary>
        [SerializeField]
        private Transform _cylinder;
        /// <summary>
        /// The container with all the UI buttons, arranged in a normal UI canvas.
        /// </summary>
        [SerializeField]
        private Transform _buttonsContainer;

        /// <summary>
        /// The animation curve used to tweak the rotation feel.
        /// </summary>
        [SerializeField]
        private AnimationCurve _rotationCurve;

        /// <summary>
        /// The current selected button.
        /// </summary>
        private GameObject _selectedButton;

        /// <summary>
        /// The amount of rotation for each button.
        /// </summary>
        private float _rotationFactor = 0;

        /// <summary>
        /// The speed of rotation.
        /// </summary>
        private float _rotationSpeed = 2f;

        private bool _rotateImmediately = true;
        public bool RotateImmediately { get => _rotateImmediately; set => _rotateImmediately = value; }

        private void Start()
        {
            // Calculate the rotation factor from the amount of buttons.
            _rotationFactor = 360f / _buttonsContainer.childCount;

            // Set the initial rotation.
            _cylinder.rotation = Quaternion.Euler(new Vector3(0, -90, _startRotation));
        }

        /// <summary>
        /// Find the rotation for the selected button.
        /// Receive from an event trigger on each button of the menu.
        /// </summary>
        /// <param name="pEventData"></param>
        public void OnSelected(BaseEventData pEventData)
        {
            GameObject selectedButton = pEventData.selectedObject;

            _selectedButton = selectedButton;

            RotateToSelection();
        }

        /// <summary>
        /// Rotate to the selected button
        /// </summary>
        public void RotateToSelection()
        {
            if(_selectedButton != null)
            {
                int position = 0;

                // Find the button that matches.
                for (int i = 0; i < _buttonsContainer.childCount; i++)
                {
                    if (_buttonsContainer.GetChild(i).gameObject == _selectedButton)
                    {
                        position = i;
                        break;
                    }
                }

                // Calculate the accurate rotation.
                float rotation = _startRotation - (_rotationFactor * position);

                if (_rotateImmediately)
                {
                    RotateImmediate(rotation);
                }
                else
                {
                    // Start rotation coroutine.
                    Rotate(rotation);
                }
            }
        }

        /// <summary>
        /// Store the coroutine in order to prevent it from running multiple times.
        /// </summary>
        private Coroutine _rotateCoroutine;

        /// <summary>
        /// Start the rotation sequence.
        /// </summary>
        /// <param name="pRotation"></param>
        private void Rotate(float pRotation)
        {
            StopRotateCoroutine();

            // Store the new rotation coroutine so that it can be removed if necessary.
            _rotateCoroutine = StartCoroutine(RotateCoroutine(pRotation));
        }

        /// <summary>
        /// The cylinder rotation coroutine, rotates the cylinder to the position in a given speed.
        /// </summary>
        /// <param name="pRotation"></param>
        /// <returns></returns>
        private IEnumerator RotateCoroutine(float pRotation)
        {
            float normal = 0f;

            Quaternion currentRotation = _cylinder.rotation;

            // Convert the rotation to quaternion.
            Quaternion rotation = Quaternion.Euler(new Vector3(0, -90, pRotation));

            while (normal <= 1f)
            {
                _cylinder.rotation = Quaternion.Lerp(currentRotation, rotation, _rotationCurve.Evaluate(normal)); // Evaluate the curve for better control of the animation.

                normal += Time.deltaTime * _rotationSpeed;

                yield return null;
            }

            normal = 1f;

            _cylinder.rotation = Quaternion.Lerp(currentRotation, rotation, _rotationCurve.Evaluate(normal)); // Make sure the last step is at the end of the normal.

            _rotateCoroutine = null;
        }

        /// <summary>
        /// Rotate immediately to position.
        /// </summary>
        /// <param name="pRotation">The rotation</param>
        private void RotateImmediate(float pRotation)
        {
            StopRotateCoroutine();

            Quaternion rotation = Quaternion.Euler(new Vector3(0, -90, pRotation));

            _cylinder.rotation = rotation;

            _rotateImmediately = false;
        }

        /// <summary>
        /// Stop the rotation coroutine.
        /// </summary>
        private void StopRotateCoroutine()
        {
            if (_rotateCoroutine != null)
            {
                StopCoroutine(_rotateCoroutine);
                _rotateCoroutine = null;
            }
        }
    }
}
