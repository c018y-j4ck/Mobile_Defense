using System.Collections;
using System.Collections.Generic;
using TiltFive;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// A simple classes to toggle the glasses from an options menu.
    /// </summary>
    public class GlassesToggler : MonoBehaviour
    {
        private TiltFiveManager _tiltFiveManager;

        // Start is called before the first frame update
        void Start()
        {
            _tiltFiveManager = FindObjectOfType<TiltFiveManager>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// Toggle the glasses manager
        /// </summary>
        public void ToggleGlasses()
        {
            _tiltFiveManager.enabled = !_tiltFiveManager.enabled;
        }
    }
}
