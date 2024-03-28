using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    /// <summary>
    /// Select with the direction directly from the glasses to the wand.
    /// </summary>
    public class WandPointerView : WandPointer
    {
        private new void Update()
        {
            if(_active)
            {
                StartRaycast();
            }
        }

        /// <summary>
        /// Perform a raycast using the direction from the glasses to the wand.
        /// </summary>
        protected override void StartRaycast()
        {
            Vector3 direction = (_pointerOrigin.position - _glassesCamera.transform.position).normalized;

            DoRaycast(direction);

            base.StartRaycast();
        }
    }
}
