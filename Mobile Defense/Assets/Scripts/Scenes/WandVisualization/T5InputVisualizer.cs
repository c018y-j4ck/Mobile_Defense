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
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// The Input visualizer class shows the buttons on the visualization lighting up when the events are triggered.
    /// </summary>
    public class T5InputVisualizer : MonoBehaviour
    {

        private enum WandHand
        {
            Right,
            Left,
        }

        /// <summary>
        /// Which hand's wand this visualizer is handling
        /// </summary>
        [SerializeField]
        private WandHand _wandHand;

        /// <summary>
        /// The normal material for released controller buttons.
        /// </summary>
        [SerializeField]
        private Material _normalMaterial;

        /// <summary>
        /// The selected material for pressed controller buttons.
        /// </summary>
        [SerializeField]
        private Material _selectedMaterial;

        /// <summary>
        /// Container for the wand object and all labels, so that we can apply transformations outside the wand object.
        /// </summary>
        [SerializeField]
        private Transform _wandContainer;

        /// <summary>
        /// The transform of the wand controller.
        /// </summary>
        [SerializeField]
        private Transform _wandTransform;

        /// <summary>
        /// The button A mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _aButton;

        /// <summary>
        /// The a label.
        /// </summary>
        [SerializeField]
        private GameObject _aLabel;

        /// <summary>
        /// The button B mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _bButton;

        /// <summary>
        /// The b label.
        /// </summary>
        [SerializeField]
        private GameObject _bLabel;

        /// <summary>
        /// The button X mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _xButton;

        /// <summary>
        /// The x label.
        /// </summary>
        [SerializeField]
        private GameObject _xLabel;

        /// <summary>
        /// The button Y mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _yButton;

        /// <summary>
        /// The y label.
        /// </summary>
        [SerializeField]
        private GameObject _yLabel;

        /// <summary>
        /// The button 1 mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _oneButton;

        /// <summary>
        /// The one label.
        /// </summary>
        [SerializeField]
        private GameObject _oneLabel;

        /// <summary>
        /// The button 2 mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _twoButton;

        /// <summary>
        /// The two label.
        /// </summary>
        [SerializeField]
        private GameObject _twoLabel;

        /// <summary>
        /// The button 'Tilt Five' mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _tiltFiveButton;

        /// <summary>
        /// The system label.
        /// </summary>
        [SerializeField]
        private GameObject _tiltFiveLabel;

        /// <summary>
        /// The trigger mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _trigger;

        /// <summary>
        /// The trigger label.
        /// </summary>
        [SerializeField]
        private GameObject _triggerLabel;

        /// <summary>
        /// The joystick mesh in the visualization.
        /// </summary>
        [SerializeField]
        private GameObject _joystick;

        /// <summary>
        /// The joystick label.
        /// </summary>
        [SerializeField]
        private GameObject _joystickLabel;

        /// <summary>
        /// The joystick press label.
        /// </summary>
        [SerializeField]
        private GameObject _joystickPressLabel;

        /// <summary>
        /// The container with all the labels.
        /// </summary>
        [SerializeField]
        private Transform _labelsContainer;

        /// <summary>
        /// The image showing the current wand status.
        /// </summary>
        [SerializeField]
        private Image _wandAvailableImage;

        /// <summary>
        /// The text with the current wand status.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _wandAvailableText;

        /// <summary>
        /// The color to show on the image that displays the wand status when the wand is available.
        /// </summary>
        [SerializeField]
        private Color _wandAvailableColor;

        /// <summary>
        /// The color to show on the image that displays the wand status when the wand is unavailable.
        /// </summary>
        [SerializeField]
        private Color _wandUnavailableColor;

        /// <summary>
        /// The minimum rotation of the trigger (On the X axis in the model)
        /// </summary>
        [SerializeField]
        private float _triggerMinRotation = 3;
        /// <summary>
        /// The maximum rotation of the trigger (On the X axis in the model)
        /// </summary>
        [SerializeField]
        private float _triggerMaxRotation = -20;

        /// <summary>
        /// The maximum rotation of the stick on the X axis
        /// </summary>
        [SerializeField]
        private float _stickMaxRotationX = 10;
        /// <summary>
        /// The maximum rotation of the stick on the Y axis
        /// </summary>
        [SerializeField]
        private float _stickMaxRotationY = -10;

        /// <summary>
        /// The amount that the button is lowered when pressed.
        /// </summary>
        [SerializeField]
        private float _buttonPressAmount = 0.025f;

        /// <summary>
        /// The current value of the trigger press.
        /// </summary>
        private float _triggerValue = 0f;

        /// <summary>
        /// The current value of the joystick movement.
        /// </summary>
        private Vector2 _joystickValue = Vector2.zero;

        /// <summary>
        /// The current value of the wand position.
        /// </summary>
        private Vector3 _wandPositionValue = Vector3.zero;

        /// <summary>
        /// The current value of the wand rotation.
        /// </summary>
        private Quaternion _wandRotationValue = Quaternion.identity;

        // Encapsulate these two values.
        public float TriggerValue { get => _triggerValue; set => _triggerValue = value; }
        public Vector2 JoystickValue { get => _joystickValue; set => _joystickValue = value; }
        public Quaternion WandRotationValue { get => _wandRotationValue; set => _wandRotationValue = value; }
        public Vector3 WandPositionValue { get => _wandPositionValue; set => _wandPositionValue = value; }

        private TiltFive.ControllerIndex ControllerIndex()
        {
            switch (_wandHand)
            {
                case WandHand.Right:
                    return TiltFive.ControllerIndex.Right;
                case WandHand.Left:
                    return TiltFive.ControllerIndex.Left;
            }
            throw new System.ArgumentException($"Invalid wand hand: {_wandHand}");
        }

        private void Start()
        {
            DisableAllLabels();
        }

        private void Update()
        {
            UpdateIMU();
            _wandPositionValue = TiltFive.Wand.GetPosition(ControllerIndex());
        }

        /// <summary>
        /// Update the IMU visualization.
        /// </summary>
        private void UpdateIMU()
        {

            _wandRotationValue = TiltFive.Wand.GetRotation(ControllerIndex());
            _wandContainer.transform.rotation = _wandRotationValue;
        }

        /// <summary>
        /// Disable All Labels.
        /// </summary>
        private void DisableAllLabels()
        {
            // Disable labels on Start()
            foreach (Transform label in _labelsContainer)
            {
                label.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Called when the wand becomes available.
        /// Show the image in the available color and change the text.
        /// </summary>
        public void OnWandAvailable()
        {
            switch (_wandHand)
            {
                case WandHand.Right:
                    _wandAvailableText.text = "Right Wand Available";
                    break;
                case WandHand.Left:
                    _wandAvailableText.text = "Left Wand Available";
                    break;
                default:
                    throw new System.ArgumentException($"Invalid wand hand: {_wandHand}");
            }
            _wandAvailableImage.color = _wandAvailableColor;
            DisableAllLabels();
        }

        /// <summary>
        /// Called when the wand becomes unavailable.
        /// Show the image in the unavailable color and change the text.
        /// </summary>
        public void OnWandUnavailable()
        {
            switch (_wandHand)
            {
                case WandHand.Right:
                    _wandAvailableText.text = "Right Wand Unavailable";
                    break;
                case WandHand.Left:
                    _wandAvailableText.text = "Left Wand Unavailable";
                    break;
                default:
                    throw new System.ArgumentException($"Invalid wand hand: {_wandHand}");
            }
            _wandAvailableImage.color = _wandUnavailableColor;
            DisableAllLabels();
        }

        /// <summary>
        /// The original Y position of the button A
        /// </summary>
        private float _originalYPositionA;
        /// <summary>
        /// Called when the A button is pressed.
        /// </summary>
        public void OnAPressed()
        {
            _aButton.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionA = _aButton.transform.localPosition.y;
            _aButton.transform.localPosition = new Vector3(_aButton.transform.localPosition.x, _aButton.transform.localPosition.y - _buttonPressAmount, _aButton.transform.localPosition.z);
            _aLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the A button is lifted.
        /// </summary>
        public void OnALifted()
        {
            _aButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _aButton.transform.localPosition = new Vector3(_aButton.transform.localPosition.x, _originalYPositionA, _aButton.transform.localPosition.z);
            _aLabel.SetActive(false);
        }

        /// <summary>
        /// The original Y position of the button B
        /// </summary>
        private float _originalYPositionB;

        /// <summary>
        /// Called when the B button is pressed.
        /// </summary>
        public void OnBPressed()
        {
            _bButton.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionB = _bButton.transform.localPosition.y;
            _bButton.transform.localPosition = new Vector3(_bButton.transform.localPosition.x, _bButton.transform.localPosition.y - _buttonPressAmount, _bButton.transform.localPosition.z);
            _bLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the B button is lifted.
        /// </summary>
        public void OnBLifted()
        {
            _bButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _bButton.transform.localPosition = new Vector3(_bButton.transform.localPosition.x, _originalYPositionB, _bButton.transform.localPosition.z);
            _bLabel.SetActive(false);
        }

        /// <summary>
        /// The original Y position of the button X
        /// </summary>
        private float _originalYPositionX;

        /// <summary>
        /// Called when the X button is pressed.
        /// </summary>
        public void OnXPressed()
        {
            _xButton.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionX = _xButton.transform.localPosition.y;
            _xButton.transform.localPosition = new Vector3(_xButton.transform.localPosition.x, _xButton.transform.localPosition.y - _buttonPressAmount, _xButton.transform.localPosition.z);
            _xLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the X button is lifted.
        /// </summary>
        public void OnXLifted()
        {
            _xButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _xButton.transform.localPosition = new Vector3(_xButton.transform.localPosition.x, _originalYPositionX, _xButton.transform.localPosition.z);
            _xLabel.SetActive(false);
        }

        /// <summary>
        /// The original Y position of the button Y
        /// </summary>
        private float _originalYPositionY;

        /// <summary>
        /// Called when the Y button is pressed.
        /// </summary>
        public void OnYPressed()
        {
            _yButton.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionY = _yButton.transform.localPosition.y;
            _yButton.transform.localPosition = new Vector3(_yButton.transform.localPosition.x, _yButton.transform.localPosition.y - _buttonPressAmount, _yButton.transform.localPosition.z);
            _yLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the Y button is lifted.
        /// </summary>
        public void OnYLifted()
        {
            _yButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _yButton.transform.localPosition = new Vector3(_yButton.transform.localPosition.x, _originalYPositionY, _yButton.transform.localPosition.z);
            _yLabel.SetActive(false);
        }

        /// <summary>
        /// The original Y position of the button 1
        /// </summary>
        private float _originalYPositionOne;

        /// <summary>
        /// Called when the 1 button is pressed.
        /// </summary>
        public void OnOnePressed()
        {
            _oneButton.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionOne = _oneButton.transform.localPosition.y;
            _oneButton.transform.localPosition = new Vector3(_oneButton.transform.localPosition.x, _oneButton.transform.localPosition.y - _buttonPressAmount, _oneButton.transform.localPosition.z);
            _oneLabel.SetActive(true);
        }
        
        /// <summary>
        /// Called when the 1 button is lifted.
        /// </summary>
        public void OnOneLifted()
        {
            _oneButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _oneButton.transform.localPosition = new Vector3(_oneButton.transform.localPosition.x, _originalYPositionOne, _oneButton.transform.localPosition.z);
            _oneLabel.SetActive(false);
        }

        /// <summary>
        /// The original Y position of the button 2
        /// </summary>
        private float _originalYPositionTwo;
        /// <summary>
        /// Called when the 2 button is pressed.
        /// </summary>
        public void OnTwoPressed()
        {
            _twoButton.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionTwo = _twoButton.transform.localPosition.y;
            _twoButton.transform.localPosition = new Vector3(_twoButton.transform.localPosition.x, _twoButton.transform.localPosition.y - _buttonPressAmount, _twoButton.transform.localPosition.z);
            _twoLabel.SetActive(true);
        }
        
        /// <summary>
        /// Called when the 1 button is lifted.
        /// </summary>
        public void OnTwoLifted()
        {
            _twoButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _twoButton.transform.localPosition = new Vector3(_twoButton.transform.localPosition.x, _originalYPositionTwo, _twoButton.transform.localPosition.z);
            _twoLabel.SetActive(false);
        }

        /// <summary>
        /// Called when the Tilt Five button is pressed.
        /// </summary>
        public void OnTiltFiveButtonPressed()
        {
            _tiltFiveButton.GetComponent<MeshRenderer>().material = _selectedMaterial; 
            _tiltFiveLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the Tilt Five button is pressed.
        /// </summary>
        public void OnTiltFiveButtonLifted()
        {
            _tiltFiveButton.GetComponent<MeshRenderer>().material = _normalMaterial;
            _tiltFiveLabel.SetActive(false);
        }

        /// <summary>
        /// Called when the trigger button is pressed.
        /// </summary>
        public void OnTriggerPressed()
        {
            _trigger.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _triggerLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the trigger button is held down.
        /// Represent the trigger pushing into the controller depending on the amount received.
        /// </summary>
        public void OnTriggerDown(float pTrigger)
        {
            _triggerValue = pTrigger;
            // Calculate the rotation of the trigger proportional to the received input.
            float amount = _triggerMaxRotation - _triggerMinRotation;
            float proportionValue = amount * _triggerValue;
            float rotation = proportionValue + _triggerMinRotation;
            _trigger.transform.localEulerAngles = new Vector2(rotation, _trigger.transform.localEulerAngles.y);
        }

        /// <summary>
        /// Called when the trigger button is released.
        /// </summary>
        public void OnTriggerReleased()
        {
            _triggerValue = 0f;
            _trigger.GetComponent<MeshRenderer>().material = _normalMaterial;
            _trigger.transform.localEulerAngles = new Vector2(_triggerMinRotation, _trigger.transform.localEulerAngles.y);
            _triggerLabel.SetActive(false);
        }

        /// <summary>
        /// Called when the Joystick is moved.
        /// Represent the tilting of the stick.
        /// </summary>
        public void OnStickMoved(Vector2 pJoystickTilt)
        {
            _joystickValue = pJoystickTilt;

            _joystick.GetComponent<MeshRenderer>().material = _selectedMaterial;

            // Calculate the proportional rotation depending on the received tilt
            float rotationX = _stickMaxRotationX * _joystickValue.x;
            float rotationY = _stickMaxRotationY * _joystickValue.y;

            _joystick.transform.localEulerAngles = new Vector3(rotationY, _joystick.transform.localRotation.eulerAngles.y, rotationX); // Rotations assignments are different in the actual visualization.
            _joystickLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the stick stops moving
        /// </summary>
        public void OnStickStopMoving()
        {
            _joystickValue = Vector2.zero;
            _joystick.transform.localEulerAngles = new Vector3(0f, _joystick.transform.localRotation.eulerAngles.y, 0f); 
            _joystickLabel.SetActive(false);
        }

        /// <summary>
        /// The original Y position of the stick button
        /// </summary>
        private float _originalYPositionStick;

        /// <summary>
        /// Called when the stick is pressed.
        /// </summary>
        public void OnStickPressed()
        {
            _joystick.GetComponent<MeshRenderer>().material = _selectedMaterial;
            _originalYPositionStick = _joystick.transform.localPosition.y;
            _joystick.transform.localPosition = new Vector3(_joystick.transform.localPosition.x, _joystick.transform.localPosition.y - _buttonPressAmount, _joystick.transform.localPosition.z);
            _joystickPressLabel.SetActive(true);
        }

        /// <summary>
        /// Called when the stick press is released.
        /// </summary>
        public void OnStickReleased()
        {
            _joystick.transform.localPosition = new Vector3(_joystick.transform.localPosition.x, _originalYPositionStick, _joystick.transform.localPosition.z);
            _joystickPressLabel.SetActive(false);
        }

        /// <summary>
        /// Called when the stick stops being used (pressed or moved).
        /// Needs to use a separate method to avoid conflict in the visualization mesh between moving and pressing.
        /// </summary>
        public void OnStickStopUse()
        {
            _joystick.GetComponent<MeshRenderer>().material = _normalMaterial;
        }
    }
}
