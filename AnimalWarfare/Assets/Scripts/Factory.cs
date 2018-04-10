using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    public Grid grid;
    public Animal prefab;
    public int spawnAmmount = 1;
    void Start () {
        Tile tile = grid.animalSpawnTile;
        //Add move towards position from out of screen
        for (int i = 0; i < spawnAmmount; i++) {
            int rotationNr = Random.Range (0, 4);
            Vector3 rotation = Vector3.zero;
            switch (rotationNr) {
                case 0:
                    rotation = new Vector3 (0, 0, 0);
                    break;

                case 1:
                    rotation = new Vector3 (0, 90, 0);
                    break;

                case 2:
                    rotation = new Vector3 (0, 180, 0);
                    break;

                case 3:
                    rotation = new Vector3 (0, 270, 0);
                    break;

                default:
                    break;
            }
            SpawnAnimal (tile, rotation);
        }
    }

    void SpawnAnimal (Tile tile, Vector3 rotation) {
        Tile targetTile = grid.GetTile (new Vector2 (Mathf.Round (Random.Range (1, grid.gridWidth - 1)), Mathf.Round (Random.Range (0, grid.gridHeight))));
        if (!targetTile.occupied) {
            Animal animal = Instantiate (prefab);
            animal.transform.SetParent (tile.transform);
            animal.transform.localEulerAngles = rotation;
            animal.transform.localPosition = new Vector3 (0, 0, 0);
            animal.transform.SetParent (targetTile.transform);
            targetTile.SetAnimal (animal);
            animal.Move (targetTile, 0);
        } else {
            SpawnAnimal (tile, rotation);
        }
    }
}