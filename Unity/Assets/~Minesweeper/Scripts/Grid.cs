using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
    public class Grid : MonoBehaviour
    {
        #region Variables
        public GameObject tilePrefab;
        public int width = 10, height = 10;
        public float spacing = 0.155f;

        public AudioSource audio;

        private Tile[,] tiles;
        #endregion

        void Start()
        {
            GenerateTiles();
        }

        void Update()
        {
            // check if mouse button is pressed 
            if (Input.GetMouseButtonDown(0))
            {
                // run the method for selecting tiles
                SelectATile();
            }
        }

        // funcionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.parent = transform;
            // edit its properties
            clone.transform.position = pos;
            Tile currentTile = clone.GetComponent<Tile>();
            // return it
            return currentTile;
        }

        // Spawns tiles in a grid like pattern
        void GenerateTiles()
        {
            // create a new 2D array of size width x height
            tiles = new Tile[width, height];
            // loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // store half size for later use
                    Vector2 halfSize = new Vector2(width * 0.5f, height * 0.5f);
                    // pivot tiles around grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);
                    // realign grid
                    Vector2 offset = new Vector2(0.5f, 0.5f);
                    pos += offset;
                    // apply spacing 
                    pos *= spacing;

                    // spawn tile using spawn function made earlier
                    Tile tile = SpawnTile(pos);
                    // attatch newly spawned tile to self (transform)
                    tile.transform.SetParent(transform);
                    // store its array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // store tile in array at those coordinates
                    tiles[x, y] = tile; 
                    
                }
            }
        }

        public int GetAdjacentMineCount(Tile tile)
        {
            // set count to 0
            int count = 0;
            // loop through all the adjacent tiles on the x
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // calculate which adjacent tile to look at 
                    int desiredX = tile.x + x;
                    int desiredY = tile.y + y;
                    // check if the desired x & y is outside bounds
                    if (desiredX < 0 || desiredX >= width || desiredY < 0 || desiredY >= height)
                    {
                        // continue to next element in loop
                        continue;
                    }
                    // select current tile
                    Tile currentTile = tiles[desiredX, desiredY];
                    // check if that tile is a mine
                    if (currentTile.isMine)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        void SelectATile()
        {
            // play audio clip
            audio.Play();
            // generate a ray from teh camera with mouse position
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // perform raycast
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            // if the mouse hit something
            if (hit.collider != null)
            {
                // try getting  a tile component from the thing we hit
                Tile hitTile = hit.collider.GetComponent<Tile>();
                // check if the thing it hit was a tile
                if (hitTile != null)
                {
                    // get a count of all mines around the hit tile
                    int adjacentMines = GetAdjacentMineCount(hitTile);
                    // reveal what that hit tile is
                    hitTile.Reveal(adjacentMines);
                }
            }
            
        }

    }
}