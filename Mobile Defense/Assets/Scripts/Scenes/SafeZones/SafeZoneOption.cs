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
    /// The class for the safe zone option UI.
    /// </summary>
    public class SafeZoneOption : MonoBehaviour
    {
        /// <summary>
        /// The enum for the type of option.
        /// </summary>
        public enum SafeZoneSettingType
        {
            BoardConfiguration,
            PlayerPosition,
            TableHeight,
            GamePosition
        }

        /// <summary>
        /// The safe zone type of this UI
        /// </summary>
        [SerializeField]
        private SafeZoneSettingType _safeZoneSettingType;

        /// <summary>
        /// The container for all the "buttons", displaying the number of options.
        /// </summary>
        [SerializeField]
        private Transform _buttonsContainer;

        /// <summary>
        /// The prefab for the button.
        /// </summary>
        [SerializeField]
        private GameObject _buttonPrefab;

        /// <summary>
        /// The color for the button on its normal state.
        /// </summary>
        [SerializeField]
        private Color _normalButtonColor;

        /// <summary>
        /// The color for the button on its selected state.
        /// </summary>
        [SerializeField]
        private Color _selectedButtonColor;

        /// <summary>
        /// The text with the information.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _text;

        /// <summary>
        /// The list of button images.
        /// </summary>
        private Image[] _buttons;

        public SafeZoneSettingType SafeZoneSetting { get => _safeZoneSettingType; set => _safeZoneSettingType = value; }

        // Start is called before the first frame update
        void Awake()
        {
            // Destroy any items inside the container of buttons in the update.
            foreach (Transform button in _buttonsContainer)
            {
                Destroy(button.gameObject);
            }
        }

        /// <summary>
        /// Set the buttons in the container UI from the prefab.
        /// </summary>
        /// <param name="pNumberOfButtons"></param>
        public void SetButtons(int pNumberOfButtons)
        {
            List<Image> buttonsList = new List<Image>();

            for (int i = 0; i < pNumberOfButtons; i++)
            {
                GameObject button = Instantiate(_buttonPrefab, _buttonsContainer);

                buttonsList.Add(button.GetComponent<Image>());
            }

            _buttons = buttonsList.ToArray();
        }

        /// <summary>
        /// Set the current active button and text.
        /// </summary>
        /// <param name="pCurrentActive"></param>
        /// <param name="pDescription"></param>
        public void SetCurrentActive(int pCurrentActive, string pDescription)
        {
            SetAllButtonsNormal();

            _buttons[pCurrentActive].color = _selectedButtonColor;

            string text = "";

            switch (_safeZoneSettingType)
            {
                case SafeZoneSettingType.PlayerPosition:
                    text = "Player Position: ";
                    break;
                case SafeZoneSettingType.GamePosition:
                    text = "Game Position: ";
                    break;
                case SafeZoneSettingType.TableHeight:
                    text = "Table Height: ";
                    break;
                case SafeZoneSettingType.BoardConfiguration:
                    text = "Board Size: ";
                    break;
            }

            text += pDescription;

            _text.text = text;
        }

        /// <summary>
        /// Return all buttons to normal.
        /// </summary>
        private void SetAllButtonsNormal()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].color = _normalButtonColor;
            }
        }
    }
}