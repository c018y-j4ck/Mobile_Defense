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
using TMPro;
using UnityEngine;


namespace TiltFiveDemos
{
    /// <summary>
    /// An simple class that continuously measures the player's distance to the object, 
    /// and assigns the correct distance to a material that changes depending on distance.
    /// </summary>
    public class CloseObject : MonoBehaviour
    {
        /// <summary>
        /// The text with the distance.
        /// </summary>
        [SerializeField]
        private TextMeshPro _text;

        /// <summary>
        /// The distance at which the player is too close to the object.
        /// </summary>
        [SerializeField]
        private float _closeDistance = 3f;

        /// <summary>
        /// The distance from the player to the object that is safe and comfortable for the player.
        /// </summary>
        [SerializeField]
        private float _safeDistance = 9f;

        /// <summary>
        /// The mesh renderer of the object. renderer.
        /// </summary>
        [SerializeField]
        private MeshRenderer _renderer;

        /// <summary>
        /// The tilt five manager.
        /// </summary>
        private TiltFiveManager2 _tiltFiveManager;

        /// <summary>
        /// The transform of the glasses position in the environment.
        /// </summary>
        private Transform _glassesTransform;

        // Start is called before the first frame update
        void Start()
        {
            // Get the Tilt Five components.
            _tiltFiveManager = FindObjectOfType<TiltFiveManager2>();
            _glassesTransform = GameObject.FindGameObjectWithTag("Glasses").transform;

            // Assign the correct distances to the material.
            _renderer.material.SetFloat("_MinDistanceColor", _closeDistance);
            _renderer.material.SetFloat("_MaxDistanceColor", _safeDistance);
        }

        // Update is called once per frame
        void Update()
        {
            // Get the distance from the glasses to the object.
            float distance = Vector3.Distance(_glassesTransform.position, transform.position);

            // Convert the distance to unit meters
            float fixedDistance = (_tiltFiveManager.playerOneSettings.scaleSettings.oneUnitLengthInMeters * distance) * 100f;

            // Display the distance in the text and display a warning depending on the distance.
            _text.text = $"Distance: {fixedDistance.ToString("#.##")} cm";

            if (distance > _safeDistance)
            {
                _text.text += "\nSafe distance";
            }
            else if (distance < _closeDistance)
            {
                _text.text += "\nToo close!";
            }
            else
            {
                _text.text += "\nWarning!";
            }
        }
    }
}
