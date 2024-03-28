using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// An element that can be toggled on and off.
    /// Used for when outside elements need to toggle it independently of their type.
    /// </summary>
    public class ToggleableElement : MonoBehaviour
    {
        public void Toggle(bool pToggle)
        {
            gameObject.SetActive(pToggle);
        }
    }
}
