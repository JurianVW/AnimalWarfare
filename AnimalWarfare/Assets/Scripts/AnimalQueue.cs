using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimalQueue : MonoBehaviour
{

    public List<GameObject> animalPanels;
    public AnimalTurnManager animalTurnManager;

    void Start()
    {
        AnimalTurnManager.AnimalQueueChange += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            int index = animalTurnManager.currentAnimal + i;

            if (index >= animalTurnManager.animalQueue.Count)
            {
                index = count;
                count++;
                if(count>= animalTurnManager.animalQueue.Count) count = 0;
            }
            Animal animal = animalTurnManager.animalQueue[index];
            if (animal != null)
            {
                Color color = animal.GetPlayer().playerColor;
                color.a = 0.5f;
                animalPanels[i].GetComponent<Image>().color = color;
                animalPanels[i].GetComponentInChildren<Text>().text = animal.animalName;
            }
        }
    }
}
