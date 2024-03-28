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

using System;
using System.Collections;
using TiltFive;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Orients the environment when the player moves from one side of the board to the other.
    /// </summary>
    public class AutoOrientation: MonoBehaviour
    {
        /// <summary>
        /// The transform of the camera.
        /// </summary>
        [SerializeField]
        private Transform _cameraTransform;

        /// <summary>
        /// The transform of the board.
        /// </summary>
        [SerializeField]
        private Transform _boardTransform;

        /// <summary>
        /// The deadzone of the direction from the board to the camera before the rotation is performed.
        /// </summary>
        [SerializeField]
        private float _directionDeadzone = 0.25f;

        /// <summary>
        /// The amount of the dot product before the rotation is performed.
        /// </summary>
        [SerializeField]
        private float _dotDeadzone = 0.25f;

        /// <summary>
        /// The start rotation of the environment.
        /// </summary>
        private Vector3 _startRotation;

        /// <summary>
        /// The current rotation the environment is moving to.
        /// </summary>
        private Vector3 _currentRotation;

        /// <summary>
        /// The current dot product of the direction from the board to the camera, and the camera forward.
        /// </summary>
        private float _currentDot;

        /// <summary>
        /// The current direction the camera is looking to.
        /// </summary>
        private Vector3 _currentViewDirection;

        // Encapsulate these values for use by other classes.
        public float DirectionDeadzone { get => _directionDeadzone; set => _directionDeadzone = value; }
        public float DotDeadzone { get => _dotDeadzone; set => _dotDeadzone = value; }
        public float CurrentDot { get => _currentDot; set => _currentDot = value; }
        public Vector3 CurrentViewDirection { get => _currentViewDirection; set => _currentViewDirection = value; }

        /// <summary>
        /// Get the start rotation as it is currently in the environment.
        /// </summary>
        private void Start()
        {
            _startRotation = transform.localEulerAngles;

            _currentRotation = _startRotation;
        }

        // Update is called once per frame
        void Update()
        { 
            // Get the position of the camera at the height of the board
            Vector3 flatCamera = new Vector3(_cameraTransform.position.x, _boardTransform.position.y, _cameraTransform.position.z);

            // The direction of the camera from the board, using the relative rotation of the board, so that the board can start on any flat rotation.
            _currentViewDirection = _boardTransform.InverseTransformDirection((_boardTransform.position - flatCamera).normalized);

            // The forward direction of the camera relative to the board.
            Vector3 cameraForward = _boardTransform.InverseTransformDirection(_cameraTransform.forward);

            // Get the dot product of the direction to the board and camera forward, to find out if the player is currently looking at the board.
            // If the dot product is 1, the player is directly looking at the board. If the dot product is 0, the look direction is perpendicular to the board.
            // If the dot product is -1, the player is looking opposite at the board.
            _currentDot = Vector3.Dot(_currentViewDirection, cameraForward);

            // Make sure the dot product is higher than the desired deadzone.
            if (_currentDot < _dotDeadzone) return;

            // Assing the new rotation, adding or taking 90 or 180 degrees, depending on the new look direction.
            Vector3 newRotation = _currentRotation;

            if (_currentViewDirection.z > _directionDeadzone && _currentViewDirection.z > Mathf.Abs(_currentViewDirection.x)) // Front
            {
                newRotation = _startRotation;
            }
            else if (_currentViewDirection.z < -_directionDeadzone && _currentViewDirection.z < -Mathf.Abs(_currentViewDirection.x)) // Back
            {
                newRotation = _startRotation + new Vector3(0f, (90f * 2), 0f);
            }
            else if (_currentViewDirection.x > _directionDeadzone && _currentViewDirection.x > Mathf.Abs(_currentViewDirection.z)) // Right
            {
                newRotation = _startRotation + new Vector3(0f, 90f, 0f);
            }
            else if (_currentViewDirection.x < -_directionDeadzone && _currentViewDirection.x < -Mathf.Abs(_currentViewDirection.z)) // Left
            {
                newRotation = _startRotation + new Vector3(0f, -90f, 0f);
            }

            // If we haven't already selected this rotation, perform the new rotation.
            if (_currentRotation.y != newRotation.y)
            {
                DoRotation(newRotation);
            }
        }

        /// <summary>
        /// The coroutine of the rotation
        /// </summary>
        private Coroutine _doRotationCoroutine;

        /// <summary>
        /// The start of the rotation sequence.
        /// Removes the coroutine if it already exists and restart it, so that rotations can start from any rotation point.
        /// </summary>
        /// <param name="pTargetRotation"></param>
        private void DoRotation(Vector3 pTargetRotation)
        {
            if (_doRotationCoroutine != null)
            {
                StopCoroutine(_doRotationCoroutine);
                _doRotationCoroutine = null;
            }

            _currentRotation = pTargetRotation;

            _doRotationCoroutine = StartCoroutine(DoRotationCoroutine(pTargetRotation));
        }

        /// <summary>
        /// Perform the rotation coroutine.
        /// </summary>
        /// <param name="pTargetRotation"></param>
        /// <returns></returns>
        private IEnumerator DoRotationCoroutine(Vector3 pTargetRotation)
        {
            float normal = 0f;

            Quaternion originalRotation = transform.localRotation;

            Quaternion targetRotation = Quaternion.Euler(pTargetRotation);

            while (normal < 1f)
            {
                transform.localRotation = Quaternion.Lerp(originalRotation, targetRotation, normal);

                normal += Time.deltaTime * 6f;

                yield return null;
            }

            transform.localRotation = Quaternion.Lerp(originalRotation, targetRotation, 1f);

            yield return null;
        }
    }
}
