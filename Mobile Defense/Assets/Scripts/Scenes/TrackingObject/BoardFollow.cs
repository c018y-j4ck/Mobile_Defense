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
    /// Class to make the board follow an object in the scene.
    /// </summary>
    public class BoardFollow : MonoBehaviour
    { 
        /// <summary>
        /// The board transform.
        /// </summary>
        [SerializeField] private Transform boardTransform;
    
        /// <summary>
        /// The object to be followed.
        /// </summary>
        [SerializeField] private Transform followObject;

        /// <summary>
        /// Update this instance.
        /// </summary>
        private void Update()
        {
            TrackFollowObject();
        }

        /// <summary>
        /// Make the board object follow the tracked object's position in space.
        /// </summary>
        private void TrackFollowObject()
        {
            boardTransform.position = followObject.transform.position;
        }
    }
}
