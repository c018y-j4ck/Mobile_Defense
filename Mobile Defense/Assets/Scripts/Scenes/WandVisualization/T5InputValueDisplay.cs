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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Base class for a simple display of values from the T5InputVisualizer.
    /// </summary>
    public class T5InputValueDisplay : MonoBehaviour
    {
        /// <summary>
        /// The visualizer.
        /// </summary>
        [SerializeField]
        protected T5InputVisualizer _visualizer;

        /// <summary>
        /// The first string of before the display.
        /// </summary>
        [SerializeField]
        protected string _openingString;

        /// <summary>
        /// The text label.
        /// </summary>
        [SerializeField]
        protected TextMeshProUGUI _text;

        /// <summary>
        /// The value gathered from the visualizer, obtained in each child class in Update().
        /// </summary>
        protected string _value = "0.0";

        /// <summary>
        /// Display the string in late update
        /// </summary>
        void LateUpdate()
        {
            _text.text = $"{_openingString}\n{_value}";
        }
    }
}
