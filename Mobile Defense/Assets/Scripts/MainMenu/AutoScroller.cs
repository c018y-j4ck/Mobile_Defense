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
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Use event triggers to make the scroll rect automatically scroll to the selected object.
    /// All selectable buttons inside the scroll view content have an Event Trigger with a callback to OnSelected().
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class AutoScroller : MonoBehaviour
    {
        /// <summary>
        /// The speed of scrolling.
        /// </summary>
        [SerializeField]
        private float _scrollSpeed = 12f;

        /// <summary>
        /// The scroll container.
        /// </summary>
        private ScrollRect _scrollRect;

        /// <summary>
        /// Get the scroll container.
        /// </summary>
        private void Start()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        /// <summary>
        /// Find vertical normalized position to scroll to.
        /// </summary>
        /// <param name="pEventData"></param>
        public void OnSelected(BaseEventData pEventData)
        {
            float localPosition = pEventData.selectedObject.transform.localPosition.y;

            float objectHeight = pEventData.selectedObject.GetComponent<RectTransform>().sizeDelta.y;

            float contentHeight = _scrollRect.content.sizeDelta.y;

            // The position of the game objects inside the content view is inverted. Simply add the content height to the position to reach the correct local position.
            float fixedPosition = localPosition + contentHeight;

            // If the height of the object is outside the bounds of the container, scroll to the very edges of the container.
            if (fixedPosition + objectHeight > contentHeight)
            {
                fixedPosition = contentHeight;
            }
            else if (fixedPosition - objectHeight < 0f)
            {
                fixedPosition = 0f;
            }

            float normalizedPosition = (1f / contentHeight) * fixedPosition;

            // Make sure the normalized position never goes over 1 or below 0
            if (normalizedPosition < 0f)
            {
                normalizedPosition = 0f;
            }
            else if (normalizedPosition > 1f)
            {
                normalizedPosition = 1f;
            }

            AutoScroll(normalizedPosition);
        }

        /// <summary>
        /// The auto scroll coroutine.
        /// Stored so we can stop it if we scroll to a new element.
        /// </summary>
        private Coroutine _autoScrollCoroutine;

        /// <summary>
        /// Call the autoscroll coroutine and stop it if it's already running.
        /// </summary>
        /// <param name="pTarget"></param>
        private void AutoScroll(float pTarget)
        {
            // Stop the coroutine if we're already auto scrolling.
            if (_autoScrollCoroutine != null)
            {
                StopCoroutine(_autoScrollCoroutine);
                _autoScrollCoroutine = null;
            }

            _autoScrollCoroutine = StartCoroutine(AutoScrollCoroutine(pTarget));
        }

        /// <summary>
        /// Do the autoscroll coroutine interpolating between the current position and the targer position.
        /// </summary>
        /// <param name="pTarget"></param>
        /// <returns></returns>
        private IEnumerator AutoScrollCoroutine(float pTarget)
        {
            float normal = 0f;

            float originalVerticalPosition = _scrollRect.verticalNormalizedPosition;

            while (normal < 1f)
            {
                normal += Time.deltaTime * _scrollSpeed;

                // Use Mathf.Lerp to interpolate between the two positions.
                _scrollRect.verticalNormalizedPosition = Mathf.Lerp(originalVerticalPosition, pTarget, normal);

                yield return null;
            }

            _scrollRect.verticalNormalizedPosition = pTarget;
        }

    }
}
