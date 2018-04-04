using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {

        #region Variables
        // get prefab for tiles
        public GameObject tilePrefab;
        // default grid width
        public int width = 10;
        // default grid height
        public int height = 10;
        // space between tiles
        public float spacing = .155f;

        // stores generated tiles
        private Tile[,] tiles;

        #endregion

        // Use this for initialization
        void Start()
        {
            // generate tiles on startup
            GenerateTiles();
        }

        void Update()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            if (hit.collider != null)
            {
                Tile hitTile = hit.collider.GetComponent<Tile>();
                if (hitTile != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        int adjacentMines = GetAdjacentMineCount(hitTile);
                        hitTile.Reveal(adjacentMines);
                    }
                }
            }
        }

        // functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; // position tile
            Tile currentTile = clone.GetComponent<Tile>(); // get tile component
            return currentTile;
        }

        // spawns tiles in a grid like pattern
        void GenerateTiles()
        {
            // create new 2D array of size width x height
            tiles = new Tile[width, height];

            //loop through entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // store halfsize for later use
                    Vector2 halfSize = new Vector2((width / 2) , (height / 2));
                    // pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfSize.x -.5f, y - halfSize.y - .5f);
                    // apply spacing
                    pos *= spacing;

                    // spawn the tile
                    Tile tile = SpawnTile(pos);
                    // attach newly spawned tile to
                    tile.transform.SetParent(transform);
                    // store its array coords within itself for future reference
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
            // loop through all adjacent tiles on the x
            for (int x = -1; x <= 1; x++)
            {
                // loop through all adjacent tiles on the y
                for (int y = -1; y <= 1; y++)
                {
                    // calculate which adjacent tile to look at
                    int desiredX = tile.x + x;
                    int desiredY = tile.y + y;

                    if (desiredX < 0 || desiredX > tiles.Length)
                    {

                    }

                    // select current tile
                    Tile currentTile = tiles[desiredX, desiredY];

                    // check to see if that tile is a mine
                    if (currentTile.isMine)
                    {
                        // increment count by 1
                        count++;
                    }
                }
            }
            return count;
        }

    }
}