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
using TiltFive;
using UnityEngine;
using Input = UnityEngine.Input;

namespace TiltFiveDemos
{
    /// <summary>
    /// A simple class that reads the board state at the start and resets it.
    /// </summary>
    public class ResetBoard : MonoBehaviour
    {
        [SerializeField]
        private Transform _boardTransform;

        [SerializeField]
        private bool _resetPosition = true;
        [SerializeField]
        private bool _resetScale = true;
        [SerializeField]
        private bool _resetRotation = true;

        private Vector3 _originalPosition;
        private Vector3 _originalScale;
        private Quaternion _originalRotation;

        private void Start()
        {
            _originalPosition = _boardTransform.position;
            _originalScale = _boardTransform.localScale;
            _originalRotation = _boardTransform.rotation;
        }

        public void DoReset()
        {
            if(_resetPosition) _boardTransform.position = _originalPosition;
            if(_resetScale) _boardTransform.localScale = _originalScale;
            if(_resetRotation) _boardTransform.localRotation = _originalRotation;
        }
    }
}
