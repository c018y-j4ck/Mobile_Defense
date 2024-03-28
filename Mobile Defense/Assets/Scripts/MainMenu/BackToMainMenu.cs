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
    /// Listens for a specific key press and returns the player to the main menu.
    /// </summary>
    public class BackToMainMenu : MonoBehaviour
    {
        /// <summary>
        /// Use a simple singleton pattern to keep this object running on all scenes of the game.
        /// </summary>
        private static BackToMainMenu _instance;
    
        public static BackToMainMenu Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<BackToMainMenu>();
             
                    if (_instance == null)
                    {
                        GameObject container = new GameObject("BackToMainMenu");
                        _instance = container.AddComponent<BackToMainMenu>();
                    }
                }
     
                return _instance;
            }
        }
    
        /// <summary>
        /// The main menu scene
        /// </summary>
        private const string MAIN_MENU_SCENE = "MainMenu";
 
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

        /// <summary>
        /// Update this instance.
        /// </summary>
        private void Update()
        {
            if (Input.GetButtonDown("Start"))
            {
                GoToMainMenu();
            }
        }

        /// <summary>
        /// Load the main menu scene.
        /// </summary>
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
        }
    }
}
