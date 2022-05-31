using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int width, height;

    [SerializeField] private Tile grassTile, mountainTile;

    [SerializeField] private Transform cam;

    private Dictionary<Vector2, Tile> tiles;

    //make this a singleton
    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>(); //keeps track of all the tiles
        //determine the tile for each block; randomly decide if it is grass or mountain
        //start at -1 and end with one extra round so the edges are made
        for(int i = -1; i <= width; i++)
        {
            for(int j = -1; j <= height; j++)
            {
                //if i or j is representing an edge piece, put a mountain tile
                if(i == width || i == -1 || j == height || j == -1)
                {
                    var spawnedTile = Instantiate(mountainTile, new Vector3(i, j), Quaternion.identity);
                    spawnedTile.name = $"Tile {i} {j}";

                    spawnedTile.Init(i, j);

                    tiles[new Vector2(i, j)] = spawnedTile;
                }
                //else, put a random tile
                else
                {
                    var randomTile = Random.Range(0, 6) == 3 ? mountainTile : grassTile;
                    var spawnedTile = Instantiate(randomTile, new Vector3(i, j), Quaternion.identity);
                    spawnedTile.name = $"Tile {i} {j}";

                    spawnedTile.Init(i, j);

                    tiles[new Vector2(i, j)] = spawnedTile;
                }

            }
        }



        cam.transform.position = new Vector3((float)width / 2 -0.5f, (float)height / 2 -0.5f, -10);

        GameManager.Instance.ChangeState(GameManager.GameState.SpawnHeroes);
    }

    //decide where the hero will be spawned
    public Tile GetHeroSpawnTile()
    {
        return tiles.Where(t => t.Key.x < width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    //decide where the enemy will be spawned
    public Tile GetEnemySpawnTile()
    {
        return tiles.Where(t => t.Key.x > width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    //return the tile at the specified position
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
