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
                        SelectTile(hitTile);
                    }
                }
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

        void FFuncover(int x, int y, bool[,] visited)
        {
            // is x and y within bounds of the grid
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                // have these cooridnates ben visited?
                if (visited[x, y])
                {
                    return;
                }
                // reveal tile in that x an y coodinate
                Tile tile = tiles[x, y];
                int adjacentMines = GetAdjacentMineCount(tile);
                tile.Reveal(adjacentMines);

                // if there were no adjacent mines around that tile
                if (adjacentMines == 0)
                {
                    // this tile has been visited
                    visited[x, y] = true;
                    FFuncover(x - 1, y, visited);
                    FFuncover(x + 1, y, visited);
                    FFuncover(x, y - 1, visited);
                    FFuncover(x, y + 1, visited);
                }
            }
        }

        // uncovers all mines in the grid
        void UncoverMines(int mineState = 0)
        {
            // loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    // check if tile is a mine
                    if (tile.isMine)
                    {
                        // reveal that tile
                        int adjacentMines = GetAdjacentMineCount(tile);
                        tile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        // scans the grid to check if there are no more empty tiles
        bool NoMoreEmptyTiles()
        {
            // set empty tile count to zero
            int emptyTileCount = 0;
            // loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    // if tile is not revealed and not a mine
                    if (!tile.isRevealed && !tile.isMine)
                    {
                        // we found an empty tile 
                        emptyTileCount += 1;
                    }
                }
            }
            // if there are empty tiles return true
            // if there are no empty tiles return fales
            return emptyTileCount == 0;
        }

        // uncovers a selected tile
        void SelectTile(Tile selected)
        {
            int adjacentMines = GetAdjacentMineCount(selected);
            selected.Reveal(adjacentMines);

            // is the selected tiule a mine?
            if (selected.isMine)
            {
                // uncover all mines 
                UncoverMines();
            }

            // otherwise are there no mines around this tile?
            else if (adjacentMines == 0)
            {
                int x = selected.x;
                int y = selected.y;
                // then use flood fill to uncover all adjacent mines
                FFuncover(x, y, new bool[width, height]);
            }

            // are there no more empty tiles in the game at this point?
            if (NoMoreEmptyTiles())
            {
                // uncover all mines, with the win state 1
                UncoverMines(1);
                // win
            }
        }


    }
}