using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    public Text nameText;
    public Slider HPSlider;
    public Text APText;
    public Text currentRangeText;
    public Text maxRangeText;


    private Animal myAnimal;
    void OnEnable()
    {
        myAnimal = transform.GetComponentInParent<Animal>();

        nameText.text = myAnimal.animalName;
        HPSlider.maxValue = myAnimal.maxHealthPower;
        maxRangeText.text = myAnimal.maxMovement.ToString();

        Animal.AnimalStatsChange += UpdateUI;
    }

    private void UpdateUI()
    {
        HPSlider.value = myAnimal.currentHealthPower;
        APText.text = myAnimal.currentAttackPower.ToString();
        currentRangeText.text = myAnimal.currentMovement.ToString();
    }
}
