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
    /// Class for the dial interactable, inherits from GestureInteractable.
    /// 
    /// This dial is designed such that it's contained by a parent that is rotated to the desired visual position of the dial.
    /// </summary>
    public class GestureDial : GestureInteractable
    {
        /// <summary>
        /// The multiplier factor of the rotation.
        /// </summary>
        [SerializeField]
        private float _rotationMultiplier = 1.5f;

        /// <summary>
        /// Whether the rotation should clamped (Limited at the start and end of the dial.)
        /// </summary>
        [SerializeField]
        private bool _clampRotation = false;

        /// <summary>
        /// Define a class that inherits from UnityEvent<float> to expose a dynamic UnityEvent in the inspector,
        /// for the event that is triggered on the change in absolute rotation of the dial.
        /// </summary>
        [Serializable]
        public class OnAbsoluteRotationChanged : UnityEvent<float> { };

        /// <summary>
        /// The event to update the absolute rotation when the rotation changes.
        /// </summary>
        [SerializeField]
        private OnAbsoluteRotationChanged _onAbsolutePositionChanged;

        /// <summary>
        /// Define a class that inherits from UnityEvent<float> to expose a dynamic UnityEvent in the inspector,
        /// for the event that is triggered on the change in delta rotation of the dial.
        /// </summary>
        [Serializable]
        public class OnDeltaRotationChanged : UnityEvent<float> { };

        /// <summary>
        /// The event to update the delta rotation when the rotation changes.
        /// </summary>
        [SerializeField]
        private OnDeltaRotationChanged _onDeltaPositionChanged;

        /// <summary>
        /// The current dial rotation.
        /// </summary>
        private float _dialRotation = 0.5f;

        /// <summary>
        /// The dial delta of the rotation.
        /// </summary>
        private float _dialDelta = 0.5f;

        private void Start()
        {
            _renderer.material = _normalMaterial;

            // Update the initial values of absolute rotation and delta rotation.
            UpdateDialAbsoluteRotation();
            UpdateDialDeltaRotation();
        }

        /// <summary>
        /// Receive the rotation from the wand.
        /// </summary>
        /// <param name="pWandRotationDelta">The rotation delta from the wand.</param>
        public override void WandRotationDeltaGesture(Vector3 pWandRotationDelta)
        {
            // Increase the rotation on the Z axis with the delta rotation and the multiplier.
            float newRotation = _interactableTransform.localEulerAngles.z + (pWandRotationDelta.z * _rotationMultiplier);

            // If the rotation is clamped, it never goes under 0 angles or higher than 359.99
            if (_clampRotation)
            {
                newRotation = Mathf.Clamp(newRotation, 0f, 359.99f);
            }

            // Update the transform of the dial.
            _interactableTransform.localEulerAngles = new Vector3(_interactableTransform.localEulerAngles.x, _interactableTransform.localEulerAngles.y, newRotation);

            _dialDelta = pWandRotationDelta.z * (1f / 360f);

            UpdateDialDeltaRotation();
            UpdateDialAbsoluteRotation();

            base.WandRotationDeltaGesture(pWandRotationDelta);
        }

        private void UpdateDialAbsoluteRotation()
        {
            // Calculate the current normalized rotation from 1 to 0 by multiplying the rotation on the z axis by 1/360th.
            _dialRotation = _interactableTransform.localEulerAngles.z * (1f / 360f);

            // Invoke the UnityEvent on the absolute position changed.
            _onAbsolutePositionChanged.Invoke(_dialRotation);
        }

        private void UpdateDialDeltaRotation()
        {
            // Invoke the UnityEvent on the delta position changed.
            _onDeltaPositionChanged.Invoke(_dialDelta);
        }
    }
}
