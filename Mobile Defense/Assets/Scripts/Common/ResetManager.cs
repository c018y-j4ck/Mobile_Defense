using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TiltFiveDemos
{
    /// <summary>
    /// A reset manager class that finds all reseateable elements in the scene and calls reset.
    /// </summary>
    public class ResetManager : MonoBehaviour
    {
        /// <summary>
        /// The reseteable elements.
        /// </summary>
        private ReseteableElement[] _reseteableElements;

        // Start is called before the first frame update
        void Start()
        {
            _reseteableElements = FindObjectsOfType<ReseteableElement>();
        }

        /// <summary>
        /// Reset on all reseteable elements
        /// </summary>
        public void DoReset()
        {
            foreach (ReseteableElement element in _reseteableElements)
            {
                element.DoReset();
            }
        }
    }
}
