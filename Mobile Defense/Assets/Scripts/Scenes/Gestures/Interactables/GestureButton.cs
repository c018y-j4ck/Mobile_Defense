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
    /// Class for the button interactable, inherits from GestureInteractable.
    /// </summary>
    public class GestureButton : GestureInteractable
    {
        /// <summary>
        /// The starting state of this button.
        /// </summary>
        [SerializeField]
        private bool _startState = true;

        /// <summary>
        /// The animator for this button, containing the appropriate state machine.
        /// </summary>
        [SerializeField]
        private Animator _buttonAnimator;

        /// <summary>
        /// A public class definition inheriting UnityEvent<bool>() to create a dynamic unity event that receives a boolean.
        /// </summary>
        [Serializable]
        public class OnToggle : UnityEvent<bool> { };

        /// <summary>
        /// The OnToggle() dynamic UnityEvent, exposed in the inspector.
        /// </summary>
        [SerializeField]
        private OnToggle _onToggle;

        /// <summary>
        /// The current state of the button.
        /// </summary>
        private bool _state = false;

        /// <summary>
        /// The constant string for button animator boolean.
        /// </summary>
        private const string ON_ANIMATOR_BOOL = "On";

        private void Start()
        {
            // Toggle the button with the start state.
            Toggle(_startState);
        }

        /// <summary>
        /// On touch enter, toggle the button.
        /// </summary>
        public override void OnTouchEnter()
        {
            Toggle();
            base.OnTouchEnter();
        }

        /// <summary>
        /// Toggle without a parameter, simply switches the current state.
        /// </summary>
        public void Toggle()
        {
            _state = !_state;
            OnToggling();
        }

        /// <summary>
        /// Toggle with a parameter, sets it to a specific state.
        /// </summary>
        /// <param name="pToggle">The new state</param>
        public void Toggle(bool pToggle)
        {
            _state = pToggle;
            OnToggling();
        }

        /// <summary>
        /// On toggling, set the button animator and call the dynamic UnityEvent.
        /// </summary>
        public void OnToggling()
        {
            _buttonAnimator.SetBool(ON_ANIMATOR_BOOL, _state);

            _onToggle.Invoke(_state);
        }

        /// <summary>
        /// Toggle on on trigger pressed.
        /// </summary>
        public override void OnTriggerPressed()
        {
            Toggle();

            base.OnTriggerPressed();
        }
    }
}
