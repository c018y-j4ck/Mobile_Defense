using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Simple class to perform a color change, inherite methods from ViewPointerInteractableBase
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class ViewPointerInteractableColor : ViewPointerInteractableBase
    {
        /// <summary>
        /// The colors to cycle through
        /// </summary>
        [SerializeField]
        private Color[] _colors;

        /// <summary>
        /// The normal object material
        /// </summary>
        [SerializeField]
        private Material _normalMaterial;

        /// <summary>
        /// The object's material when it's highlighted
        /// </summary>
        [SerializeField] 
        private Material _highlightedMaterial;

        /// <summary>
        /// The mesh renderer
        /// </summary>
        private MeshRenderer _renderer;

        /// <summary>
        /// Initial counter
        /// </summary>
        private int _counter = 0;

        /// <summary>
        /// Set the color as the starting color.
        /// </summary>
        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = _normalMaterial;
            _renderer.material.color = _colors[_counter]; // Set the first color
        }

        /// <summary>
        /// Perform the interaction.
        /// </summary>
        public override void DoInteraction()
        {
            _counter++;

            if(_counter >= _colors.Length)
            {
                _counter = 0; // Reset counter if larger than the colors array.
            }

            _renderer.material.color = _colors[_counter];

            base.DoInteraction();
        }

        /// <summary>
        /// Perform the highlight (changing the material to one with outline)
        /// </summary>
        public override void DoHighlight()
        {
            _renderer.material = _highlightedMaterial;
            _renderer.material.color = _colors[_counter];
            base.DoHighlight();
        }

        /// <summary>
        /// Stop (changing the material to a normal one)
        /// </summary>
        public override void StopHighlight()
        {
            _renderer.material = _normalMaterial;
            _renderer.material.color = _colors[_counter];
            base.StopHighlight();
        }
    }
}
