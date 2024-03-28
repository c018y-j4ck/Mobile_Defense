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
    /// The Board info class contains the info to retrieve when switching the board, including _scale, unit and the selected board.
    /// </summary>
    [Serializable]
    public class BoardInfo
    {
        /// <summary>
        /// The TiltFive Game board.
        /// </summary>
        [SerializeField] private GameBoard _gameBoard;
        
        /// <summary>
        /// The scale to change to.
        /// </summary>
        [SerializeField] private float _scale = 5f;
        
        /// <summary>
        /// The unit to change to.
        /// </summary>
        [SerializeField] private LengthUnit _lengthUnit = LengthUnit.Centimeters;

        /// <summary>
        /// Encapsulated fields return the fields.
        /// </summary>
        public GameBoard GameBoard => _gameBoard;
        public float Scale => _scale;
        public LengthUnit LengthUnit => _lengthUnit;
    }
}
