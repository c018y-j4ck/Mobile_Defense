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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TiltFiveDemos
{    
    /// <summary>
    /// Class for the vertical slider interactable, inherits from GestureInteractable.
    /// 
    /// This class is designed for a horizontal slider contained within a bounding box of arbitrary scale, 
    /// such that the local position is 0 when the slider is at the left end of the bounding box, 
    /// and the local position is 1 when the slider is at the right end of the bounding box.
    /// </summary>
    public class GestureSliderHorizontal : GestureInteractable
    {
        /// <summary>
        /// The factor of the movement.
        /// </summary>
        [SerializeField]
        private float _movementMultiplier = 1f;

        /// <summary>
        /// Define a class that inherits from UnityEvent<float> to expose a dynamic UnityEvent in the inspector,
        /// for the event that is triggered on the change in absolute position of the slider.
        /// </summary>
        [Serializable]
        public class OnAbsolutePositionChanged : UnityEvent<float> { };

        /// <summary>
        /// UnityEvent that is called with the absolute position information.
        /// </summary>
        [SerializeField]
        private OnAbsolutePositionChanged _onAbsolutePositionChanged;

        /// <summary>
        /// Define a class that inherits from UnityEvent<float> to expose a dynamic UnityEvent in the inspector,
        /// for the event that is triggered on the change in delta position of the slider.
        /// </summary>
        [Serializable]
        public class OnDeltaPositionChanged : UnityEvent<float> { };

        /// <summary>
        /// UnityEvent that is called with the delta position information.
        /// </summary>
        [SerializeField]
        private OnDeltaPositionChanged _onDeltaPositionChanged;

        private float _sliderPosition = 0.5f;
        private float _sliderDelta = 0.5f;

        private float _previousPosition = 0.5f;

        private void Start()
        {
            // Assign the previous position from the transform's local position.
            _previousPosition = _interactableTransform.localPosition.x;

            _renderer.material = _normalMaterial;

            // Update the initial values for absolute position and delta position.
            UpdateSliderAbsolutePosition();
            UpdateSliderDeltaPosition();
        }

        /// <summary>
        /// Called when the wand changes position.
        /// </summary>
        /// <param name="pWandPositionDelta">The wand position delta.</param>
        public override void WandPositionDeltaGesture(Vector3 pWandPositionDelta)
        {
            // Increase the new position using the delta and the movement multiplier.
            float newPosition = _interactableTransform.localPosition.x + (pWandPositionDelta.x * _movementMultiplier);

            // Update the local position with the delta, clamping the position on X with the limits of the bounding box.
            _interactableTransform.localPosition = new Vector3(Mathf.Clamp(newPosition, -0.5f, 0.5f), _interactableTransform.localPosition.y, _interactableTransform.localPosition.z);

            // Calculate the delta of this slider with the difference between the current position and the previous position.
            _sliderDelta = _previousPosition - _interactableTransform.localPosition.x;

            // Update the slider absolute position and delta.
            UpdateSliderDeltaPosition();
            UpdateSliderAbsolutePosition();

            // Set the previous local position.
            _previousPosition = _interactableTransform.localPosition.x;

            base.WandPositionDeltaGesture(pWandPositionDelta);
        }

        /// <summary>
        /// Update the absolute position of the slider, and call the event on change.
        /// </summary>
        private void UpdateSliderAbsolutePosition()
        {
            _sliderPosition = _interactableTransform.localPosition.x + 0.5f;

            _onAbsolutePositionChanged.Invoke(_sliderPosition);
        }

        /// <summary>
        /// Update the delta position of the slider, and call the event on change.
        /// </summary>
        private void UpdateSliderDeltaPosition()
        {
            _onDeltaPositionChanged.Invoke(_sliderDelta);
        }
    }
}
