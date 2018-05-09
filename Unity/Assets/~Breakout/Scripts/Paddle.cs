using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public Ball currentBall;
        public bool ballIsChild = true;
        // Directiosn array defaults to two values
        public Vector2[] directions = new Vector2[]
        {
            new Vector2(-0.5f, 0.5f),
            new Vector2(0.5f, 0.5f)
        };

        // Use this for initialization
        void Start()
        {
            // Grabs currentBall from children of the Paddle
            currentBall = GetComponentInChildren<Ball>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            Movement();
        }

        void Fire()
        {
            if (ballIsChild)
            {
                // Detatch as child
                currentBall.transform.SetParent(null);
                // Generate random dir from list of directions
                Vector3 randomDir = directions[Random.Range(0, directions.Length)];
                // fire off ball in randomDirection
                currentBall.Fire(randomDir);
                ballIsChild = false;
            }

        }

        void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }

        void Movement()
        {
            // Get input on the horizontal axis
            float inputH = Input.GetAxis("Horizontal");
            // Set force to direction (inputH to decide which direction)
            Vector3 force = transform.right * inputH;
            // Apply movement speed to force
            force *= movementSpeed;
            // Apply delta time to force
            force *= Time.deltaTime;
            // Add forve to position
            transform.position += force;
        }
    }
}