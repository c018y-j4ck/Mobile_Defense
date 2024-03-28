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
using TiltFive;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Visualize the orientation thresholds of the board and the current orientation by taking the values from the AutoOrientation class.
    /// </summary>
    public class AutoOrientationVisualization : MonoBehaviour
    {
        /// <summary>
        /// The list of possible positions.
        /// </summary>
        private enum Positions
        {
            None,
            Front,
            Back,
            Right,
            Left
        }

        /// <summary>
        /// A pair of sprites representing a specific side.
        /// </summary>
        [Serializable]
        private struct VisualizationSpritePair
        {
            /// <summary>
            /// A pair of sprites that represent this side.
            /// </summary>
            [SerializeField]
            private SpriteRenderer[] _spriteRenderer;

            /// <summary>
            /// The position of this side.
            /// </summary>
            [SerializeField]
            private Positions _position;

            // Encapsulate the fields
            public SpriteRenderer[] SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
            public Positions Position { get => _position; set => _position = value; }
        }

        /// <summary>
        /// The orientation manager.
        /// </summary>
        [SerializeField]
        private AutoOrientation _orientationManager;

        /// <summary>
        /// The list of sprite pair
        /// </summary>
        [SerializeField]
        VisualizationSpritePair[] _spritePairs;

        /// <summary>
        /// The color when the side is active.
        /// </summary>
        [SerializeField]
        private Color _activeColor;

        /// <summary>
        /// The color when the side is inactive.
        /// </summary>
        [SerializeField]
        private Color _inactiveColor;

        [SerializeField]
        private Transform _playerDirectionLine;

        /// <summary>
        /// The current position.
        /// </summary>
        private Positions _currentPosition = Positions.None;

        /// <summary>
        /// The deadzone of the direction.
        /// </summary>
        private float _directionDeadzone;

        /// <summary>
        /// The deadzone of the dot product between the camera view and the board.
        /// </summary>
        private float _dotDeadzone;

        /// <summary>
        /// Gather the initial values from the AutoOrientation class.
        /// </summary>
        private void Start()
        {
            SetAllVisualizationInactive();
            _directionDeadzone = _orientationManager.DirectionDeadzone;
            _dotDeadzone = _orientationManager.DotDeadzone;
        }

        /// <summary>
        /// Find which position the player is currently on, and enable those sprites.
        /// </summary>
        private void Update()
        {
            float _currentDot = _orientationManager.CurrentDot;

            _playerDirectionLine.transform.rotation = Quaternion.LookRotation(_orientationManager.CurrentViewDirection, _playerDirectionLine.transform.up);

            // Make sure the dot product is higher than the desired deadzone.
            if (_currentDot < _dotDeadzone) return;

            Vector3 currentViewDirection = _orientationManager.CurrentViewDirection;

            Positions newPosition = Positions.None;

            if (currentViewDirection.z > _directionDeadzone && currentViewDirection.z > Mathf.Abs(currentViewDirection.x)) // Front
            {
                newPosition = Positions.Front;
            }
            else if (currentViewDirection.z < -_directionDeadzone && currentViewDirection.z < -Mathf.Abs(currentViewDirection.x)) // Back
            {
                newPosition = Positions.Back;
            }
            else if (currentViewDirection.x > _directionDeadzone && currentViewDirection.x > Mathf.Abs(currentViewDirection.z)) // Right
            {
                newPosition = Positions.Right;
            }
            else if (currentViewDirection.x < -_directionDeadzone && currentViewDirection.x < -Mathf.Abs(currentViewDirection.z)) // Left
            {
                newPosition = Positions.Left;
            }

            if (newPosition != _currentPosition)
            {
                _currentPosition = newPosition;
                SetAllVisualizationInactive();
                SetVisualizationPairActive(_currentPosition);
            }
        }

        /// <summary>
        /// Disable all visualization sprites.
        /// </summary>
        private void SetAllVisualizationInactive()
        {
            foreach(VisualizationSpritePair visualPair in _spritePairs)
            {
                foreach(SpriteRenderer renderer in visualPair.SpriteRenderer)
                {
                    renderer.color = _inactiveColor;
                }
            }
        }

        /// <summary>
        /// Enable a visualization sprite with a set position.
        /// </summary>
        /// <param name="pPosition">The position to enable</param>
        private void SetVisualizationPairActive(Positions pPosition)
        {
            foreach (VisualizationSpritePair visualPair in _spritePairs)
            {
                if(visualPair.Position == pPosition)
                {
                    foreach (SpriteRenderer renderer in visualPair.SpriteRenderer)
                    {
                        renderer.color = _activeColor;
                    }
                    break;
                }
            }
        }
    }
}
