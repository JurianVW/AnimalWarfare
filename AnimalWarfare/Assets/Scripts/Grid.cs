using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int gridWidth, gridHeight;
    public float tileOffset, tileHeight;
    public Tile prefab;
    public Tile[,] tiles { get; private set; }

    void Awake()
    {
        tiles = new Tile[gridWidth, gridHeight];
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Tile tile = Instantiate(prefab, new Vector3((i * 10) * (prefab.transform.localScale.x + tileOffset), tileHeight, (j * 10) * (prefab.transform.localScale.z + tileOffset)), Quaternion.identity);
                tile.SetPosition(i, j);
                tile.transform.SetParent(this.transform);
                tiles[i, j] = tile;
            }
        }
        Vector3 tilePosition = tiles[gridWidth - 1, gridHeight - 1].transform.position;
        this.transform.position = new Vector3(-(tilePosition.x / 2), 0, -(tilePosition.z / 2));
    }

    public Tile GetTile(Vector2 position){
        return tiles[(int)position.x,(int) position.y];
    }

    public void EndTurn(){
        foreach(Tile t in tiles){
            if(t.animal!= null){t.animal.EndTurn();}
            t.GetComponent<Selectable>().Deselect();
        }
    }

    // https://www.raywenderlich.com/4946/introduction-to-a-pathfinding
    public Dictionary<Tile, int> CalculatePossibleTiles(Tile startingTile, int range)
    {
        Dictionary<Tile, int> availableTiles = new Dictionary<Tile, int>();

        CalculateAvailableTiles(startingTile, 0, range, availableTiles);
        return availableTiles;
    }

    private void CalculateAvailableTiles(Tile currentTile, int currentMovement, int range, Dictionary<Tile, int> availableTiles)
    {
        Dictionary<Tile, int> tilesToCalculate = new Dictionary<Tile, int>();
        foreach (TileMovement tileNeightbor in GetNeighbours(currentTile.position))
        {
            if (!tileNeightbor.tile.occupied)
            {
                int newMovement = (tileNeightbor.movement + currentMovement);
                if (newMovement <= range)
                {
                    if (!availableTiles.ContainsKey(tileNeightbor.tile))
                    {
                        availableTiles.Add(tileNeightbor.tile, newMovement);
                        tilesToCalculate.Add(tileNeightbor.tile, newMovement);
                    }
                }
            }
        }
        foreach (Tile tile in tilesToCalculate.Keys)
        {
            CalculateAvailableTiles(tile, tilesToCalculate[tile], range, availableTiles);
        }
    }

    private List<TileMovement> GetNeighbours(Vector2 startingPosition)
    {
        List<TileMovement> neighbours = new List<TileMovement>();

        //1 Movement
        if (startingPosition.x > 0) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x - 1, (int)startingPosition.y], 1));
        if (startingPosition.y > 0) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x, (int)startingPosition.y - 1], 1));
        if (startingPosition.x < (gridWidth - 1)) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x + 1, (int)startingPosition.y], 1));
        if (startingPosition.y < (gridHeight - 1)) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x, (int)startingPosition.y + 1], 1));

        //2 movement
        if (startingPosition.x > 0 && startingPosition.y > 0) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x - 1, (int)startingPosition.y - 1], 2));
        if (startingPosition.x < (gridWidth - 1) && startingPosition.y > 0) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x + 1, (int)startingPosition.y - 1], 2));
        if (startingPosition.x < (gridWidth - 1) && startingPosition.y < (gridHeight - 1)) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x + 1, (int)startingPosition.y + 1], 2));
        if (startingPosition.x > 0 && startingPosition.y < (gridHeight - 1)) neighbours.Add(new TileMovement(tiles[(int)startingPosition.x - 1, (int)startingPosition.y + 1], 2));

        return neighbours;
    }

    public List<Tile> GetNeighbours(Tile tile){
        List<Tile> neighbours = new List<Tile>();
        foreach (TileMovement neighbourTile in GetNeighbours(tile.position))
        {
            neighbours.Add(neighbourTile.tile);
        }
        return neighbours;
    }

    public class TileMovement
    {
        public TileMovement(Tile tile, int movement)
        {
            this.tile = tile;
            this.movement = movement;
        }
        public Tile tile { get; private set; }
        public int movement { get; private set; }
    }
}