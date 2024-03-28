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
using Input = UnityEngine.Input;

namespace TiltFiveDemos
{
    /// <summary>
    /// Performs the input for the navigation view (Tilt Five game board)
    /// </summary>
    public class CameraNavigationInput : BaseDemoInput
    {
        [SerializeField]
        private bool _enableTranslation = true;
        [SerializeField]
        private bool _enableRotation = true;
        [SerializeField]
        private bool _enableHeight = true;
        [SerializeField]
        private bool _enableTiltingVertical = true;
        [SerializeField]
        private bool _enableTiltingHorizontal = true;

        /// <summary>
        /// The tilt five board.
        /// </summary>
        [SerializeField] private Transform tiltFiveBoard;

        /// <summary>
        /// The tilt five camera transform, in order to move the board accurately depending on the camera's position.
        /// </summary>
        [SerializeField] private Transform tiltFiveCamera;

        /// <summary>
        /// The translation speed.
        /// </summary>
        [SerializeField] private float translationSpeed = 10f;

        /// <summary>
        /// The rotation speed;
        /// </summary>
        [SerializeField] private float rotationSpeed = 60;

        /// <summary>
        /// The tilting speed;
        /// </summary>
        [SerializeField] private float tiltingSpeed = 60;

        /// <summary>
        /// The height speed;
        /// </summary>
        [SerializeField] private float heightSpeed = 10f;

        [SerializeField]
        private bool _clampTranslation = false;

        [SerializeField]
        private Vector3 _minNavigation = Vector3.zero;

        [SerializeField]
        private Vector3 _maxNavigation = Vector3.zero;

        [SerializeField]
        private bool _restrictTranslationX = false;

        [SerializeField]
        private bool _restrictTranslationY = false;

        /// <summary>
        /// The translation value.
        /// </summary>
        private Vector2 _translateValue;

        /// <summary>
        /// The rotation value.
        /// </summary>
        private float _rotateValue;

        /// <summary>
        /// The tilting value.
        /// </summary>
        private Vector2 _tiltValue;

        /// <summary>
        /// The height value.
        /// </summary>
        private float _heightValue;

        // Since we're not using the Input Manager for input with the TiltFive Wand, the Wand Input conflicts with the requested Input axes.
        // These flags are used to resolve those issues.

        /// <summary>
        /// Flag to check if we're using the T5 Wand for translation.
        /// </summary>
        private bool _translateT5Wand = false;
        /// <summary>
        /// Flag to check if we're using the T5 Wand for height.
        /// </summary>
        private bool _heightT5Wand = false;
        /// <summary>
        /// Flag to check if we're using the T5 Wand for rotation.
        /// </summary>
        private bool _rotateT5Wand = false;
        /// <summary>
        /// Flag to check if we're using the T5 Wand for tilting.
        /// </summary>
        private bool _tiltT5Wand = false;

        /// <summary>
        /// The original container position.
        /// </summary>
        private Vector3 _originalPosition;

        /// <summary>
        /// The original container rotation.
        /// </summary>
        private Quaternion _originalRotation;

        private void Start()
        {
            // Store the original rotation and position on the start.
            _originalPosition = tiltFiveBoard.position;
            _originalRotation = tiltFiveBoard.rotation;
        }

        // Update is called once per frame
        private void Update()
        {
            // Return if we're showing the info notice with instructions.
            if (!_active) return;

            // Check all inputs
            CheckTranslateInput();
            CheckRotateInput();
            CheckTiltInput();
            CheckHeightInput(); 

            // Perform the actions
            DoTranslate();
            DoRotate();
            DoTilt();
            DoHeight();

            CheckReset();
        }

        /// <summary>
        /// Checks the rotate input.
        /// </summary>
        private void CheckRotateInput()
        {
            if (_rotateT5Wand) return;
            _rotateValue = Input.GetAxis("RotateBoard");
        }

        /// <summary>
        /// Rotate with the T5 Wand to the right.
        /// </summary>
        /// <param name="pValue"></param>
        public void RotateT5WandRight()
        {
            _rotateT5Wand = true;
            _rotateValue = 1f;
        }

        /// <summary>
        /// Rotate with the T5 Wand to the left.
        /// </summary>
        /// <param name="pValue"></param>
        public void RotateT5WandLeft()
        {
            _rotateT5Wand = true;
            _rotateValue = -1f;
        }

        /// <summary>
        /// Stop rotating with the T5 Wand
        /// </summary>
        public void StopRotateT5Wand()
        {
            _rotateT5Wand = false;
            _rotateValue = 0;
        }

        /// <summary>
        /// Checks the tilt input.
        /// </summary>
        private void CheckTiltInput()
        {
            if (_tiltT5Wand) return;
            _tiltValue = new Vector2(Input.GetAxis("TiltBoardSides"),Input.GetAxis("TiltBoardForward"));

            if (_tiltValue.magnitude < 0.25f) _tiltValue = Vector2.zero;

        }

        /// <summary>
        /// Check for the reset input.
        /// </summary>
        private void CheckReset()
        {
            if (Input.GetButtonDown("NavigationReset")) ResetBoardPosition();
        }

        /// <summary>
        /// Checks the tilt input from Wand to the right.
        /// </summary>
        public void SetTiltValueT5Right()
        {
            _tiltT5Wand = true;
            _tiltValue = new Vector2(1, _tiltValue.y);
        }
        /// <summary>
        /// Checks the tilt input from Wand to the left.
        /// </summary>
        public void SetTiltValueT5Left()
        {
            _tiltT5Wand = true;
            _tiltValue = new Vector2(-1, _tiltValue.y);
        }
        /// <summary>
        /// Checks the tilt input from Wand to the front.
        /// </summary>
        public void SetTiltValueT5Front()
        {
            _tiltT5Wand = true;
            _tiltValue = new Vector2(_tiltValue.x,1);
        }
        /// <summary>
        /// Checks the tilt input from Wand to the back.
        /// </summary>
        public void SetTiltValueT5Back()
        {
            _tiltT5Wand = true;
            _tiltValue = new Vector2(_tiltValue.x, -1);
        }
        /// <summary>
        /// Stops the tilting input from wand.
        /// </summary>
        public void StopT5Tilt()
        {
            _tiltT5Wand = false;
            _tiltValue = Vector2.zero;
        }

        /// <summary>
        /// Checks the translate input.
        /// </summary>
        private void CheckTranslateInput()
        {
            if (_translateT5Wand) return;
           
            _translateValue = new Vector2(-Input.GetAxis("BoardTranslateSides"), -Input.GetAxis("BoardTranslateForward"));

            if (_translateValue.magnitude < 0.25f) _translateValue = Vector2.zero;
        }

        /// <summary>
        /// Set translate input for wand.
        /// </summary>
        /// <param name="pTranslateValue"></param>
        public void SetT5TranslateInput(Vector2 pTranslateValue)
        {
            Debug.Log("Moving: "+pTranslateValue);
            _translateT5Wand = true;
            _translateValue = new Vector2(-pTranslateValue.x, pTranslateValue.y);
        }

        /// <summary>
        /// Stop translate input for wand.
        /// </summary>
        /// <param name="pTranslateValue"></param>
        public void StopT5TranslateInput()
        {
            _translateT5Wand = false;
            _translateValue = Vector2.zero;
        }

        /// <summary>
        /// Checks the height input.
        /// </summary>
        private void CheckHeightInput()
        {
            if (_heightT5Wand) return;

            // We need to use platform dependent compilation since gamepad triggers are different between platforms.
#if UNITY_STANDALONE_WIN
            _heightValue = Input.GetAxis("RaiseBoard");
#elif UNITY_STANDALONE_LINUX
            _heightValue = -(Input.GetAxis("RaiseBoardDown"))+(Input.GetAxis("RaiseBoardUp"));
#endif
        }

        /// <summary>
        /// Set the height input from T5 wand down.
        /// </summary>
        public void SetHeightT5Down()
        {
            _heightT5Wand = true;
            _heightValue = -1;
        }

        /// <summary>
        /// Set the height input from T5 wand up.
        /// </summary>
        public void SetHeightT5Up()
        {
            _heightT5Wand = true;
            _heightValue = 1;
        }

        /// <summary>
        /// Stops the height input from T5 wand.
        /// </summary>
        public void StopHeightT5Input()
        {
            _heightT5Wand = false;
            _heightValue = 0;
        }
        
        /// <summary>
        /// Stop all the T5 input (when we disconnect the wand, for example)
        /// </summary>
        public override void StopAllT5Input()
        {
            StopHeightT5Input(); 
            StopT5TranslateInput(); 
            StopT5Tilt(); 
            StopRotateT5Wand();
            base.StopAllT5Input();
        }

        /// <summary>
        /// Perform the translation using the value received in Input.
        /// </summary>
        private void DoTranslate()
        {
            // Check that the magnitude of the translation vector is higher than 0.
            if (_translateValue.magnitude > 0f && _enableTranslation)
            {
                float translateValueX = _translateValue.x;
                float translateValueY = _translateValue.y;

                if(_restrictTranslationX) translateValueX = 0f;
                
                if(_restrictTranslationY) translateValueY = 0f;

                // Invert X axis and use Y value for the Z axis
                Vector3 translateAdjust = new Vector3(-translateValueX, 0f, translateValueY);

                float previousY = tiltFiveBoard.localPosition.y; // Save the previous height
                float previousX = tiltFiveBoard.localPosition.x; // Save the previous height
                float previousZ = tiltFiveBoard.localPosition.z; // Save the previous height

                // Move the container with transform.Translate().
                tiltFiveBoard.Translate(translateAdjust.normalized * translationSpeed * Time.deltaTime, Space.Self);

                // We don't want our height to change, so we simply reset it to the previous one.

                if(_clampTranslation)
                {
                    tiltFiveBoard.localPosition = new Vector3(Mathf.Clamp(tiltFiveBoard.localPosition.x, _minNavigation.x, _maxNavigation.x), previousY, Mathf.Clamp(tiltFiveBoard.localPosition.z, _minNavigation.z, _maxNavigation.z));
                }
                else
                {
                    tiltFiveBoard.localPosition = new Vector3(tiltFiveBoard.localPosition.x, previousY, tiltFiveBoard.localPosition.z);
                }
            } 
        }

        /// <summary>
        /// Perform the rotation using the value received in Input.
        /// </summary>
        private void DoRotate()
        {
            // Check that the value of rotation is different from 0.
            if (_rotateValue != 0f && _enableRotation)
            {
                Vector3 newRotation = new Vector3(0f, _rotateValue, 0f); // Apply rotation the the Z axis

                tiltFiveBoard.Rotate(newRotation.normalized * rotationSpeed * Time.deltaTime, Space.World); // Rotate in world space
            }
        }

        /// <summary>
        /// Perform the rotation value received in Input.
        /// </summary>
        private void DoTilt()
        {
            // Check that the magnitude of the tilt vector is higher than 0.
            if (_tiltValue.magnitude > 0f)
            {
                // Convert the tilt input to vector3, separately so that the rotation can be performed
                // in different axis spaces.
                Vector3 tiltForward = new Vector3(_tiltValue.y, 0f,0f);
                Vector3 tiltHorizontal = new Vector3(0f, 0f, -_tiltValue.x);

                if (!_enableTiltingHorizontal) tiltHorizontal = Vector3.zero;
                if (!_enableTiltingVertical) tiltForward = Vector3.zero;

                tiltFiveBoard.Rotate(tiltForward.normalized * tiltingSpeed * Time.deltaTime, Space.Self); // Use self space for board forwards and backwards rotation.
                tiltFiveBoard.Rotate(tiltHorizontal.normalized * tiltingSpeed * Time.deltaTime, Space.Self); // Use self space for board sides rotation.
            }
        }
        
        /// <summary>
        /// Perform the height movement using the flag and value received in Input.
        /// </summary>
        private void DoHeight()
        {
            // Check that the value of height is higher than 0.
            if (_heightValue != 0f && _enableHeight)
            {
                // Use the axis input value for the Y axis
                Vector3 heightAdjust = new Vector3(0f,_heightValue, 0f);

                // Raise or lower the board with transform.Translate().
                tiltFiveBoard.Translate(heightAdjust.normalized * heightSpeed * Time.deltaTime, Space.World);
            }
        }

        /// <summary>
        /// Reset the board position and rotation
        /// </summary>
        public void ResetBoardPosition()
        {
            tiltFiveBoard.position = _originalPosition;
            tiltFiveBoard.rotation = _originalRotation;
        }
    }
}
