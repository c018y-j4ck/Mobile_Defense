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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Creates a virtual cursor on the selected canvas capable of interacting with UI elements,
    /// positioned proportionately with the position of the cursor on the computer's screen.
    /// This class requires an image and a rect transform since it's part of the UI.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class VirtualCursor : BaseDemoInput
    {
        /// <summary>
        /// The container of the cursor.
        /// </summary>
        [SerializeField] private RectTransform _container;

        /// <summary>
        /// The reference Tilt Five camera.
        /// </summary>
        [SerializeField] private Camera _referenceCamera;

        /// <summary>
        /// The graphics raycaster in the canvas.
        /// </summary>
        [SerializeField] GraphicRaycaster _raycaster;

        /// <summary>
        /// The movement speed for joystick or keyboard input.
        /// </summary>
        [SerializeField] private float _speed = 0.1f;

        /// <summary>
        /// The normal pointer color.
        /// </summary>
        [SerializeField]
        private Color _normalColor;

        /// <summary>
        /// The highlighted pointer color.
        /// </summary>
        [SerializeField]
        private Color _highlightedColor;

        /// <summary>
        /// The event system in the scene.
        /// </summary>
        EventSystem _eventSystem;

        /// <summary>
        /// The screen width.
        /// </summary>
        private float _screenWidth = 0f;
        /// <summary>
        /// The screen height.
        /// </summary>
        private float _screenHeight = 0f;

        /// <summary>
        /// The size of the container.
        /// </summary>
        private Vector2 _containerSize;

        /// <summary>
        /// The cursor's rect transform.
        /// </summary>
        private RectTransform _rect;

        /// <summary>
        /// The pointer image.
        /// </summary>
        private Image _pointerImage;

        /// <summary>
        /// The current stored mouse position.
        /// </summary>
        private Vector2 _storedMousePosition = Vector2.positiveInfinity;

        /// <summary>
        /// The current movement.
        /// </summary>
        private Vector2 _cursorMovement = Vector2.zero;

        /// <summary>
        /// Flag to check if we're using the wand, to account for conflicts with the Input Manager.
        /// </summary>
        private bool _usingT5Wand = false;

        /// <summary>
        /// Flag to check if we already performed the click.
        /// </summary>
        private bool _doingClick = false;

        // Start is called before the first frame update
        private void Start()
        {
            _pointerImage = GetComponent<Image>();
            //Fetch the Event System from the Scene
            _eventSystem = GetComponent<EventSystem>();

            // Store the screen's width and height to find the normalized cursor position.
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;

            // Store the size of the container to use as the reference for positioning the cursor.
            _containerSize = _container.sizeDelta;

            _rect = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_active) return;

            MoveCursor();

            CheckUI();
        }

        /// <summary>
        /// Move the cursor with keyboard, gamepad input, or Tilt Five.
        /// </summary>
        private void MoveCursor()
        {
            if (!_usingT5Wand)
            {
                _cursorMovement = new Vector2(Input.GetAxis("CursorX"), Input.GetAxis("CursorY"));
            }

            if(_cursorMovement.magnitude != 0f)
            {
                if (_cursorMovement.magnitude < 0.25f) _cursorMovement = Vector2.zero;

                _rect.anchoredPosition = new Vector2(Mathf.Clamp(_rect.anchoredPosition.x + (_cursorMovement.x * _speed * Time.deltaTime), 0f, _containerSize.x),
                    Mathf.Clamp(_rect.anchoredPosition.y + (_cursorMovement.y * _speed * Time.deltaTime), 0f, _containerSize.y));
            }
        }

        /// <summary>
        /// On the T5 wand movement.
        /// </summary>
        /// <param name="pInput"></param>
        public void T5WandCursorInput(Vector2 pInput)
        {
            _usingT5Wand = true;

            _cursorMovement = pInput;
        }

        /// <summary>
        /// Stop the T5 wand movement.
        /// </summary>
        public void StopT5WandCursorInput()
        {
            _usingT5Wand = false;

            _cursorMovement = Vector2.zero;
        }

        /// <summary>
        /// Stop the T5 wand movement.
        /// </summary>
        public override void StopAllT5Input()
        {
            StopT5WandCursorInput();
            base.StopAllT5Input();
        }

        /// <summary>
        /// Check the UI click.
        /// </summary>
        private void CheckUI()
        {
            //Set up the new Pointer Event
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);

            //Set the Pointer Event Position to that of the virtual cursor position
            pointerEventData.position = _referenceCamera.WorldToScreenPoint(transform.position);

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            _raycaster.Raycast(pointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            if (results.Count > 0)
            {
                for(int i = 0; i < results.Count; i++)
                {
                    Button button = results[i].gameObject.GetComponent<Button>();

                    if (button != null)
                    {
                        _pointerImage.color = _highlightedColor;
                        //Check if the left Mouse button is clicked
                        if (Input.GetButtonDown("CursorClick") || _doingClick)
                        {
                            _doingClick = false;
                            // Invoke the button event
                            button.onClick.Invoke();
                        }
                    }
                    else
                    {
                        _pointerImage.color = _normalColor;
                    }
                }
            }
            else
            {
                _pointerImage.color = _normalColor;
            }
        }

        /// <summary>
        /// Perform the click (Called from outside events.)
        /// </summary>
        public void DoClick()
        {
            _doingClick = true;
        }
    }
}
