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

namespace TiltFiveDemos
{
    /// <summary>
    /// The safe zone manager class.
    /// Contains all methods with information for different board configurations.
    /// </summary>
    public class SafeZoneManager : MonoBehaviour
    {
        /// <summary>
        /// Enum containing table height types.
        /// </summary>
        public enum TableHeight
        {
            DiningTable,
            CoffeeTable
        }

        /// <summary>
        /// Enum containing player position types.
        /// </summary>
        public enum PlayerPosition
        {
            PlayerSeated,
            PlayerStanding
        }

        /// <summary>
        /// Enum containing game position types.
        /// </summary>
        public enum GamePosition
        {
            Normal,
            Skyscraper,
            Well
        }

        /// <summary>
        /// Enum containing board configuration types.
        /// </summary>
        public enum BoardConfiguration
        {
            LE,
            XE,
            KickstandXE
        }

        /// <summary>
        /// The safe zone sides.
        /// </summary>
        [SerializeField]
        private SafeZoneSide[] _safeZoneSides;

        /// <summary>
        /// The RectTransform of the main canvas.
        /// </summary>
        [SerializeField]
        private RectTransform _canvasRect;

        /// <summary>
        /// The UI icons that contain the information on different settings.
        /// </summary>
        [SerializeField]
        private SafeZoneOption[] _safeZoneOptions;

        /// <summary>
        /// The safe zone object at the center of the board.
        /// </summary>
        [SerializeField]
        private GameObject _centerSafeZoneObject;

        /// <summary>
        /// The normal environment object.
        /// </summary>
        [SerializeField]
        private GameObject _normalObject;

        /// <summary>
        /// The skyscraper environment game object.
        /// </summary>
        [SerializeField]
        private GameObject _skyscraperObject;

        /// <summary>
        /// The well environment game object.
        /// </summary>
        [SerializeField]
        private GameObject _wellObject;

        /// <summary>
        /// The plane at the normal position.
        /// </summary>
        [SerializeField]
        private GameObject _normalPlane;

        /// <summary>
        /// The current selected environment, depending on the selected game position.
        /// </summary>
        private GameObject _currentObject;

        /// <summary>
        /// The current table height.
        /// </summary>
        private TableHeight _currentTableHeight = TableHeight.DiningTable;

        /// <summary>
        /// The current player position.
        /// </summary>
        private PlayerPosition _currentPlayerPosition = PlayerPosition.PlayerSeated;

        /// <summary>
        /// The current game position.
        /// </summary>
        private GamePosition _currentGamePosition = GamePosition.Normal;

        /// <summary>
        /// The current board configuration.
        /// </summary>
        private BoardConfiguration _currentBoardConfiguration = BoardConfiguration.LE;

        // Start is called before the first frame update
        void Start()
        {
            // Configure the UI buttons at the start depending on the kind of configuration they contain.
            foreach (SafeZoneOption options in _safeZoneOptions)
            {
                switch (options.SafeZoneSetting)
                {
                    case SafeZoneOption.SafeZoneSettingType.PlayerPosition:
                        options.SetButtons(Enum.GetNames(typeof(PlayerPosition)).Length);
                        SetCurrentPlayerPositionUI();
                        break;

                    case SafeZoneOption.SafeZoneSettingType.GamePosition:
                        options.SetButtons(Enum.GetNames(typeof(GamePosition)).Length);
                        SetCurrentGamePositionUI();
                        break;

                    case SafeZoneOption.SafeZoneSettingType.TableHeight:
                        options.SetButtons(Enum.GetNames(typeof(TableHeight)).Length);
                        SetCurrentTableHeightUI();
                        break;

                    case SafeZoneOption.SafeZoneSettingType.BoardConfiguration:
                        options.SetButtons(Enum.GetNames(typeof(BoardConfiguration)).Length);
                        SetCurrentBoardConfigurationUI();
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Listen to the buttons on keyboard and gamepad.

            if (Input.GetButtonDown("SwitchGameBoard"))
            {
                SwitchCurrentBoardConfiguration();
            }
            if (Input.GetButtonDown("SwitchTableType"))
            {
                SwitchCurrentTableHeight();
            }
            if (Input.GetButtonDown("SwitchPlayerPosition"))
            {
                SwitchCurrentPlayerPosition();
            }
            if (Input.GetButtonDown("SwitchGamePosition"))
            {
                SwitchCurrentGamePosition();
            }
            if ((Input.GetButtonDown("ToggleEnvironment") || Input.GetAxis("ToggleEnvironment") > 0.5f) && !_currentObject.activeInHierarchy)
            {
                ShowEnvironment();
            }
            if (((Input.GetButtonUp("ToggleEnvironment") || Input.GetAxis("ToggleEnvironment") < 0.5f) && _currentObject.activeInHierarchy) && !_showingEnvironmentFromT5)
            {
                HideEnvironment();
            }
        }

        /// <summary>
        /// Switch the current table height.
        /// Increase the current enum index and compare if it's larger than the total items in the enum.
        /// </summary>
        public void SwitchCurrentTableHeight()
        {
            int currentTableHeightNumber = (int)_currentTableHeight + 1;

            if (currentTableHeightNumber >= Enum.GetNames(typeof(TableHeight)).Length)
            {
                currentTableHeightNumber = 0;
            }

            _currentTableHeight = (TableHeight)currentTableHeightNumber;

            SetCurrentTableHeightUI();

            CalculateConfiguration();
        }

        /// <summary>
        /// Set the UI for the table height.
        /// </summary>
        private void SetCurrentTableHeightUI()
        {
            string description = "";

            switch (_currentTableHeight)
            {
                case TableHeight.DiningTable:
                    description = "Dining Table";
                    break;

                case TableHeight.CoffeeTable:
                    description = "Coffee Table";
                    break;
            }

            SafeZoneOption option = GetSafeZoneOption(SafeZoneOption.SafeZoneSettingType.TableHeight);

            option.SetCurrentActive((int)_currentTableHeight, description);
        }

        /// <summary>
        /// Switch the current player position.
        /// Increase the current enum index and compare if it's larger than the total items in the enum.
        /// </summary>
        public void SwitchCurrentPlayerPosition()
        {
            int currentPlayerPositionNumber = (int)_currentPlayerPosition + 1;

            if (currentPlayerPositionNumber >= Enum.GetNames(typeof(PlayerPosition)).Length)
            {
                currentPlayerPositionNumber = 0;
            }

            _currentPlayerPosition = (PlayerPosition)currentPlayerPositionNumber;

            SetCurrentPlayerPositionUI();

            CalculateConfiguration();
        }

        /// <summary>
        /// Set the UI for the player position.
        /// </summary>
        private void SetCurrentPlayerPositionUI()
        {
            string description = "";

            switch (_currentPlayerPosition)
            {
                case PlayerPosition.PlayerSeated:
                    description = "Seated";
                    break;

                case PlayerPosition.PlayerStanding:
                    description = "Standing";
                    break;
            }

            SafeZoneOption option = GetSafeZoneOption(SafeZoneOption.SafeZoneSettingType.PlayerPosition);

            option.SetCurrentActive((int)_currentPlayerPosition, description);
        }

        /// <summary>
        /// Switch the current game position.
        /// Increase the current enum index and compare if it's larger than the total items in the enum.
        /// </summary>
        public void SwitchCurrentGamePosition()
        {
            int currentGamePositionNumber = (int)_currentGamePosition + 1;

            if (currentGamePositionNumber >= Enum.GetNames(typeof(GamePosition)).Length)
            {
                currentGamePositionNumber = 0;
            }

            _currentGamePosition = (GamePosition)currentGamePositionNumber;

            SetCurrentGamePositionUI();

            CalculateConfiguration();
        }

        /// <summary>
        /// Set the UI for the game position.
        /// </summary>
        private void SetCurrentGamePositionUI()
        {
            string description = "";

            switch (_currentGamePosition)
            {
                case GamePosition.Normal:
                    description = "Normal";
                    _currentObject = _normalObject;
                    break;

                case GamePosition.Skyscraper:
                    description = "Skyscraper";
                    _currentObject = _skyscraperObject;
                    break;

                case GamePosition.Well:
                    description = "Well";
                    _currentObject = _wellObject;
                    break;
            }

            SafeZoneOption option = GetSafeZoneOption(SafeZoneOption.SafeZoneSettingType.GamePosition);

            option.SetCurrentActive((int)_currentGamePosition, description);
        }

        /// <summary>
        /// Switch the current board configuration.
        /// Increase the current enum index and compare if it's larger than the total items in the enum.
        /// </summary>
        public void SwitchCurrentBoardConfiguration()
        {
            int currentBoardConfigurationNumber = (int)_currentBoardConfiguration + 1;

            if (currentBoardConfigurationNumber >= Enum.GetNames(typeof(BoardConfiguration)).Length)
            {
                currentBoardConfigurationNumber = 0;
            }

            _currentBoardConfiguration = (BoardConfiguration)currentBoardConfigurationNumber;

            SetCurrentBoardConfigurationUI();

            CalculateConfiguration();
        }

        /// <summary>
        /// Set the UI for the board configuration.
        /// </summary>
        private void SetCurrentBoardConfigurationUI()
        {
            string description = "";

            switch (_currentBoardConfiguration)
            {
                case BoardConfiguration.LE:
                    description = "LE";
                    break;

                case BoardConfiguration.XE:
                    description = "XE";
                    break;

                case BoardConfiguration.KickstandXE:
                    description = "Kickstand XE";
                    break;
            }

            SafeZoneOption option = GetSafeZoneOption(SafeZoneOption.SafeZoneSettingType.BoardConfiguration);

            option.SetCurrentActive((int)_currentBoardConfiguration, description);
        }

        /// <summary>
        /// Calculate the configuration
        /// </summary>
        public void CalculateConfiguration()
        {
            // Set different factor multipliers for all sides.

            float[] factorsFront = new float[3];
            float[] factorsBack = new float[3];
            float[] factorsLeft = new float[3];
            float[] factorsRight = new float[3];

            // Change the factors depending on the current configuration.

            switch (_currentBoardConfiguration)
            {
                case BoardConfiguration.LE:
                    _canvasRect.sizeDelta = new Vector2(_canvasRect.sizeDelta.x, 700f);
                    break;

                case BoardConfiguration.XE:
                case BoardConfiguration.KickstandXE:
                    _canvasRect.sizeDelta = new Vector2(_canvasRect.sizeDelta.x, 966f);
                    break;
            }

            switch (_currentTableHeight)
            {
                case TableHeight.CoffeeTable:
                    factorsFront[0] = 0.8f;
                    factorsBack[0] = 1.2f;
                    factorsLeft[0] = 1f;
                    factorsRight[0] = 1f;
                    break;

                case TableHeight.DiningTable:
                    factorsFront[0] = 1f;
                    factorsBack[0] = 1f;
                    factorsLeft[0] = 1f;
                    factorsRight[0] = 1f;
                    break;
            }

            switch (_currentPlayerPosition)
            {
                case PlayerPosition.PlayerSeated:
                    factorsFront[1] = 1f;
                    factorsBack[1] = 1f;
                    factorsLeft[1] = 1f;
                    factorsRight[1] = 1f;
                    break;

                case PlayerPosition.PlayerStanding:
                    factorsFront[1] = 1.2f;
                    factorsBack[1] = 0.8f;
                    factorsLeft[1] = 1f;
                    factorsRight[1] = 1f;
                    break;
            }


            switch (_currentGamePosition)
            {
                case GamePosition.Normal:
                    factorsFront[2] = 1f;
                    factorsBack[2] = 1f;
                    factorsLeft[2] = 1;
                    factorsRight[2] = 1f;
                    break;

                case GamePosition.Skyscraper:
                    factorsFront[2] = 0.8f;
                    factorsBack[2] = 1.2f;
                    factorsLeft[2] = 1f;
                    factorsRight[2] = 1f;
                    break;

                case GamePosition.Well:
                    factorsFront[2] = 1.2f;
                    factorsBack[2] = 0.8f;
                    factorsLeft[2] = 1f;
                    factorsRight[2] = 1f;
                    break;
            }

            SafeZoneSide safeZoneFront = GetSafeZoneSide(SafeZoneSide.SafeZonePosition.Front);
            SafeZoneSide safeZoneBack = GetSafeZoneSide(SafeZoneSide.SafeZonePosition.Back);
            SafeZoneSide safeZoneLeft = GetSafeZoneSide(SafeZoneSide.SafeZonePosition.Left);
            SafeZoneSide safeZoneRight = GetSafeZoneSide(SafeZoneSide.SafeZonePosition.Right);

            safeZoneFront.SetRectsLengths(factorsFront, factorsFront);
            safeZoneBack.SetRectsLengths(factorsBack, factorsBack);
            safeZoneLeft.SetRectsLengths(factorsLeft, factorsLeft);
            safeZoneRight.SetRectsLengths(factorsRight, factorsRight);
        }

        /// <summary>
        /// Returns the side of a specific position.
        /// </summary>
        /// <param name="pPosition"></param>
        /// <returns></returns>
        private SafeZoneSide GetSafeZoneSide(SafeZoneSide.SafeZonePosition pPosition)
        {
            foreach (SafeZoneSide side in _safeZoneSides)
            {
                if (side.Position == pPosition) return side;
            }

            return null;
        }

        /// <summary>
        /// Returns the UI option of a specific type.
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        private SafeZoneOption GetSafeZoneOption(SafeZoneOption.SafeZoneSettingType pType)
        {
            foreach (SafeZoneOption options in _safeZoneOptions)
            {
                if (options.SafeZoneSetting == pType) return options;
            }

            return null;
        }


        bool _showingEnvironmentFromT5 = false;

        /// <summary>
        /// Hide the center zone with the UI and display the environment.
        /// </summary>
        public void ShowEnvironment(bool pFromT5 = false)
        {
            Debug.Log("Show environment");
            _showingEnvironmentFromT5 = pFromT5;
            _normalPlane.SetActive(false);
            _centerSafeZoneObject.SetActive(false);
            _currentObject.SetActive(true);
        }

        /// <summary>
        /// Show the center zone with the UI and hide the environment.
        /// </summary>
        public void HideEnvironment()
        {
            _showingEnvironmentFromT5 = false;
            Debug.Log("Hide environment");
            _normalPlane.SetActive(true);
            _centerSafeZoneObject.SetActive(true);
            _currentObject.SetActive(false);
        }
    }
}
