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

using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// A stop for scale components that contains the specific scale number.
    /// </summary>
    public class ScaleStop : MonoBehaviour
    {
        /// <summary>
        /// The scale number for the stop.
        /// </summary>
        [SerializeField] private float _scale;
    
        /// <summary>
        /// Encapsulate the scale for access with the ScalingInput class.
        /// </summary>
        public float Scale => _scale;
    }
}
