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
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Creates a selectable menu that then selects the last object of the menu.
    /// Assign the last selected object using a trigger event.
    /// Select this menu instead of selecting the buttons so that we can return to the last selected button.
    /// </summary>
    public class SelectableMenu : Selectable
    {
        /// <summary>
        /// The first selected object.
        /// </summary>
        [SerializeField]
        private GameObject _firstSelected;

        /// <summary>
        /// The current selected object.
        /// </summary>
        private GameObject _currentSelected;

        protected override void Start()
        {
            // If the first selected object is assigned, then assing the first selected object as the current one.
            if (_firstSelected != null)
            {
                _currentSelected = _firstSelected;
            }

            base.Start();
        }

        /// <summary>
        /// Callback to set the selected button as the current selected object in this class.
        /// Use an event trigger to call back to this function.
        /// </summary>
        /// <param name="pEventData"></param>
        public void SetSelected(BaseEventData pEventData)
        {
            _currentSelected = pEventData.selectedObject;
        }

        /// <summary>
        /// Override on select to proceed to select the current selected button.
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            StartCoroutine(WaitToSelectLastObject());
        }

        /// <summary>
        /// Coroutine that waits for a frame to pass so that there's no conflict with the event system trying to select this menu and the next object.
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitToSelectLastObject()
        {
            yield return new WaitForEndOfFrame();

            if (_currentSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(_currentSelected);
            }
        }
    }
}
