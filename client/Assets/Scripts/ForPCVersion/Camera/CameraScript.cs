using System;
using UnityEngine;

namespace QuantomCOMPPC
{
    public class CameraScript : MonoBehaviour
    {
        public float speedH = 2.0f;
        public float speedV = 2.0f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;

        private bool enableRotation = false;
        private bool movefb = false;
        private bool moverl = false;
        private float movementdirectionfb;
        private float movementdirectionrl;

        void Update()
        {
            
            if (Input.GetMouseButtonDown(1))
            {
                enableRotation = !enableRotation;
            }

            if (Input.GetKeyDown("w"))
            {
                movefb = true;
                movementdirectionfb = 0.5f;
            }

            if (Input.GetKeyDown("s"))
            {
                movefb = true;
                movementdirectionfb = -0.5f;
            }

            if (movefb)
                transform.position += this.transform.forward * movementdirectionfb * Time.deltaTime;

            if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
            {
                movefb = false;
            }


            if (Input.GetKeyDown("d"))
            {
                moverl = true;
                movementdirectionrl = 0.5f;
            }

            if (Input.GetKeyDown("a"))
            {
                moverl = true;
                movementdirectionrl = -0.5f;
            }

            if (moverl)
                transform.position += this.transform.right * movementdirectionrl * Time.deltaTime;

            if (Input.GetKeyUp("d") || Input.GetKeyUp("a"))
            {
                moverl = false;
            }

            if (enableRotation)
            {
                rotateCamera();
            }
        }

        private void rotateCamera()
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

}
