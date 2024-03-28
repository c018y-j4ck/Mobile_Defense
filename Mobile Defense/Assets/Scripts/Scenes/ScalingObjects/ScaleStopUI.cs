using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    public class ScaleStopUI : MonoBehaviour
    {
        /// <summary>
        /// The image of the stop UI.
        /// </summary>
        [SerializeField] private Image _image;

        /// <summary>
        /// The selected sprite.
        /// </summary>
        [SerializeField] private Sprite _unselectedSprite;

        /// <summary>
        /// The unselected sprite.
        /// </summary>
        [SerializeField] private Sprite _selectedSprite;

        /// <summary>
        /// Activate the UI indicator.
        /// </summary>
        public void Activate()
        {
            _image.sprite = _selectedSprite;
        }

        /// <summary>
        /// Deactivate the UI indicator.
        /// </summary>
        public void Deactivate()
        {
            _image.sprite = _unselectedSprite;
        }
    }
}
