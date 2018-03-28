using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class PlayerController : MonoBehaviour
    {

        public Moving movement;
        public Shooting shoot;

        // Use this for initialization
        void Start()
        {
            movement = GetComponent<Moving>();
        }

        void Update()
        {
            Shoot();
            Movement();
        }

        void Shoot()
        {
            // Check if space is pressed
            if (Input.GetKey(KeyCode.Space))
            {
                // Fire in the direction of the player's up
                shoot.Fire();
            }
        }

        // Update is called once per frame
        void Movement()
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


            if (inputV > 0)
            {
                movement.Accelerate(transform.up);
            }

            if (inputV < 0)
            {
                movement.Accelerate(-transform.up);
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