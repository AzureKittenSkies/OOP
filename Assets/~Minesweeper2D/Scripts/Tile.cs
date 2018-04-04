using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minesweeper2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        #region Variables

        //store x and y coordinates in array for later use
        public int x = 0;
        public int y = 0;
        // is the current tile a mine?
        public bool isMine = false;
        // has the tile already been revealed?
        public bool isRevealed = false;

        [Header("References")]
        // list of empty sprites ie; empty, 1, 2, 3, 4, etc
        public Sprite[] emptySprites;
        // the mine sprites
        public Sprite[] mineSprites;

        // reference to the sprite renderer
        private SpriteRenderer rend; 
        #endregion

        void Awake()
        {
            // grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }


        // Use this for initialization
        void Start()
        {
            // randomly decide if it's a mine or not
            isMine = Random.value < .05f;
        }

        public void Reveal(int adjacentMine, int mineState = 0)
        {
            // flags the tile as eing revealed
            isRevealed = true;
            // checks if tile is a mine
            if (isMine)
            {
                // sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // sets sprite to appropriate textture based on adjacent mines
                rend.sprite = emptySprites[adjacentMine];
            }
        }

    }

}