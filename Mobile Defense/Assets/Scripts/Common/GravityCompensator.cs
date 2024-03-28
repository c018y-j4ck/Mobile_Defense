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
using TiltFive;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Compensates the gravity scale to the scale of the scene as seen through the board
    /// So that objects that appear to be 1 meter fall as they would in the real world.
    /// </summary>
    public class GravityCompensator : MonoBehaviour
    {
        [SerializeField]
        private TiltFiveManager _tiltFiveManager;

        // It may be useful to hold onto the original gravity value.
        private Vector3 originalGravity;

        void Start()
        {
            originalGravity = Physics.gravity;

            // Convenience property for the Content Scale in Unity worldspace units.
            // (e.g. 10cm Content Scale would result in a value of 0.1)
            float gravityScalar = _tiltFiveManager.scaleSettings.physicalMetersPerWorldSpaceUnit;

            // Scaling the GameBoard will also affect gravity. To compensate:
            gravityScalar *= _tiltFiveManager.gameBoardSettings.gameBoardScale;

            // Divide the original gravity by the Content Scale
            // (and perhaps game board scale)
            Physics.gravity = originalGravity / gravityScalar;
        }
    }
}
