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
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// The first selected button.
        /// </summary>
        [SerializeField]
        private Button _firstButton;

        /// <summary>
        /// The quit button.
        /// </summary>
        [SerializeField]
        private Button _quitButton;

        /// <summary>
        /// The quit menu.
        /// </summary>
        [SerializeField]
        private QuitMenu _quitMenu;

        [SerializeField]
        private CylinderScroll[] _cylinders;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(WaitForSelect());
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _quitMenu.gameObject.SetActive(false);
            //SelectLastButton();
        }

        /// <summary>
        /// Waits a frame before doing the button selection.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForSelect()
        {
            yield return new WaitForEndOfFrame();
            SelectLastButton();
        }

        /// <summary>
        /// Select the first button stored in the playerprefs.
        /// </summary>
        public void SelectLastButton()
        {
            string buttonName = PlayerPrefs.GetString("Last_Scene");

            if (!string.IsNullOrEmpty(buttonName))
            {
                GameObject lastButton = GameObject.Find(buttonName);

                if (lastButton != null)
                {
                    Button buttonSelectable = lastButton.GetComponent<Button>();

                    foreach(CylinderScroll cylinder in _cylinders)
                    {
                        cylinder.RotateImmediately = true;
                    }

                    if (buttonSelectable != null)
                    {
                        buttonSelectable.Select();
                    }
                }
            }
            else
            {
                _firstButton.Select();
            }
        }

        /// <summary>
        /// Select the quit button
        /// </summary>
        public void SelectQuit()
        {
            _quitButton.Select();
        }

        /// <summary>
        /// When the quit button is pressed.
        /// </summary>
        public void OnQuitPressed()
        {
            _quitMenu.gameObject.SetActive(true);
        }

        public void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}
