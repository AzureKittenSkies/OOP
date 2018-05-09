using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        // Speed ball travels
        public float speed = 5f;
        // Velocity of the ball (Direction x speed)
        private Vector3 velocity;

        // Fires off ball in a given direction
        public void Fire(Vector3 direction)
        {
            // Calculate velocity
            velocity = direction * speed;
        }

        // Detect collisions
        void OnCollisionEnter(Collision other)
        {
            // Grab contact points of collision
            ContactPoint contact = other.contacts[0];
            // Calculate the reflection point of the ball using velocity and contact normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            // Calculate new velocity from reflection multiply by the same speed (velocity.magnitude)
            velocity = reflect.normalized * velocity.magnitude;
            if (!other.gameObject.CompareTag("Player"))
            {
                Destroy(other.gameObject);
            }

        }

        private void Update()
        {
            // Moves ball using velocity and deltaTime
            transform.position += velocity * Time.deltaTime;

        }
    }
}