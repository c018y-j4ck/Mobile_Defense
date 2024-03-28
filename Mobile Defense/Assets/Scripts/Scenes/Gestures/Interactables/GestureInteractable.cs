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
    /// The base class for all gesture interactables, including dials, sliders and buttons.
    /// Includes virtual methods to receive all gestures from the wand.
    /// </summary>
    public class GestureInteractable : MonoBehaviour
    {
        /// <summary>
        /// The transform of this interactable.
        /// </summary>
        [SerializeField]
        protected Transform _interactableTransform;

        /// <summary>
        /// Whether to allow the ray to interact with it.
        /// </summary>
        [SerializeField]
        private bool _allowRay = true;

        /// <summary>
        /// Whether to allow the touch to interact with it.
        /// </summary>
        [SerializeField]
        private bool _allowTouch = true;

        /// <summary>
        /// Whether it's holdable (The player can press trigger to interact with it.)
        /// </summary>
        [SerializeField]
        private bool _holdable = true;

        /// <summary>
        /// Whether gestures can be performed just by touching (And not necessarily by holding trigger.)
        /// </summary>
        [SerializeField]
        private bool _gesturesOnTouch = false;

        /// <summary>
        /// The mesh renderer.
        /// </summary>
        [SerializeField]
        protected MeshRenderer _renderer;

        /// <summary>
        /// The material for the normal state.
        /// </summary>
        [SerializeField]
        protected Material _normalMaterial;

        /// <summary>
        /// The material for the selected state.
        /// </summary>
        [SerializeField]
        protected Material _selectedMaterial;

        /// <summary>
        /// The material index to change when it's selected/deselected.
        /// </summary>
        [SerializeField]
        private int _materialIndex = 0;

        // Encapsulate the booleans for the pointer to view.
        public bool AllowRay { get => _allowRay; set => _allowRay = value; }
        public bool AllowTouch { get => _allowTouch; set => _allowTouch = value; }
        public bool Holdable { get => _holdable; set => _holdable = value; }
        public bool GesturesOnTouch { get => _gesturesOnTouch; set => _gesturesOnTouch = value; }

        /// <summary>
        /// Call from the pointer when the object is selected.
        /// </summary>
        public virtual void OnSelected()
        {
            // Set the material to selected.
            if (_selectedMaterial != null && _renderer.materials.Length > _materialIndex)
            {
                Material[] materials = _renderer.materials;

                materials[_materialIndex] = _selectedMaterial;

                _renderer.materials = materials;
            }
        }

        /// <summary>
        /// Call from the pointer when the object is deselected.
        /// </summary>
        public virtual void OnDeselect()
        {
            // Set the material to normal.
            if (_normalMaterial != null && _renderer.materials.Length > _materialIndex)
            {
                Material[] materials = _renderer.materials;

                materials[_materialIndex] = _normalMaterial;

                _renderer.materials = materials;
            }
        }

        /// <summary>
        /// Call when the trigger is pressed.
        /// </summary>
        public virtual void OnTriggerPressed() { }

        /// <summary>
        /// Call when the trigger is released.
        /// </summary>
        public virtual void OnTriggerReleased() { }

        /// <summary>
        /// Receive the rotation delta from the wand.
        /// </summary>
        /// <param name="pWandRotationDelta">The rotation delta</param>
        public virtual void WandRotationDeltaGesture(Vector3 pWandRotationDelta) { }

        /// <summary>
        /// Receive the current rotation from the wand.
        /// </summary>
        /// <param name="pWandRotation">The current rotation</param>
        public virtual void WandRotationGesture(Vector3 pWandRotation) { }

        /// <summary>
        /// Receive the movement delta from the wand.
        /// </summary>
        /// <param name="pWandMovementDelta">The movement delta</param>
        public virtual void WandPositionDeltaGesture(Vector3 pWandMovementDelta) { }

        /// <summary>
        /// Receive the current position from the wand.
        /// </summary>
        /// <param name="pWandPosition">The current position</param>
        public virtual void WandPositionGesture(Vector3 pWandPosition) { }

        /// <summary>
        /// Call when the object is touched.
        /// </summary>
        public virtual void OnTouchEnter() { }

        /// <summary>
        /// Call while the object is being touched.
        /// </summary>
        public virtual void OnTouchStay() { }

        /// <summary>
        /// Call when the object stops being touched.
        /// </summary>
        public virtual void OnTouchExit() { }
    }
}
