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
    [ExecuteInEditMode]
    /// <summary>
    /// Rotates the flat canvas object towards the player.
    /// </summary>
    public class UILookAtPlayer : MonoBehaviour
    {
        /// <summary>
        /// The canvas transform.
        /// </summary>
        [SerializeField] private Transform _uITransform;
        
        /// <summary>
        /// The TiltFive camera transform.
        /// </summary>
        [SerializeField] private Transform _cameraTransform;

        /// <summary>
        /// The original canvas rotation on the X axis.
        /// </summary>
        private float _uITransformOriginalRotationX;

        // Start is called before the first frame update
        void Start()
        {
            // Get the original canvas rotation.
            _uITransformOriginalRotationX = _uITransform.rotation.eulerAngles.x;
        }

        // Update is called once per frame
        void Update()
        {
            LookAtPlayer();
        }

        /// <summary>
        /// Look at the player in the flat plane.
        /// </summary>
        private void LookAtPlayer()
        {
            // Get the camera position and the canvas position.
            Vector3 cameraPosition = _cameraTransform.position;
            Vector3 canvasPosition = _uITransform.position;

            // Use the direction from the canvas to the camera to find the look rotation.
            Quaternion lookRotation = Quaternion.LookRotation(canvasPosition - cameraPosition);
            Vector3 lookRotationEuler = lookRotation.eulerAngles;
            
            // Ignore the X axis and use the original one.
            Vector3 goalRotationEuler = new Vector3(_uITransformOriginalRotationX, lookRotationEuler.y, lookRotationEuler.z);
            Quaternion goalRotation = Quaternion.Euler(goalRotationEuler);

            _uITransform.rotation = goalRotation;
        }
    }
}
