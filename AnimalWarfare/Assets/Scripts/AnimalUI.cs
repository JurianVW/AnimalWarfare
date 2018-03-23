using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    public GameObject animalUIPosition;
    public Text animalNameText;

    void Upade()
    {
        var wantedPos = Camera.main.WorldToViewportPoint(animalUIPosition.transform.position);
        animalNameText.transform.position = wantedPos;
    }
}
