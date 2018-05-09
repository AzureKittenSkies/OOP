using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breakout
{
    public class BrickSpawner : MonoBehaviour
    {
        public GameObject brickA;
        public static GameObject brickB;

        public int brickScaleX = 2;
        public int brickScaleY = 1;

        void Awake()
        {
            for (int i = 0; i < Screen.width; i += brickScaleX)
            {
                for (int j = 0; j < Screen.width / 2; j += brickScaleY)
                {
                    Vector3 newBrick = new Vector3(i, j);
                    Instantiate(brickA, newBrick, Quaternion.identity);
                }
            }

        }


    }
}