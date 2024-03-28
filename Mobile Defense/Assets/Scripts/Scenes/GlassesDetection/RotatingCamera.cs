using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Rotate the camera around a target
    /// </summary>
    public class RotatingCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _referenceTarget;
        [SerializeField]
        private float _speed = 2f;

        // Start is called before the first frame update
        void Start()
        {
            transform.LookAt(_referenceTarget);
        }

        // Update is called once per frame
        void Update()
        {
            // Move the camera to the right while constantly turning to look at the target;
            transform.LookAt(_referenceTarget);
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
    }
}
