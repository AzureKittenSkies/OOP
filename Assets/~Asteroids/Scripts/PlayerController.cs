using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class PlayerController : MonoBehaviour
    {

        public Moving movement;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            // linear interpolation;
            // -1   == A || LeftArrow
            //  0   == Not pressed
            //  1   == D || RightArrow

            float inputV = Input.GetAxis("Vertical");
            // More linear interpolation;
            // -1   == S || DownArrow
            //  0   == Nothing pressed
            //  1   == W || UpArrow


            if (inputV == 1)
            {
                movement.Accelerate(transform.up);
            }

            if (Input.GetKey(KeyCode.A))
            {
                movement.RotateRight();
            }

            if (Input.GetKey(KeyCode.D))
            {
                movement.RotateLeft();
            }

        }
    }
}