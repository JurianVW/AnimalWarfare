using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Material deselectedMaterial, selectedMaterial, hoverMaterial;
    bool selected = false;
    Material standardMaterial;
   
    void Start() { 
    }

    void OnMouseOver()
    {
        this.GetComponent<Renderer>().material = hoverMaterial;
        AnimalSelection(true);
    }

    void OnMouseExit()
    {
        if (!selected)
        {
            this.GetComponent<Renderer>().material = deselectedMaterial;
            AnimalSelection(false);
        }
        else{
             this.GetComponent<Renderer>().material = selectedMaterial;
        }
    }

    public void Select()
    {
        selected = true;
        this.GetComponent<Renderer>().material = selectedMaterial;
        AnimalSelection(true);
    }

    public void Deselect()
    {
        selected = false;
        this.GetComponent<Renderer>().material = deselectedMaterial;
        AnimalSelection(false);
    }

    private void AnimalSelection(bool selected)
    {
        Animal animal = this.GetComponent<Tile>().animal;
        if (animal != null) this.GetComponent<Tile>().animal.SetSelected(selected);
    }
}
