using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TiltFiveDemos
{
    [ExecuteInEditMode]
    /// <summary>
    /// Represent the pointer with a parabolic curve, and draw it in front of the wand.
    /// </summary>
    public class WandPointerArc : WandPointer
    {
        private Vector3 _endpoint;
        [SerializeField]
        private float _extensionFactor = 0f;
        [SerializeField]
        private float _extendStep = 5f;
        [SerializeField]
        private int _segmentCount = 50;
        [SerializeField]
        private float _upDistance = 50f;
        [SerializeField]
        private float _downDistance = 100f;
        private Vector3[] _controlPoints = new Vector3[3];

        protected override void Start()
        {
            _controlPoints = new Vector3[3];
            base.Start();
        }

        private new void Update()
        {
            if(_active)
            {
                UpdateControlPoints();
                HandleExtension();
                DrawCurve();
                HandleInput();
            }
        }

        /// <summary>
        /// Handle the inputs for increase and decrease arc.
        /// </summary>
        private void HandleInput()
        {
            if (TiltFive.Input.GetButton(TiltFive.Input.WandButton.One) || Input.GetButton("IncreaseArc")) IncreaseArc();
            if (TiltFive.Input.GetButton(TiltFive.Input.WandButton.Two) || Input.GetButton("DecreaseArc")) DecreaseArc();
        }

        /// <summary>
        /// Designate the curve extension based on the extension factor
        /// </summary>
        void HandleExtension()
        {
            if (_extensionFactor == 0f)
                return;

            float finalExtension = _extendStep + Time.deltaTime * _extensionFactor * 2f;
            _extendStep = Mathf.Clamp(finalExtension, 2.5f, 7.5f);
        }

        /// <summary>
        /// The first control is the remote. The second is a forward projection. The third is a forward and downward projection.
        /// </summary>
        void UpdateControlPoints()
        {
            _controlPoints = new Vector3[3];
            _controlPoints[0] = _pointerOrigin.position; // Get Controller Position
            _controlPoints[1] = _controlPoints[0] + (gameObject.transform.forward * _extendStep * 2f / 5f) + Vector3.up * _upDistance;
            _controlPoints[2] = _controlPoints[1] + (gameObject.transform.forward * _extendStep * 3f / 5f) + (Vector3.up * -_downDistance);
        }

        /// <summary>
        /// Draw the bezier curve using the control points
        /// </summary>
        void DrawCurve()
        {
            if (!_line.enabled)
                return;

            _line.enabled = true;
            _line.positionCount = 1;
            _line.SetPosition(0, _line.transform.InverseTransformPoint(_pointerOrigin.transform.position));
            bool hitCanvas = false;

            Vector3 prevPosition = _controlPoints[0];
            Vector3 nextPosition = prevPosition;

            for (int i = 1; i <= _segmentCount; i++)
            {
                if (i == _segmentCount) break;

                float t = i / (float)_segmentCount;

                _line.positionCount = i + 1;
                nextPosition = CalculateBezierPoint(t, _controlPoints[0], _controlPoints[1], _controlPoints[2]);

                // Perform the raycast
                Ray ray = new Ray(prevPosition, (nextPosition - prevPosition).normalized);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Vector3.Distance(prevPosition, nextPosition), _uILayers))
                {
                    Vector3 hitPoint = hit.point;
                    hitCanvas = true;
                    _endpoint = hit.point;
                    _line.SetPosition(i, _line.transform.InverseTransformPoint(_endpoint));

                    GraphicRaycaster raycaster = hit.collider.GetComponent<GraphicRaycaster>();

                    if (raycaster != null)
                    {
                        if (_pointer != null)
                        {
                            _pointer.SetActive(true);
                            _pointer.transform.position = hit.point;
                            _pointer.transform.rotation = raycaster.transform.rotation;
                        }

                        CheckCanvas(raycaster, hitPoint);
                    }
                }

                if (hitCanvas)
                { // If the segment intersects a surface, draw the point and return.
                    _line.SetPosition(i, _line.transform.InverseTransformPoint(_endpoint));
                    break;
                }
                else
                { // If the point does not intersect, continue to draw the curve.
                    _line.SetPosition(i, _line.transform.InverseTransformPoint(nextPosition));
                    prevPosition = nextPosition;
                }
            }

            // Deselect the last selectable if a canvas wasn't found.
            if (!hitCanvas)
            {
                if (_pointer != null)
                {
                    _pointer.SetActive(false);
                }
                
                DeselectCurrentSelectable();
            }
        }
        
        /// <summary>
        /// Use the bezier control points to calculate the next step in the curve.
        /// https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Cubic_B%C3%A9zier_curves
        /// </summary>
        /// <param name="t">Time Step</param>
        /// <param name="p0">Point 0</param>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        /// <returns></returns>
        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            return Mathf.Pow((1f - t), 2) * p0 + 
                2f * (1f - t) * t * p1 + 
                Mathf.Pow(t, 2) * p2;
        }

        /// <summary>
        /// Increase the length of the arc.
        /// </summary>
        private void IncreaseArc()
        {
            _upDistance = Mathf.Clamp(_upDistance + (3f * Time.deltaTime),-10f,10f);
        }

        /// <summary>
        /// Decrease the length of the arc.
        /// </summary>
        private void DecreaseArc()
        {
            _upDistance = Mathf.Clamp(_upDistance - (3f * Time.deltaTime), -10f, 10f);
        }
    }
}
