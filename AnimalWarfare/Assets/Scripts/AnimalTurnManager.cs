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

    public void addAnimal(Animal hero)
    {
        animalQueue.Add(hero);
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
