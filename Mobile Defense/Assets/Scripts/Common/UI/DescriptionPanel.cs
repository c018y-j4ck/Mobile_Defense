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
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Controls the behaviour of the description panel pop-up.
    /// </summary>
    public class DescriptionPanel : MonoBehaviour
    {
        /// <summary>
        /// The Tilt Five input receiver for this demo, that sends wand actions.
        /// </summary>
        [SerializeField]
        private T5InputReceiver _demoInputReceiver;

        /// <summary>
        /// The input handler for this demo, that handles different actions.
        /// </summary>
        [SerializeField]
        private BaseDemoInput _demoInput;

        /// <summary>
        /// The main ok button.
        /// </summary>
        [SerializeField]
        private Button _okButton;

        /// <summary>
        /// Event to be called when the pop-up start, to enable different behaviours that occur at the start.
        /// </summary>
        [SerializeField]
        private UnityEvent _onStartEvent;

        /// <summary>
        /// Event to be called when the pop-up is closed (such as focusing on a different UI)
        /// </summary>
        [SerializeField]
        private UnityEvent _onCloseEvent;

        /// <summary>
        /// Event to be called when the pop-up is about to open.
        /// </summary>
        [SerializeField]
        private UnityEvent _onOpenEvent;

        /// <summary>
        /// The main menu scene
        /// </summary>
        private const string MAIN_MENU_SCENE = "MainMenu";

        /// <summary>
        /// Whether the instructions can be opened.
        /// </summary>
        private bool _canOpenInstructions = true;

        private void Start()
        {
            // Toggle the description at the start.
            ToggleDescription(true);

            _onStartEvent.Invoke();
        }

        /// <summary>
        /// Toggle whether the instructions can be opened, useful for some demos.
        /// </summary>
        /// <param name="pCanOpen"></param>
        public void ToggleCanOpen(bool pCanOpen)
        {
            _canOpenInstructions = pCanOpen;
        }

        /// <summary>
        /// Toggle the description and deactivate this demo's input receiver.
        /// </summary>
        /// <param name="pToggle"></param>
        public void ToggleDescription(bool pToggle)
        {
            pToggle = pToggle && _canOpenInstructions;

            if(pToggle)
            {
                _onOpenEvent.Invoke();
            }

            gameObject.SetActive(pToggle);

            if (_demoInputReceiver != null)
            {
                _demoInputReceiver.Active = !pToggle;
            }
            if (_demoInput != null)
            {
                _demoInput.Active = !pToggle;
            }

            // Change the timescale depending on the current state.
            if(pToggle)
            {
                Time.timeScale = 0f;
                SwitchToInteractable();
            }
            else
            {
                Time.timeScale = 1f;
                _onCloseEvent.Invoke();
            }
        }

        /// <summary>
        /// Switches to this instruction's panel main interactable button.
        /// </summary>
        public void SwitchToInteractable()
        {
            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);

                Canvas.ForceUpdateCanvases();

                _okButton.Select();

                Canvas.ForceUpdateCanvases();
            }
        }

        /// <summary>
        /// Return to the main menu.
        /// </summary>
        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(MAIN_MENU_SCENE);
        }    
    }
}
