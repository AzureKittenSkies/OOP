using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        #region Variables
        public int x, y;
        public bool isMine = false; // is the current tile a mine?
        public bool isRevealed = false;// has the tile already been revealed?

        [Header("References")]
        public Sprite[] emptySprites; // List of empty sprites i.e. empty, 1, 2, 3, etc...
        public Sprite[] mineSprites; // The mine sprites
        private SpriteRenderer rend; // references to sprite renderer
        #endregion

        void Awake()
        {
            // Grab refernce to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            // Randomly decide if this tile is a mine - using a 5% chance
            isMine = Random.value < 0.05f;
        }

        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // flags the tile as being revealed
            isRevealed = true;
            // checks if tile is a mine
            if (isMine)
            {
                // sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }





    }
}