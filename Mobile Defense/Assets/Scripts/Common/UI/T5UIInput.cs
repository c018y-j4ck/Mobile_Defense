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

namespace TiltFiveDemos
{
    /// <summary>
    /// Class for connecting to the event system and sending inputs with actions assigned in the T5InputReceiver class.
    /// </summary>
    public class T5UIInput : MonoBehaviour
    {
        /// <summary>
        /// Use a singleton pattern to keep this object running on all scenes of the game.
        /// </summary>
        private static T5UIInput _instance;

        public static T5UIInput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T5UIInput>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("T5UIInput");
                        _instance = container.AddComponent<T5UIInput>();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Initialize the singleton, remove itself if this is not the only one present in the scene.
        /// </summary>
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

        /// Assign the events on the T5InputReceiver class.

        /// <summary>
        /// On move down.
        /// </summary>
        public void OnMoveDown()
        {
            Move(MoveDirection.Down);
        }

        /// <summary>
        /// On move up.
        /// </summary>
        public void OnMoveUp()
        {

            Move(MoveDirection.Up);
        }

        /// <summary>
        /// On move left.
        /// </summary>
        public void OnMoveLeft()
        {

            Move(MoveDirection.Left);
        }

        /// <summary>
        /// On move right.
        /// </summary>
        public void OnMoveRight()
        {

            Move(MoveDirection.Right);
        }

        /// <summary>
        ///  Move the UI to the selected direction by connecting to the current EventSystem.
        /// </summary>
        /// <param name="direction">The direction to move the UI.</param>
        private void Move(MoveDirection direction)
        {
            // Check that there's an event system present in the scene.
            if (EventSystem.current != null)
            {
                // Create new axis event and manually assign the move direction.
                AxisEventData data = new AxisEventData(EventSystem.current);

                data.moveDir = direction;

                // Execute the new movement event from the selected object.
                data.selectedObject = EventSystem.current.currentSelectedGameObject;

                ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
            }
        }

        /// <summary>
        /// On clicking the UI.
        /// </summary>
        public void OnClick()
        {
            // Check that there's an event system present in the scene.
            if (EventSystem.current != null)
            {
                // Create a pointer event data and execute on the current event system.
                PointerEventData data = new PointerEventData(EventSystem.current);

                data.selectedObject = EventSystem.current.currentSelectedGameObject;

                ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.submitHandler);
            }
        }
    }
}
