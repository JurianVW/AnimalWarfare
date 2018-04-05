using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    public Text nameText;
    public Slider HPSlider;
    public Text currentHPText;
    public Text maxHPText;
    public Text APText;
    public Text currentRangeText;
    public Text maxRangeText;
    private Animal myAnimal;

    void OnEnable()
    {
        myAnimal = transform.GetComponentInParent<Animal>();
        if (myAnimal != null)
        {
            StartUI();
        }
    }

    public void StartUI()
    {
        nameText.text = myAnimal.animalName;
        HPSlider.maxValue = myAnimal.maxHealthPower;
        maxHPText.text = myAnimal.maxHealthPower.ToString();
        maxRangeText.text = myAnimal.maxMovement.ToString();
        Animal.AnimalStatsChange += UpdateUI;
        UpdateUI();
    }

    public void SetAnimal(Animal animal)
    {
        myAnimal = animal;
        StartUI();
    }

    private void UpdateUI()
    {
        HPSlider.value = myAnimal.currentHealthPower;
        currentHPText.text = myAnimal.currentHealthPower.ToString();
        APText.text = myAnimal.currentAttackPower.ToString();
        currentRangeText.text = myAnimal.currentMovement.ToString();
    }
}