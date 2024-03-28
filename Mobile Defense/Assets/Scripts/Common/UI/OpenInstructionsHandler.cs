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
using UnityEngine;
using UnityEngine.SceneManagement;
using Input = UnityEngine.Input;

namespace TiltFiveDemos
{
    /// <summary>
    /// Handles opening the instructions panel on the current scene from various input methods, as well as positioning it on different boards.
    /// </summary>
    public class OpenInstructionsHandler : MonoBehaviour
    {        
        /// <summary>
        /// Use a singleton pattern to keep this object running on all scenes of the game.
        /// </summary>
        private static OpenInstructionsHandler _instance;

        public static OpenInstructionsHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<OpenInstructionsHandler>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("T5UIInput");
                        _instance = container.AddComponent<OpenInstructionsHandler>();
                    }
                }

                return _instance;
            }
        }

        [SerializeField]
        private GameObject _doublePressNotification;

        [SerializeField]
        private bool _enableInstructions = true;

        /// <summary>
        /// Whether the game should wait for a second press on instructions.
        /// </summary>
        private bool _doublePressForInstructions = false;

        private bool _pressedOnce = false;

        /// <summary>
        /// The instructions.
        /// </summary>
        private DescriptionPanel _instructions;

        public bool EnableInstructions
        {
            get => Instance._enableInstructions;
            set => Instance._enableInstructions = value;
        }

        /// <summary>
        /// Set to find the current instructions panel on start scene.
        /// </summary>
        private void OnEnable()
        {
            SceneManager.sceneLoaded += delegate { LoadInstructions(); };
        }

        /// <summary>
        /// Removed the current delegate on game end.
        /// </summary>
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= delegate { LoadInstructions(); };
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadInstructions();
            if (Instance._doublePressNotification != null)
            {
                Instance._doublePressNotification.SetActive(false);
            }
        }

        /// <summary>
        /// Load the instructions panel in the scene.
        /// </summary>
        private void LoadInstructions()
        {
            Instance._instructions = FindObjectOfType<DescriptionPanel>();

            // Find the sample settings in the scene.
            SampleSettings sceneSampleSettings = FindObjectOfType<SampleSettings>();
            if(sceneSampleSettings != null)
            {
                Instance._doublePressForInstructions = sceneSampleSettings.DoublePressForInstructions;
            }
            else // Set the default sample settings if we can't find the object in the scene.
            {
                Instance._doublePressForInstructions = false;
            }
        }

        private void Update()
        {
            // Listen to the exit button.
            if (Input.GetButtonDown("Exit"))
            {
                OpenInstructions();
            }
        }

        private Coroutine _waitForDisableDoublePressCoroutine = null;

        private void OpenInstructionsInternal()
        {
            if (!_enableInstructions) return;

            if (_instructions != null)
            {
                if (!_instructions.gameObject.activeSelf)
                {
                    if (_doublePressForInstructions && !_pressedOnce)
                    {
                        _pressedOnce = true;
                        if (_doublePressNotification != null) _doublePressNotification.SetActive(true);
                        _waitForDisableDoublePressCoroutine = StartCoroutine(WaitForDisableDoublePressCoroutine());
                        return;
                    }

                    DisableDoublePress();

                    TiltFiveManager tiltFiveManager = FindObjectOfType<TiltFiveManager>();

                    _instructions.ToggleDescription(true);

                    if (tiltFiveManager != null)
                    {
                        _instructions.transform.SetParent(tiltFiveManager.gameBoardSettings.currentGameBoard.transform, false);
                    }
                }
                else
                {
                    _instructions.ReturnToMainMenu();
                }
            }
            else
            {
                MainMenu mainMenu = FindObjectOfType<MainMenu>();

                if(mainMenu != null)
                {
                    mainMenu.OnQuitPressed();
                }
            }
        }

        /// <summary>
        /// Open the instructions panel if it's found in the scene.
        /// This method is also called on alternative input options.
        /// </summary>
        public void OpenInstructions()
        {
            Instance.OpenInstructionsInternal();
        }

        private IEnumerator WaitForDisableDoublePressCoroutine()
        {
            yield return new WaitForSeconds(1f); 
            DisableDoublePress();
        }

        private void DisableDoublePress()
        {
            _pressedOnce = false;
            if (_doublePressNotification != null) _doublePressNotification.SetActive(false);

            if (_waitForDisableDoublePressCoroutine != null)
            {
                StopCoroutine(_waitForDisableDoublePressCoroutine);
                _waitForDisableDoublePressCoroutine = null;
            }
        }
    }
}
