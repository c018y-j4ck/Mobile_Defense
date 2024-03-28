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
    /// Rotates the object with this scripted attached.
    /// </summary>
    public class SimpleRotator : MonoBehaviour
    {
        /// <summary>
        /// The rotation speed.
        /// </summary>
        [SerializeField] private float _rotationSpeed = 25f;

        /// <summary>
        /// Whether direction should be random or not.
        /// </summary>
        [SerializeField] private bool _randomDirection = false;

        /// <summary>
        /// The rotation direction (defaults up, which rotates to the left).
        /// </summary>
        private Vector3 _rotationDirection;

        private void Start()
        {
            _rotationDirection = Vector3.up;

            if (_randomDirection)
            {
                _rotationDirection = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f));
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(_rotationDirection * (Time.deltaTime * _rotationSpeed), Space.World);
        }
    }
}
