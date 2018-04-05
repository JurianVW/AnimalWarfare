using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimalQueue : MonoBehaviour {

	public List<GameObject> animalPanels;
	public AnimalTurnManager animalTurnManager;

	void Start () {
		foreach(GameObject go in animalPanels){
			go.GetComponent<Image>().color = new Color(1,0,0,0.5f);
		}
		//Subscribe
		UpdateUI();
	}
	
	void UpdateUI(){
		for (int i = 0; i < 5; i++)
		{
			Animal animal = animalTurnManager.animalQueue[i];
			if(animal != null){
				Color color = animal.GetPlayer().playerColor;
				color.a = 0.5f;
				animalPanels[i].GetComponent<Image>().color = color;
				animalPanels[i].GetComponentInChildren<Text>().text = animal.name;
			}
		}
	}
}
