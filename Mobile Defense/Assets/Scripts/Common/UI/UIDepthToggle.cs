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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// This class listens to the space key or gamepad 'South' button and toggles all the UI elements
    /// between materials that use depth testing.
    /// </summary>
    public class UIDepthToggle : BaseDemoInput
    {
        /// <summary>
        /// The material with depth testing for UI elements, like images.
        /// </summary>
        [SerializeField] private Material _depthMaterial;
    
        /// <summary>
        /// The material with no depth testing.
        /// </summary>
        [SerializeField] private Material _noDepthMaterial;
    
        /// <summary>
        /// The font asset with depth testing for TextMeshPro elements.
        /// </summary>
        [SerializeField] private TMP_FontAsset _depthFont;
    
        /// <summary>
        /// The font asset with no depth testing for TextMeshPro elements.
        /// </summary>
        [SerializeField] private TMP_FontAsset _noDepthFont;
    
        /// <summary>
        /// Whether the UI is currently doing depth checking.
        /// </summary>
        private bool _depthChecking = false;
    
        // Start is called before the first frame update
        void Start()
        {
            // Set depth checking to false when starting the scene.
            ToggleDepthChecking(false);
        }

        // Update is called once per frame
        /// <summary>
        /// Listens to the space key or the 'South' button in the gamepad.
        /// </summary>
        void Update()
        {
            if (!_active) return;
            if (Input.GetButtonDown("ToggleDepth"))
            {
                ToggleDepthChecking();
            }
        }

        /// <summary>
        /// Toggles the depth checking opposite to its curr ent state.
        /// </summary>
        public void ToggleDepthChecking()
        {
            // Toggle depth checking to its opposite state.
            _depthChecking = !_depthChecking;

            SetDepthChecking(_depthChecking);
        }
    
        /// <summary>
        /// Toggles the depth checking to a specific true or false state.
        /// </summary>
        /// <param name="pToggle">If set to <c>true</c> p toggle.</param>
        private void ToggleDepthChecking(bool pToggle)
        {
            _depthChecking = pToggle;

            SetDepthChecking(_depthChecking);
        }

        /// <summary>
        /// Sets the depth checking.
        /// </summary>
        /// <param name="pToggle">If set to <c>true</c> p toggle.</param>
        private void SetDepthChecking(bool pToggle)
        {
            // Get all images and texts in the scene.
            Image[] images = FindObjectsOfType<Image>();
            TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();

            // If found images in the scene, change the material of the images to a material that has the depth checking state.
            if (images != null && images.Length > 0)
            {
                for (int i = 0; i < images.Length; i++)
                {
                    if (pToggle)
                    {
                        images[i].material = _depthMaterial;
                    }
                    else
                    {
                        images[i].material = _noDepthMaterial;
                    }
                }
            }

            // If found texts in the scene, change the font to a font that has the depth checking state.
            if (texts != null && texts.Length > 0)
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    if (pToggle)
                    {
                        texts[i].font = _depthFont;
                    }
                    else
                    {
                        texts[i].font = _noDepthFont;
                    }
                }
            }
        }
    }
}

