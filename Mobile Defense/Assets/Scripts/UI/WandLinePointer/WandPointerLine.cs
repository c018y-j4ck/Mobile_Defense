using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Select using forward direction from the wand tip.
    /// </summary>
    public class WandPointerLine : WandPointer
    {
        private new void Update()
        {
            if (_active)
            {
                StartRaycast();
            }
        }

        /// <summary>
        /// Perform a raycast using the forward direction from the wand tip.
        /// </summary>
        protected override void StartRaycast()
        {
            Vector3 direction = _pointerOrigin.forward;

            DoRaycast(direction);

            base.StartRaycast();
        }
    }
}
