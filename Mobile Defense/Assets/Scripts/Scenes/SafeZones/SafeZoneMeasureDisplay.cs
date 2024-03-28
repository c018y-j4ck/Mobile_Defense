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

namespace TiltFiveDemos
{
    /// <summary>
    /// A simple class that displays the current measure of the safe zone in the board.
    /// Designed for a canvas that equals the measure of the board in millimeters.
    /// </summary>
    [ExecuteInEditMode]
    public class SafeZoneMeasureDisplay : MonoBehaviour
    {
        /// <summary>
        /// The position of the current safe zone, to know which axis to use for the length.
        /// </summary>
        [SerializeField]
        private SafeZoneSide.SafeZonePosition _safeZonePosition;

        /// <summary>
        /// The rect transform of the reference zone.
        /// </summary>
        [SerializeField]
        private RectTransform _referenceRect;

        /// <summary>
        /// The text to display.
        /// </summary>
        private TextMeshProUGUI _text;

        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            // Write the length in cm by dividing the units by 10, format to 2 decimals.
            switch (_safeZonePosition)
            {
                case SafeZoneSide.SafeZonePosition.Front:
                    _text.text = $"{(_referenceRect.sizeDelta.y / 10f).ToString("#.##")} cm";
                    break;

                case SafeZoneSide.SafeZonePosition.Back:
                    _text.text = $"{(_referenceRect.sizeDelta.y / 10f).ToString("#.##")} cm";
                    break;

                case SafeZoneSide.SafeZonePosition.Left:
                    _text.text = $"{(_referenceRect.sizeDelta.x / 10f).ToString("#.##")} cm";
                    break;

                case SafeZoneSide.SafeZonePosition.Right:
                    _text.text = $"{(_referenceRect.sizeDelta.x / 10f).ToString("#.##")} cm";
                    break;
            }
        }
    }
}
