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

using System.Collections.Generic;
using TiltFive;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Detects whether the glasses are connected.
    /// </summary>
    /// <remarks>
    /// Override the following functions to describe actions to take on availability events.
    /// <list type="bullet">
    ///     <item>
    ///       <description>
    ///           <see cref="TiltFiveDemos.GlassesDetectorBase.DoPlayerAvailable(PlayerIndex, bool)"/>
    ///       </description>
    ///     </item>
    ///     <item>
    ///       <description>
    ///           <see cref="TiltFiveDemos.GlassesDetectorBase.DoPlayerUnavailable(PlayerIndex)"/>
    ///       </description>
    ///     </item>
    ///     <item>
    ///       <description>
    ///           <see cref="TiltFiveDemos.GlassesDetectorBase.DoGlassesAvailable(bool)"/>
    ///       </description>
    ///     </item>
    ///     <item>
    ///       <description>
    ///           <see cref="TiltFiveDemos.GlassesDetectorBase.DoGlassesUnavailable()"/>
    ///       </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public class GlassesDetectorBase : MonoBehaviour
    {
        /// <summary>
        /// Create a flag to avoid checking repeatedly.
        /// </summary>
        protected bool _glassesAvailable = false;

        protected DescriptionPanel _instructions;

        private bool _firstUpdate = true;

        private static readonly PlayerIndex[] _playerIndexes = {
            PlayerIndex.One,
            PlayerIndex.Two,
            PlayerIndex.Three,
            PlayerIndex.Four,
        };

        private Dictionary<PlayerIndex, bool> _playerAvailable = new Dictionary<PlayerIndex, bool>()
        {
            [PlayerIndex.One] = false,
            [PlayerIndex.Two] = false,
            [PlayerIndex.Three] = false,
            [PlayerIndex.Four] = false,
        };

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _firstUpdate = true;
            _instructions = FindObjectOfType<DescriptionPanel>();
            // Check the glasses the first time, but force to switch to glasses on the first frame, if the glasses are connected.
            CheckGlasses(true);
            _firstUpdate = false;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // Force automatically switching if instructions are opened.
            // The demo behaviour should only work during the demo (but this decision could be removed if used for other purposes).
            if (_instructions.gameObject.activeSelf) CheckGlasses(true);
            else CheckGlasses(false);
        }

        /// <summary>
        /// Check the current state of the glasses and call the proper event.
        /// </summary>
        /// <param name="pForce">Whether to skip any UI prompt and force switching to glasses.</param>
        private void CheckGlasses(bool pForce)
        {
            bool sawAnyGlasses = false;
            // Make sure that the TiltFive plugin is enabled and available.

            foreach (var playerIndex in _playerIndexes)
            {
                if (Player.IsConnected(playerIndex))
                {
                    sawAnyGlasses = true;
                    OnPlayerAvailable(playerIndex);
                }
                else
                {
                    OnPlayerUnavailable(playerIndex);
                }
            }

            if (sawAnyGlasses)
            {
                OnGlassesAvailable(pForce);
            }
            else
            {
                OnGlassesUnavailable();
            }
        }

        /// <summary>
        /// On Glasses Available event.
        /// </summary>
        /// <param name="pForce">Whether to skip any UI prompt and force switching to glasses.</param>
        private void OnGlassesAvailable(bool pForce = false)
        {
            if (_firstUpdate || !_glassesAvailable)
            {
                _glassesAvailable = true;
                DoGlassesAvailable(pForce);
            }
        }

        /// <summary>
        /// On Glasses Unavailable event.
        /// </summary>
        private void OnGlassesUnavailable()
        {
            if (_firstUpdate || _glassesAvailable)
            {
                _glassesAvailable = false;
                DoGlassesUnavailable();
            }
        }

        /// <summary>
        /// On Player Available event.
        /// </summary>
        /// <param name="playerIndex">Indicates player for the event action.</param>
        /// <param name="pForce">Whether to skip any UI prompt and force switching to glasses.</param>
        private void OnPlayerAvailable(PlayerIndex playerIndex, bool pForce = false)
        {
            if (_firstUpdate || !_playerAvailable[playerIndex])
            {
                _playerAvailable[playerIndex] = true;
                DoPlayerAvailable(playerIndex, pForce);
            }
        }

        /// <summary>
        /// On Player Unavailable event.
        /// </summary>
        /// <param name="playerIndex">Indicates player for the event action.</param>
        private void OnPlayerUnavailable(PlayerIndex playerIndex)
        {
            if (_firstUpdate || _playerAvailable[playerIndex])
            {
                _playerAvailable[playerIndex] = false;
                DoPlayerUnavailable(playerIndex);
            }
        }

        /// <summary>
        /// Perform the glasses available action.
        /// </summary>
        /// <param name="pForce">Whether to skip any UI prompt and force switching to glasses.</param>
        protected virtual void DoGlassesAvailable(bool pForce = false)
        {

        }

        /// <summary>
        /// Perform the glasses unavailable action.
        /// </summary>
        protected virtual void DoGlassesUnavailable()
        {

        }

        /// <summary>
        /// Perform the player available action.
        /// </summary>
        /// <param name="playerIndex">Indicates player for the event action.</param>
        /// <param name="pForce">Whether to skip any UI prompt and force switching to glasses.</param>
        protected virtual void DoPlayerAvailable(PlayerIndex playerIndex, bool pForce = false)
        {

        }

        /// <summary>
        /// Perform the player unavailable action.
        /// </summary>
        /// <param name="playerIndex">Indicates player for the event action.</param>
        protected virtual void DoPlayerUnavailable(PlayerIndex playerIndex)
        {

        }
    }
}
