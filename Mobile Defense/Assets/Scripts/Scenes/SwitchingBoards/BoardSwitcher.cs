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
using TiltFive;
using UnityEngine;
using Input = UnityEngine.Input;

namespace TiltFiveDemos
{
    /// <summary>
    /// Class to switch the current board using input.
    /// </summary>
    public class BoardSwitcher : BaseDemoInput
    {
        /// <summary>
        /// The tilt five manager.
        /// We only need one Tilt Five manager and one Tilt Five Camera.
        /// </summary>
        [SerializeField] private TiltFiveManager2 _tiltFiveManager;
        
        /// <summary>
        /// All the boards we have in the game.
        /// </summary>
        [SerializeField] private GameBoard[] _boards;

        /// <summary>
        /// The index of the first board to switch to.
        /// </summary>
        [SerializeField] private int _firstBoard = 0;

        /// <summary>
        /// The variable for the current board.
        /// /// </summary>
        private int _currentBoard = 0;

        private void Start()
        { 
            // Switch to the first board.
            SwitchBoard(_firstBoard);
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        private void Update()
        {
            if (!_active) return;

            CheckForSequentialBoardInput();
            CheckForSpecificBoardInput();
        }

        /// <summary>
        /// Checks for sequential board input.
        /// </summary>
        private void CheckForSequentialBoardInput()
        {
            if (Input.GetButtonDown("PreviousBoard"))
            {
                PreviousBoard();
                return;
            }

            if (Input.GetButtonDown("NextBoard"))
            {
                NextBoard();
                return;
            }
        }

        /// <summary>
        /// Checks for specific board input.
        /// </summary>
        private void CheckForSpecificBoardInput()
        {
            if (Input.GetButtonDown("Board1"))
            {
                SwitchBoard(0);
                return;
            }
            if (Input.GetButtonDown("Board2"))
            {
                SwitchBoard(1);
                return;
            }
            if (Input.GetButtonDown("Board3"))
            {
                SwitchBoard(2);
                return;
            }
            if (Input.GetButtonDown("Board4"))
            {
                SwitchBoard(3);
                return;
            }
        }

        /// <summary>
        /// Switch to the board using Tilt Five's game manager.
        /// </summary>
        /// <param name="pBoardIndex"></param>
        private void SwitchBoard(int pBoardIndex)
        {
            _currentBoard = pBoardIndex;
            
            // Simply change which board is currently active in the glasses settings.
            GameBoard board = _boards[_currentBoard];
            board.gameObject.SetActive(true);
            
            // Set all the unit variables already set in the board info class on the tilt five manager.
            _tiltFiveManager.playerOneSettings.gameboardSettings.currentGameBoard = board;

            // Use a coroutine to wait for the end of the current frame so that objects that change boards have time to select he currently active board,
            // and then disable everything in the board that we don't want to be seen on a different board.
            StartCoroutine(WaitToCloseBoards(_currentBoard));
        }

        /// <summary>
        /// Wait for the end of frame so that boards are eliminated after the objects have been able to select the current board.
        /// </summary>
        /// <param name="pBoard">The board index to maintain active</param>
        /// <returns></returns>
        private IEnumerator WaitToCloseBoards(int pBoard)
        {
            yield return new WaitForEndOfFrame();
            
            for (int i = 0; i < _boards.Length; i++)
            {
                if (i != pBoard)
                {
                    _boards[i].gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Switches the board forward ton the next index.
        /// Wraps back to zero after reaching the final index.
        /// </summary>
        /// <param name="pContext"></param>
        public void NextBoard()
        {
                if ((_currentBoard + 1) < _boards.Length)
                {
                    SwitchBoard(_currentBoard + 1);
                }
                else
                {
                    SwitchBoard(0);
                }
        }

        /// <summary>
        /// Switches the board backwards to the previous index.
        /// Wraps back to the final index after reaching zero.
        /// </summary>
        /// <param name="pContext"></param>
        public void PreviousBoard()
        {
                if ((_currentBoard - 1) >= 0)
                {
                    SwitchBoard(_currentBoard - 1);
                }
                else
                {
                    SwitchBoard(_boards.Length - 1);
                }
        }

        /// <summary>
        /// Switch to board 1
        /// </summary>
        public void Board1()
        {
            SwitchBoard(0);
        }


        /// <summary>
        /// Switch to board 2
        /// </summary>
        public void Board2()
        {
            SwitchBoard(1);
        }


        /// <summary>
        /// Switch to board 3
        /// </summary>
        public void Board3()
        {
            SwitchBoard(2);
        }

        /// <summary>
        /// Switch to board 4
        /// </summary>
        public void Board4()
        {
            SwitchBoard(3);
        }
    }
}
