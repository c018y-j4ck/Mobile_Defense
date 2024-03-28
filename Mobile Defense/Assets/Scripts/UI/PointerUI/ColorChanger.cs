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
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Simple class to change a color in an when the method is called.
    /// </summary>
    public class ColorChanger : MonoBehaviour
    {
        /// <summary>
        /// The array of colors to use.
        /// </summary>
        [SerializeField] private Color[] _colors;
        /// <summary>
        /// The image to change color.
        /// </summary>
        [SerializeField] private Image _image;

        /// <summary>
        /// The starting color index.
        /// </summary>
        private int _currentColorIndex = -1;

        /// <summary>
        /// Simple method to change the color of an image from an array of colors.
        /// </summary>
        public void ChangeColor()
        {
            if (_currentColorIndex + 1 >= _colors.Length || _currentColorIndex + 1 < 0)
            {
                _currentColorIndex = 0;
            }
            else
            {
                _currentColorIndex++;
            }

            _image.color = _colors[_currentColorIndex];
        }
    }
}
