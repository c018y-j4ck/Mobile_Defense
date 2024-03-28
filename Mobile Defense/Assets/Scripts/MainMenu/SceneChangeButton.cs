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

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TiltFiveDemos
{
    /// <summary>
    /// Class to change the scene on button callback.
    /// </summary>
    public class SceneChangeButton : MonoBehaviour
    {
        /// <summary>
        /// Name of the scene to change to.
        /// </summary>
        [SerializeField] private string _sceneName;

        /// <summary>
        /// Change the scene on button callback.
        /// </summary>
        public void ChangeScene()
        {
            PlayerPrefs.SetString("Last_Scene",this.gameObject.name); // Save the name of this game object for reloading when we return.
            SceneManager.LoadScene(_sceneName);
        }
    }
}
