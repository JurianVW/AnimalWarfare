using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Vector2 position;
	public bool occupied;
	public Animal animal;
	
	public void SetPosition(float x, float y){
		this.position = new Vector2(x,y);
	}

	public void SetOccupied(bool occupied){
        this.occupied = occupied;
    }

    public void SetAnimal(Animal animal){
        this.animal = animal;
    }
}
