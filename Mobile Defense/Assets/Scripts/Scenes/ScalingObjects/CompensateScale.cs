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
using TiltFive;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Attach this class to any game object that you wish to compensate for scale and position.
    /// </summary>
    public class CompensateScale : MonoBehaviour
    {
        /// <summary>
        /// The glasses settings from the tilt five manager in the scene.
        /// </summary>
        private GlassesSettings _glassesSettings;

        /// <summary>
        /// The scale settings from the tilt five manager in the scene.
        /// </summary>
        private ScaleSettings _scaleSettings;

        /// <summary>
        /// The game board settings from the tilt five manager in the scene.
        /// </summary>
        private GameBoardSettings _gameBoardSettings;

        /// <summary>
        /// The original local scale of the object.
        /// </summary>
        private Vector3 _originalScale;
    
        /// <summary>
        /// The original local position of the object.
        /// </summary>
        private Vector3 _originalPosition;

        /// <summary>
        /// Vector3 storing the original scale ratio.
        /// </summary>
        private Vector3 _originalScaleRatio;
    
        /// <summary>
        /// Vector3 storing the original position ratio.
        /// </summary>
        private Vector3 _originalPositionRatio;

        /// <summary>
        /// The previous stored scale in glasses settings.
        /// </summary>
        private float _previousScale;

        private void Awake()
        {
            // Get the tilt five manager in the scene and assign the glasses settings.
            TiltFiveManager2 tiltFiveManager = FindObjectOfType<TiltFiveManager2>();
            _glassesSettings = tiltFiveManager.playerOneSettings.glassesSettings;
            _scaleSettings = tiltFiveManager.playerOneSettings.scaleSettings;
            _gameBoardSettings = tiltFiveManager.playerOneSettings.gameboardSettings;

            // Assign the original scale and position of the transform attached to this GameObject.
            _originalScale = transform.localScale;
            _originalPosition = transform.localPosition;
            
            // At the start, get the original scale ratio by dividing the original scale by the world space units per physical meter value in the glasses settings.
            _originalScaleRatio = new Vector3(_originalScale.x / _scaleSettings.worldSpaceUnitsPerPhysicalMeter,
                _originalScale.y / _scaleSettings.worldSpaceUnitsPerPhysicalMeter,
                _originalScale.z / _scaleSettings.worldSpaceUnitsPerPhysicalMeter);
        
            // Similarly, get the original position ratio by dividing the original position by the world space units per physical meter value in the glasses settings,
            // so that the GameObject stays in the same position even as the board scales.
            _originalPositionRatio = new Vector3(_originalPosition.x / _scaleSettings.worldSpaceUnitsPerPhysicalMeter,
                _originalPosition.y / _scaleSettings.worldSpaceUnitsPerPhysicalMeter,
                _originalPosition.z / _scaleSettings.worldSpaceUnitsPerPhysicalMeter);

            _previousScale = _scaleSettings.worldSpaceUnitsPerPhysicalMeter;
        }

        /// <summary>
        /// Perform the compensation in LateUpdate() to make sure that we're doing it
        /// after any scale change has already been performed.
        /// </summary>
        void LateUpdate()
        {
            ScaleCompensate();
        }

        /// <summary>
        /// Perform the scale compensate when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            ScaleCompensate();
        }

        /// <summary>
        /// Perform the scale compensation.
        /// </summary>
        private void ScaleCompensate()
        {
            // Check if scale has changed and that we're parented to the current game board.
            if (_scaleSettings.worldSpaceUnitsPerPhysicalMeter != _previousScale || _gameBoardSettings.currentGameBoard.transform != transform.parent)
            {
                // If the parent is different from the current TiltFive game board, parent to the new game board.
                if (_gameBoardSettings.currentGameBoard.transform != transform.parent)
                {
                    transform.SetParent(_gameBoardSettings.currentGameBoard.transform, false); // Disable WorldPositionStays so that the object rotates to the new board
                }
                
                // Compensate for the scale change by multiplying the world space units per physical meter value in the glasses settings
                // with the original scale ratio obtained in Start().
                transform.localScale = new Vector3( _scaleSettings.worldSpaceUnitsPerPhysicalMeter * _originalScaleRatio.x,
                    _scaleSettings.worldSpaceUnitsPerPhysicalMeter * _originalScaleRatio.y,
                    _scaleSettings.worldSpaceUnitsPerPhysicalMeter * _originalScaleRatio.z);
        
                // Compensate the position as well for the scale change by multiplying the world space units per physical meter value in the glasses settings
                // with the original position ratio obtained in Start().
                transform.localPosition = new Vector3( _scaleSettings.worldSpaceUnitsPerPhysicalMeter * _originalPositionRatio.x,
                    _scaleSettings.worldSpaceUnitsPerPhysicalMeter * _originalPositionRatio.y,
                    _scaleSettings.worldSpaceUnitsPerPhysicalMeter * _originalPositionRatio.z);

                _previousScale = _scaleSettings.worldSpaceUnitsPerPhysicalMeter;
            }
        }
    }
}
