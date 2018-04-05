using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimalTurnManager : MonoBehaviour {
	public List<Animal> animalQueue = new List<Animal>();
	public TurnManager turnManager;
	public int currentAnimal = 0;

	public void addAnimal(Animal hero){
		animalQueue.Add(hero);
	}
	
	public Animal GetStartingAnimal(){
		return animalQueue[currentAnimal];
	}

	public Animal GetNextAnimal(){
		if(currentAnimal >= animalQueue.Count-1){
			currentAnimal = 0;
		}
		else {
			currentAnimal++;
		}

		return animalQueue[currentAnimal];
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
