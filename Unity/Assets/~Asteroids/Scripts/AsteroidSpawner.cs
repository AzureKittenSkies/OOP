using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        public GameObject[] prefabs;

        public float minSpeed = 1f;
        public float maxSpeed = 5f;

        public float spawnDelay = 1f; 
        private float spawnTimer = 0f;

        //Camera
        private Bounds camBounds;
        private float camWidth, camHeight;


        void Start()
        {
            // Calculate camera bounds 
            Camera cam = Camera.main;
            camHeight = 2f * cam.orthographicSize;
            camWidth = camHeight * cam.aspect;
            camBounds = new Bounds(cam.transform.position, new Vector2(camWidth, camHeight));
        }

        void Update()
        {
            //Count up the timer
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                // Spawn the new asteroid
                Spawn();
                spawnTimer = 0;
            }
        }

        void Spawn()
        {
            Vector3 randomPoint = GenerateRandomPoint();
            int randomIndex = Random.Range(0, prefabs.Length);

            GameObject asteroid = Instantiate(prefabs[randomIndex]);
            asteroid.transform.position = randomPoint;

            Vector2 randomDir = Random.onUnitSphere;
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            Rigidbody2D rigid = asteroid.GetComponent<Rigidbody2D>();
            rigid.AddForce(randomDir * randomSpeed, ForceMode2D.Impulse);
        }

        public Vector3 GenerateRandomPoint()
        {
            Vector3 position = Vector3.zero;
            float halfWidth = camWidth * 0.5f;
            float halfHeight = camHeight * 0.5f;
            // Top/bottom (true)
            if (Random.Range(0,2)>0)
            {
                position.x = Random.Range(-halfWidth, halfWidth);
                // top (true)
                if (Random.Range(0,2) > 0)
                {
                    position.y = halfHeight;
                }
                else
                {
                    position.y = -halfHeight;
                }
            }
            else // or left/right (false)
            {
                position.y = Random.Range(-halfHeight, halfHeight);
                // left (true)
                if (Random.Range(0, 2) > 0)
                {
                    position.x = halfWidth;
                }
                else // right (false)
                {
                    position.x = -halfWidth;
                }
            }
            return position;
        }
        

    }
}