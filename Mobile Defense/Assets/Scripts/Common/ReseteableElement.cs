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
using UnityEngine.Events;

namespace TiltFiveDemos
{
    /// <summary>
    /// Simple class for an element that can be resetted by ResetManager.
    /// </summary>
    public class ReseteableElement : MonoBehaviour
    {
        /// <summary>
        /// The event on reset.
        /// </summary>
        [SerializeField]
        private UnityEvent _onReset;

        /// <summary>
        /// Perform the reset, usually called by ResetManager.
        /// </summary>
        public void DoReset()
        {
            _onReset.Invoke();
        }
    }
}
