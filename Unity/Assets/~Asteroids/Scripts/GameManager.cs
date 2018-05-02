using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        public Text scoreText;
        private int score;

        void Start()
        {
            // If there are no current instances of GameManager
            if (Instance == null)
            {
                // Set instance to this GameManager
                Instance = this;
            }
            else
            {
                // Destroy the new instance of GameManager
                Destroy(gameObject);
            }
        }


        void Update()
        {
            scoreText.text = "Score: " + score.ToString();
        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
        }



    }
}