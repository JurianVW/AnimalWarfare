using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimalTurnManager : MonoBehaviour
{
    public List<Animal> animalQueue = new List<Animal>();
    public TurnManager turnManager;
    public int currentAnimal = 0;

    public delegate void queueChange();
    public static event queueChange AnimalQueueChange;

    public void AddAnimal(Animal animal)
    {
        animalQueue.Add(animal);
        if(animalQueue.Count > 1)   QueueChange();
    }

    public void RemoveAnimal(Animal animal){
        animalQueue.Remove(animal);
        QueueChange();
    }

    public Animal GetStartingAnimal()
    {
        QueueChange();
        return animalQueue[currentAnimal];
    }

    public Animal GetNextAnimal()
    {
        if (currentAnimal >= animalQueue.Count - 1)
        {
            currentAnimal = 0;
        }
        else
        {
            currentAnimal++;
        }

        QueueChange();
        return animalQueue[currentAnimal];
    }

    private void QueueChange()
    {
        if (AnimalQueueChange != null)
        {
            AnimalQueueChange();
        }
    }
}
