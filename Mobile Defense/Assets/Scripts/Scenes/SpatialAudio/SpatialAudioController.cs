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
using TMPro;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// The controller class for the spatial audio demo.
    /// </summary>
    public class SpatialAudioController : MonoBehaviour
    {
        /// <summary>
        /// The camera transform.
        /// </summary>
        [SerializeField]
        private Transform _cameraTransform;

        /// <summary>
        /// The board transform.
        /// </summary>
        [SerializeField]
        private Transform _boardTransform;

        /// <summary>
        /// The audio listener transform.
        /// </summary>
        [SerializeField]
        private Transform _audioListenerTransform;

        /// <summary>
        /// Whether the listener is currently "flat" (at board height.)
        /// </summary>
        [SerializeField]
        private bool _flatListener = false;

        /// <summary>
        /// The text that displays the current of the listener.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _listeningNotification;

        private void Start()
        {
            FixFlat();
        }

        private void Update()
        {
            // Set the listener at the height of the board if the listener is currently flat.
            if(_flatListener)
            {
                _audioListenerTransform.position = new Vector3(_cameraTransform.position.x, _boardTransform.position.y, _cameraTransform.position.z);
                _audioListenerTransform.rotation = _cameraTransform.rotation;
            }

            if (Input.GetButtonDown("ToggleListening")) ToggleFlat();
        }

        /// <summary>
        /// Set the listener at the board height or at the camera height.
        /// Toggles the current state.
        /// </summary>
        public void ToggleFlat()
        {
            _flatListener = !_flatListener;

            FixFlat();
        }

        /// <summary>
        /// Set the listener at the board height or at the camera height
        /// with a specific state.
        /// </summary>
        /// <param name="pFlat"></param>
        public void ToggleFlat(bool pFlat)
        {
            _flatListener = pFlat;

            FixFlat();
        }

        /// <summary>
        /// Change the camera position and UI after toggling flat.
        /// </summary>
        private void FixFlat()
        {
            if (!_flatListener)
            {
                _audioListenerTransform.position = _cameraTransform.position;
                _listeningNotification.text = "Listener: Camera";
            }
            else
            {
                _audioListenerTransform.position = new Vector3(_cameraTransform.position.x, _boardTransform.position.y, _cameraTransform.position.z);
                _listeningNotification.text = "Listener: Board";
            }
        }
    }
}
