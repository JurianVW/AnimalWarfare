﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Grid grid;
    public Animal prefab;
    public int spawnAmmount = 1;
    void Start()
    {
        Tile tile = null;
        //Add move towards position from out of screen
        for (int i = 0; i < spawnAmmount; i++)
        {
            int rotationNr = Random.Range(0, 4);
    		Vector3 rotation = Vector3.zero;
            switch (rotationNr)
            {
                case 0:
                    rotation = new Vector3(0,0,0);
                    break;

                case 1:
                    rotation = new Vector3(0,90,0);
                    break;

                case 2:
                    rotation = new Vector3(0,180,0);
                    break;

                case 3:
                    rotation = new Vector3(0,270,0);
                    break;

                default:
                    break;
            }
            SpawnAnimal(tile, rotation);
        }
    }

    void SpawnAnimal(Tile tile, Vector3 rotation)
    {
        tile = grid.tiles[(int)Mathf.Round(Random.Range(1, grid.gridWidth - 1)), (int)Mathf.Round(Random.Range(0, grid.gridHeight))];
        if (!tile.occupied)
        {
            Animal animal = Instantiate(prefab);
            animal.transform.SetParent(tile.transform);
			animal.transform.localEulerAngles = rotation;
            animal.transform.localPosition = new Vector3(0, 0, 0);
            tile.SetOccupied(true);
            tile.SetAnimal(animal);
        }
        else
        {
            SpawnAnimal(tile, rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}