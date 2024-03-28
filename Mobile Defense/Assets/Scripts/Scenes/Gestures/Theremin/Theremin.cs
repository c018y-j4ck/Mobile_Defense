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
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// A theremin example that receives gestures from various gesture interactables.
    /// Since communication is done using UnityEvents, it doesn't depend on the type of interactables used in the game.
    /// Value change functions are assigned in the inspector.
    /// </summary>
    public class Theremin : MonoBehaviour
    {
        /// <summary>
        /// The transform of the marker that defines the pitch and volume.
        /// </summary>
        [SerializeField]
        private Transform _markerTransform;

        /// <summary>
        /// The audio source of the theremin.
        /// </summary>
        [SerializeField]
        private AudioSource _audioSource;

        /// <summary>
        /// The transform of the container bounding box of the marker.
        /// </summary>
        [SerializeField]
        private Transform _containerAreaTransform;

        /// <summary>
        /// A low pass filter effect for the audio source.
        /// </summary>
        [SerializeField]
        private AudioLowPassFilter _lowPassFilter;

        /// <summary>
        /// An echo filter for the audio source.
        /// </summary>
        [SerializeField]
        private AudioEchoFilter _echoFilter;

        /// <summary>
        /// A chorus filter effect for the audio source.
        /// </summary>
        [SerializeField]
        private AudioChorusFilter _chorusFilter;

        private void Update()
        {
            CalculateMarkerPosition(_markerTransform.position);
        }

        /// <summary>
        /// Calculate the pitch and volume depending on the position of the marker.
        /// </summary>
        /// <param name="pMarkerPosition"></param>
        private void CalculateMarkerPosition(Vector3 pMarkerPosition)
        {
            Vector3 markerRelativePosition = _containerAreaTransform.transform.InverseTransformPoint(pMarkerPosition);

            float markerX = markerRelativePosition.x + 0.5f;

            float markerY = markerRelativePosition.y + 0.5f;

            // Set the pitch and volume depending on the marker position relative to the bounding box of the area.
            _audioSource.volume = markerX * 0.3f;
            _audioSource.pitch = markerY * 3f;

        }

        /// <summary>
        /// Move the marker on X.
        /// </summary>
        /// <param name="pPosition">The position to move on X</param>
        public void MoveMarkerX(float pPosition)
        {
            _markerTransform.position = new Vector3(_containerAreaTransform.transform.TransformPoint(pPosition - 0.5f, 0f, 0f).x, _markerTransform.position.y, _markerTransform.position.z);
        }

        /// <summary>
        /// Move the marker on Y.
        /// </summary>
        /// <param name="pPosition">The position to move on Y</param>
        public void MoveMarkerY(float pPosition)
        {
            _markerTransform.position = new Vector3(_markerTransform.position.x, _containerAreaTransform.transform.TransformPoint(0f, pPosition - 0.5f, 0f).y, _markerTransform.position.z);
        }

        /// <summary>
        /// Change the low pass cutoff with a change delta.
        /// </summary>
        /// <param name="pChangeDelta">The delta of the change</param>
        public void ChangeLowPassCutoff(float pChangeDelta)
        {
            _lowPassFilter.cutoffFrequency += pChangeDelta * 650f;
        }

        /// <summary>
        /// Change the low pass resonance with a change delta.
        /// </summary>
        /// <param name="pChangeDelta">The delta of the change</param>
        public void ChangeLowPassResonance(float pChangeDelta)
        {
            _lowPassFilter.lowpassResonanceQ = Mathf.Clamp(_lowPassFilter.lowpassResonanceQ + (pChangeDelta * 6f), 1f, 10f);
        }

        /// <summary>
        /// Toggle the audio with a button.
        /// </summary>
        /// <param name="pToggle">The toggle state</param>
        public void ToggleAudio(bool pToggle)
        {
            if (pToggle) _audioSource.Play();
            else _audioSource.Stop();
        }


        /// <summary>
        /// Change the echo delay with a change delta.
        /// </summary>
        /// <param name="pChangeDelta">The delta of the change</param>
        public void ChangeEchoDelay(float pEchoDelay)
        {
            _echoFilter.delay = pEchoDelay * 1500f;
        }


        /// <summary>
        /// Change the chorus delay with a change delta.
        /// </summary>
        /// <param name="pChangeDelta">The delta of the change</param>
        public void ChangeChorusDelay(float pChorusDelay)
        {
            _chorusFilter.delay = pChorusDelay * 0.25f;
        }


        /// <summary>
        /// Change the chorus depth with a change delta.
        /// </summary>
        /// <param name="pChangeDelta">The delta of the change</param>
        public void ChangeChorusDepth(float pChorusDepth)
        {
            _chorusFilter.depth = pChorusDepth;
        }
    }
}
