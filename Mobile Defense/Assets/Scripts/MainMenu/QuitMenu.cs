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
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// The quit menu in the main menu.
    /// </summary>
    public class QuitMenu : MonoBehaviour
    {
        /// <summary>
        /// The button to return to the main menu.
        /// </summary>
        [SerializeField]
        private Button _buttonReturn;

        /// <summary>
        /// The button to quit the application.
        /// </summary>
        [SerializeField]
        private Button _buttonQuit;

        /// <summary>
        /// The main menu class.
        /// </summary>
        [SerializeField]
        private MainMenu _mainMenu;

        /// <summary>
        /// Select the return button when opened.
        /// </summary>
        private void OnEnable()
        {
            _buttonReturn.Select();
        }

        /// <summary>
        /// Close this panel and return to the main menu when the Tilt Five button is pressed.
        /// </summary>
        private void Update()
        {
            if (TiltFive.Input.TryGetButtonDown(TiltFive.Input.WandButton.T5, out bool t5ButtonPressed) && t5ButtonPressed)
            {
                OnReturnPressed();
            }
        }

        /// <summary>
        /// Close this menu on the button return.
        /// </summary>
        public void OnReturnPressed()
        {
            _mainMenu.SelectLastButton();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Close the application on the button quit.
        /// </summary>
        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}
