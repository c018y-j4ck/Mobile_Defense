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
    /// Class to scale the board using input.
    /// </summary>
    public class ScalingInput : BaseDemoInput
    {
        /// <summary>
        /// The Tilt Five manager in the scene.
        /// </summary>
        [SerializeField] private TiltFiveManager2 _tiltFiveManager;
        
        /// <summary>
        /// The speed of scaling.
        /// </summary>
        [SerializeField] private float _scaleSpeed = 0.75f;
        
        /// <summary>
        /// The scale function power.
        /// </summary>
        [SerializeField] private float _scaleFunctionPow = 0.975f;

        /// <summary>
        /// The minimum objective scaling.
        /// </summary>
        [SerializeField] private float _minScale = -320f;

        /// <summary>
        /// The maximum objective scaling.
        /// </summary>
        [SerializeField] private float _maxScale = 1000f;

        /// <summary>
        /// The board container, so that it can be moved as objects are scaled.
        /// </summary>
        [SerializeField] private Transform _boardContainer;

        /// <summary>
        /// The starting (smallest) object.
        /// </summary>
        [SerializeField] private Transform _startingObject;
        
        /// <summary>
        /// Y offset for the movement exponential function.
        /// </summary>
        [SerializeField] private float _moveYOffsetModifier = 200f;

        /// <summary>
        /// Scale stops in case we want to have stops.
        /// </summary>
        [SerializeField] private ScaleStop[] _scaleStops;

        /// <summary>
        /// The prefab for the scale stop UI.
        /// </summary>
        [SerializeField] private GameObject _scaleStopUIPrefab;

        /// <summary>
        /// The scale stop container for the UI.
        /// </summary>
        [SerializeField] private Transform _scaleStopUIContainer;

        /// <summary>
        /// Speed of scaling between scale stops.
        /// </summary>
        [SerializeField] private float _scaleStopSpeed = 300f;
        
        /// <summary>
        /// The current scale.
        /// </summary>
        private float _currentScale;
        
        /// <summary>
        /// The current input value
        /// </summary>
        private float _scaleInputValue;
        
        /// <summary>
        /// The current input value
        /// </summary>
        private float _smallestPosition;

        /// <summary>
        /// The list of scale stop UI indicators.
        /// </summary>
        private ScaleStopUI[] _scaleStopUIs;

        /// <summary>
        /// The coroutine used to move the scale stop.
        /// Store the coroutine in order to be able to stop it if smooth movement is performed.
        /// </summary>
        private Coroutine _moveToStopScaleCoroutine;

        /// <summary>
        /// Flag to check if we're using the wand, to account for conflicts with the Input Manager.
        /// </summary>
        private bool _scalingT5Wand = false;

        // Start is called before the first frame update
        void Start()
        {
            // Get the position of the smallest object to use with our movement function.
            _smallestPosition = _startingObject.position.x;
            
            // Get the starting scale based on the current view set in the Tilt Five parameters.
            _currentScale = GetCurrentScale();

            // Perform the initial scale and movement.
            DoScale();
            DoMovement();
            
            // Generate the stops UI
            GenerateStopsUI();
                
            // Select the first stop.
            SelectFirstStop();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_active) return;

            // Check inputs
            CheckStopsInput();
            CheckSmoothScalingInput();

            // Perform smooth scaling.
            DoSmoothScaling();
        }

        /// <summary>
        /// Checks the smooth scaling input.
        /// </summary>
        private void CheckSmoothScalingInput()
        {
            if (_scalingT5Wand) return;
            _scaleInputValue = Input.GetAxis("ScalingSmooth");
        }
        
        /// <summary>
        /// Smooth scaling with the T5 Wand.
        /// </summary>
        /// <param name="pInput"></param>
        public void T5SmoothScaling(Vector2 pInput)
        {
            _scalingT5Wand = true;
            _scaleInputValue = pInput.x;
        }

        /// <summary>
        /// Stop smooth scaling with the T5 wand.
        /// </summary>
        public void StopT5SmoothScaling()
        {
            _scalingT5Wand = false; 
            _scaleInputValue = 0f;
        }

        /// <summary>
        /// Checks the stops input.
        /// </summary>
        private void CheckStopsInput()
        {
            if (Input.GetButtonDown("ScalingNextStop"))
            {
                NextScaleStop();
                
                return;
            }
            
            if (Input.GetButtonDown("ScalingBackStop"))
            {
                PreviousScaleStop();
                
                return;
            }
        }

        /// <summary>
        /// Stop all the T5 input.
        /// </summary>
        public override void StopAllT5Input()
        {
            StopT5SmoothScaling();
            base.StopAllT5Input();
        }


        /// <summary>
        /// Dynamically generate the UI objects for the stops if the stops array has stops.
        /// </summary>
        private void GenerateStopsUI()
        {
            if (_scaleStops.Length <= 0 || _scaleStopUIContainer == null) return;
            
            // Create the new array for the stops UI elements
            _scaleStopUIs = new ScaleStopUI[_scaleStops.Length];
                
            // Instantiate the stop UI element for each scale stop and add it to the array.
            for (int i = 0; i < _scaleStops.Length; i++)
            {
                GameObject scaleStopUIObject = Instantiate(_scaleStopUIPrefab, _scaleStopUIContainer);
                ScaleStopUI scaleStopUI = scaleStopUIObject.GetComponent<ScaleStopUI>();
                _scaleStopUIs[i] = scaleStopUI;
            }
        }

        /// <summary>
        /// Select the very first scale if the stops array has defined stops.
        /// </summary>
        private void SelectFirstStop()
        {
            if (_scaleStops.Length <= 0) return;
                
            SelectScale(_scaleStops[0].Scale);
            SelectStopUI(0);
        }

        /// <summary>
        /// Select the stop and move to it.
        /// </summary>
        /// <param name="pStop">The scale stop index</param>
        private void SelectStop(int pStop)
        {
            if (_scaleStops.Length > pStop)
            {
                MoveToStopScale(pStop,_scaleStops[pStop].Scale);
            }
        }

        /// <summary>
        /// Move to the stop scale.
        /// </summary>
        /// <param name="pStop">The stop index</param>
        /// <param name="pScaleTarget">The scale stop target.</param>
        private void MoveToStopScale(int pStop, float pScaleTarget)
        {
            // Don't do the coroutine if it's currently happening.
            if (_moveToStopScaleCoroutine != null) return;
            
            // Set the coroutine to the coroutine variable, so that it can be disabled if the player inputs the stop again.
            _moveToStopScaleCoroutine = StartCoroutine(MoveToStopScaleCoroutine(pScaleTarget));
            SelectStopUI(pStop);
        }

        /// <summary>
        /// Lerp from one scale to the other using a coroutine
        /// </summary>
        /// <param name="pScaleTarget">The scale stop target.</param>
        /// <returns></returns>
        private IEnumerator MoveToStopScaleCoroutine(float pScaleTarget)
        {
            float currentScale = _currentScale;

            float normal = 0;

            while (normal < 1f)
            {
                normal += Time.deltaTime * _scaleStopSpeed;

                float scale = Mathf.Lerp(currentScale,pScaleTarget, normal);
                
                SelectScale(scale);
                
                yield return null;
            }

            // Set the coroutine as null.
            _moveToStopScaleCoroutine = null;
        }

        /// <summary>
        /// Completely stops the move to scale stop coroutine.
        /// </summary>
        private void StopMoveToStopScaleCoroutine()
        {
            if (_moveToStopScaleCoroutine != null)
            {
                StopCoroutine(_moveToStopScaleCoroutine);
                _moveToStopScaleCoroutine = null;
            }
        }

        /// <summary>
        /// Deactivate all other stops and select the current stop in the UI.
        /// </summary>
        /// <param name="pStop"></param>
        private void SelectStopUI(int pStop)
        {
            if (_scaleStopUIs.Length > 0)
            {
                foreach (ScaleStopUI scaleStopUI in _scaleStopUIs)
                {
                    scaleStopUI.Deactivate();
                }

                if (_scaleStopUIs.Length > pStop)
                {
                    _scaleStopUIs[pStop].Activate();
                }
            }
        }

        /// <summary>
        /// Get the starting scale for our exponential function based on the current scale ratio.
        /// </summary>
        /// <returns>The current scale</returns>
        private float GetCurrentScale()
        {
            float result = Mathf.Clamp(Mathf.Log(_tiltFiveManager.playerOneSettings.scaleSettings.contentScaleRatio, _scaleFunctionPow),_minScale,_maxScale);
            return result;
        }

        /// <summary>
        /// Immediately select the received scale.
        /// </summary>
        /// <param name="pScale"></param>
        private void SelectScale(float pScale)
        {
            _currentScale = pScale;
            
            DoScale();
            DoMovement();
        }

        /// <summary>
        /// Perform the smooth scaling.
        /// </summary>
        private void DoSmoothScaling()
        {
            // Perform the update if our input value is different from 0.
            if (_scaleInputValue != 0f)
            {
                StopMoveToStopScaleCoroutine();
                
                // Multiply the input value by the speed of scaling.
                float scaleChange = _scaleInputValue * _scaleSpeed * Time.deltaTime;
                
                // Change our scale value with the value received from the input, and move modifier clamp it to the maximum and minimum scale.
                _currentScale = Mathf.Clamp(_currentScale + scaleChange,_minScale,_maxScale);
                
                DoScale();
                DoMovement();
                
                SelectStopUI(GetClosestStop());
            }
        }
        
        /// <summary>
        /// Perform the scaling.
        /// </summary>
        private void DoScale()
        {
            // Get the new scale with GetNewScale().
            // and assign the changed scale ratio to the glasses settings in the Tilt Five Manager Object.
            var newScale = GetNewScale(_currentScale);
            _tiltFiveManager.playerOneSettings.scaleSettings.contentScaleRatio = newScale;
            _tiltFiveManager.playerTwoSettings.scaleSettings.contentScaleRatio = newScale;
            _tiltFiveManager.playerThreeSettings.scaleSettings.contentScaleRatio = newScale;
            _tiltFiveManager.playerFourSettings.scaleSettings.contentScaleRatio = newScale;
        }

        /// <summary>
        /// Perform the movement.
        /// </summary>
        private void DoMovement()
        {
            // Get the board's position.
            var position = _boardContainer.position;

            // Get the new position with the function GetNewPosition().
            position = new Vector3(
                GetNewPosition(_currentScale),
                position.y,
                position.z);
                
            _boardContainer.position = position;
        }
        
        /// <summary>
        /// Get the current scale ratio based on the change to our scale.
        /// We can use an increasing exponential function to modify the rate of change depending on the current scale ratio.
        /// i.e. If our scale ratio is a small number, we want it to change at that rate, and
        /// if our scale ratio is a very large number, we want it to change at that same rate,
        /// so that objects on the screen appear to scale at roughly the same speed.
        /// </summary>
        /// <param name="pScale"></param>
        /// <returns>The current scale ratio</returns>
        private float GetNewScale(float pScale)
        {
            float scaleChange = Mathf.Pow(_scaleFunctionPow, pScale);
            
            return scaleChange;
        }

        /// <summary>
        /// Get the new position to move the board using a decreasing exponential function:
        /// i.e. The smaller the current scale is, the faster the board moves. Ideally, we use the same ratio as
        /// in the scaling function, with a move modifier that defines at which point should the board start to pick up.
        /// </summary>
        /// <param name="pScale"></param>
        /// <returns></returns>
        private float GetNewPosition(float pScale)
        {
            // Perform decreasing exponential function.
            float positionChange = Mathf.Pow(_scaleFunctionPow, -(pScale - _moveYOffsetModifier)) + _smallestPosition;
            
            return positionChange;
        }

        /// <summary>
        /// Select the next scale stop.
        /// </summary>
        public void NextScaleStop()
        {
            if (_scaleStops.Length == 0) return; // return if there are no scale stops.
            
            int closestStop = GetClosestStop();
            
            // If the current scale is higher than the scale of the closest stop, then move to the next one after the closest one.
            // Otherwise, select the closest one.
            if (_currentScale >= _scaleStops[closestStop].Scale)
            {
                if (closestStop + 1 < _scaleStops.Length) // make sure that the selection is in the array.
                {
                    SelectStop(closestStop + 1);
                }
                else
                {
                    // Wrap to the first one if we're on the last one.
                    SelectStop(0);
                }
            }
            else
            {
                SelectStop(closestStop);
            }
        }

        /// <summary>
        /// Select the previous scale stop.
        /// </summary>
        public void PreviousScaleStop()
        {
            if (_scaleStops.Length == 0) return; // return if there are no scale stops.
            
            int closestStop = GetClosestStop();

            // If the current scale is less than the scale of the closest stop, then move to the previous after the closest one.
            // Otherwise, select the closest one.
            if (_currentScale <= _scaleStops[closestStop].Scale) // make sure that the selection is in the array.
            {
                if (closestStop - 1 >= 0)
                {
                    SelectStop(closestStop - 1);
                }
                else
                {
                    // Wrap to the last one if we're on the first one.
                    SelectStop(_scaleStops.Length - 1);
                }
            }
            else
            {
                SelectStop(closestStop);
            }
        }

        /// <summary>
        /// Returns the closest scale stop.
        /// </summary>
        /// <returns>The closest scale stop</returns>
        private int GetClosestStop()
        {
            int closestStop = 0;

            float previousClosest = Mathf.Abs(_scaleStops[closestStop].Scale - _currentScale);

            // Loop through all the scale stops and find the one closest to the current position.
            for (int i = 0; i < _scaleStops.Length; i++)
            {
                if (Mathf.Abs(_scaleStops[i].Scale - _currentScale) < previousClosest)
                {
                    closestStop = i;
                    previousClosest = Mathf.Abs(_scaleStops[i].Scale - _currentScale);
                }
            }

            return closestStop;
        }
    }
}
