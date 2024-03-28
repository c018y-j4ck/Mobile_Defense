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
    /// Class for a safe zone side of a specific position.
    /// </summary>
    public class SafeZoneSide : MonoBehaviour
    {
        /// <summary>
        /// Enum containing the safe zone positions.
        /// </summary>
        public enum SafeZonePosition
        {
            Front,
            Back,
            Right,
            Left
        }

        /// <summary>
        /// The safe zone position.
        /// </summary>
        [SerializeField]
        private SafeZonePosition _position;

        /// <summary>
        /// The unsafe area rect transform.
        /// </summary>
        [SerializeField]
        private RectTransform _unsafePosition;

        /// <summary>
        /// The warning area rect transform.
        /// </summary>
        [SerializeField]
        private RectTransform _warningPosition;

        /// <summary>
        /// The base length of the unsafe zone.
        /// </summary>
        private float _baseLengthUnsafe = 50f;

        /// <summary>
        /// The base length of the warning zone.
        /// </summary>
        private float _baseLengthWarning = 750f;

        // Encapsulate the position.
        public SafeZonePosition Position { get => _position; set => _position = value; }

        public void Start()
        {
            // Select if the length comes from the y or x axis depending on the position
            switch (_position)
            {
                case SafeZonePosition.Front:
                case SafeZonePosition.Back:
                    _baseLengthUnsafe = _warningPosition.sizeDelta.y;
                    _baseLengthWarning = _unsafePosition.sizeDelta.y;
                    break;

                case SafeZonePosition.Left:
                case SafeZonePosition.Right:
                    _baseLengthUnsafe = _warningPosition.sizeDelta.x;
                    _baseLengthWarning = _unsafePosition.sizeDelta.x;
                    break;
            }
        }

        /// <summary>
        /// Set the lengths of the unsafe and warning zones based on the factors calculated in SafeZoneManager.
        /// </summary>
        /// <param name="pUnsafeFactor">The length factor of the unsafe zone</param>
        /// <param name="pWarningFactorp">The length factor of the warning zone</param>
        public void SetRectsLengths(float[] pUnsafeFactor, float[] pWarningFactorp)
        {
            float unsafeFactor = 1f;

            foreach (float factor in pUnsafeFactor)
            {
                unsafeFactor *= factor;
            }

            float warningFactor = 1f;

            foreach (float factor in pWarningFactorp)
            {
                warningFactor *= factor;
            }

            float warningLength = _baseLengthWarning * warningFactor;
            float unsafeLength = _baseLengthUnsafe * unsafeFactor;

            // Set the y or x axis depending on the position.
            switch (_position)
            {
                case SafeZonePosition.Front:
                case SafeZonePosition.Back:
                    _unsafePosition.sizeDelta = new Vector2(_warningPosition.sizeDelta.x, unsafeLength);
                    _warningPosition.sizeDelta = new Vector2(_unsafePosition.sizeDelta.x, warningLength);
                    break;

                case SafeZonePosition.Left:
                case SafeZonePosition.Right:
                    _unsafePosition.sizeDelta = new Vector2(unsafeLength, _warningPosition.sizeDelta.y);
                    _warningPosition.sizeDelta = new Vector2(warningLength, _unsafePosition.sizeDelta.y);
                    break;
            }
        }
    }
}
