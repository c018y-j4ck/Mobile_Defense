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
    /// The base class that input classes should have, for use with certain elements that need to change the state of the inputs, such as pop-ups.
    /// </summary>
    public class BaseDemoInput : MonoBehaviour
    {
        /// <summary>
        /// Is this input active?
        /// </summary>
        protected bool _active = true;

        public bool Active { get => _active; set => _active = value; }

        /// <summary>
        /// Virtual method to stop all input on T5 wand (disable flags).
        /// </summary>
        public virtual void StopAllT5Input()
        {

        }
    }
}
