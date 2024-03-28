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

using System;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Performs the input for the tracked object.
    /// </summary>
    public class TrackedObjectInput : BaseDemoInput
    {
        /// <summary>
        /// The rigidbody of the movable object.
        /// </summary>
        [SerializeField] private Rigidbody movableRigidbody;
    
        /// <summary>
        /// The speed modifier of the movable object.
        /// </summary>
        [SerializeField] private float speed = 9f;
        
        /// <summary>
        /// The tilt five camera transform, in order to move the object accurately depending on the camera's position.
        /// </summary>
        [SerializeField] private Transform tiltFiveCamera;
        
        /// <summary>
        /// The moving object transform, assigned from its rigidbody at Start().
        /// </summary>
        private Transform _movableObjectTransform;
        
        /// <summary>
        /// The current velocity received from input.
        /// </summary>
        private Vector2 _inputVelocity;

        /// <summary>
        /// Flag to check if we're using the wand, to account for conflicts with the Input Manager.
        /// </summary>
        private bool _wandInput = false;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            // Assign the moving object transform from its rigidbody.
            _movableObjectTransform = movableRigidbody.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_active) return;

            CheckInput();
        }

        /// <summary>
        /// Fixed update called on physics update.
        /// </summary>
        void FixedUpdate()
        {
            DoMovement();
        }

        private void CheckInput()
        {
            if (_wandInput) return;

            //Define the speed at which the object moves.
            float horizontalInput = Input.GetAxis("Horizontal");

            //Get the value of the Horizontal input axis.
            float verticalInput = Input.GetAxis("Vertical");

            //Get the value of the Vertical input axis.
            _inputVelocity = new Vector2(-horizontalInput,verticalInput);
        }

        /// <summary>
        /// Receive T5 wand input and assign it to the velocity.
        /// </summary>
        /// <param name="pInput"></param>
        public void T5WandInput(Vector2 pInput)
        {
            _wandInput = true;
            _inputVelocity = new Vector2(-pInput.x, pInput.y);
        }

        /// <summary>
        /// Stop the wand input.
        /// </summary>
        public void StopT5WandInput()
        {
            _wandInput = false;
            _inputVelocity = Vector2.zero;
        }

        /// <summary>
        /// Stop the wand input.
        /// </summary>
        public override void StopAllT5Input()
        {
            StopT5WandInput();
            base.StopAllT5Input();
        }

        /// <summary>
        /// Performs the object's movement.
        /// </summary>
        private void DoMovement()
        {
            // Check whether the movement magnitude is higher than 0
            if (_inputVelocity.magnitude > 0f)
            {
                // Get the moving object's and the camera's position.
                Vector3 movableObjectPosition = _movableObjectTransform.position;
                Vector3 cameraPosition = tiltFiveCamera.position;
                
                // Create a vector3 for the moving object's position, but ignoring its height (Y axis).
                Vector3 movableObjectPositionPlane = new Vector3(
                    movableObjectPosition.x,
                    0f,
                    movableObjectPosition.z
                );

                // Create a vector3 for the camera's position, but ignoring its height (Y axis).
                Vector3 cameraPositionPlane =
                    new Vector3(
                        cameraPosition.x,
                        0f,
                        cameraPosition.z);
                
                // Get the camera's direction towards the moving object.
                Vector3 cameraBoardDirection = Vector3.Normalize(movableObjectPositionPlane - cameraPositionPlane);
                
                // Convert input value to 3-Dimensional space.
                Vector3 translateValuePlane = new Vector3(_inputVelocity.x, 0f, _inputVelocity.y);
                
                // To get the correct movement direction from the camera to the moving object using input values,
                // get the cross product between the camera direction and the input direction, and use its Y value as the X value for our new direction,
                // and then get the dot product between the camera direction and the input direction, and use that as the Z direction.
                // Remember to invert the X axis of the input value in order for the function to work correctly.
                Vector3 cross = Vector3.Cross(translateValuePlane, cameraBoardDirection);
                float dot = Vector3.Dot(cameraBoardDirection,translateValuePlane);
                Vector3 result = Vector3.Normalize(new Vector3(cross.y, 0f, dot));

                // Add the force to the rigidbody, let rigidbody handle movement and physical interactions.
                movableRigidbody.AddForce(result * speed * Time.deltaTime);
            }
        }
    }
}
