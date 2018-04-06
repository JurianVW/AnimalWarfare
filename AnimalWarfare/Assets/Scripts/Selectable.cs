using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Material deselectedMaterial, selectedMaterial, hoverMaterial;
    bool selected = false;
    bool current = false;
    Material standardMaterial;

    void Start()
    {
    }

    void OnMouseOver()
    {
        this.GetComponent<Renderer>().material = hoverMaterial;
        AnimalSelection(true);
    }

    void OnMouseExit()
    {
        if (current)
        {
            this.GetComponent<Renderer>().material = hoverMaterial;
            AnimalSelection(false);
        }
        else if (!selected)
        {
            this.GetComponent<Renderer>().material = deselectedMaterial;
            AnimalSelection(false);
        }
        else
        {
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
        current = false;
        this.GetComponent<Renderer>().material = deselectedMaterial;
        AnimalSelection(false);
    }

    public void Current()
    {
        current = true;
        this.GetComponent<Renderer>().material = hoverMaterial;
    }

    private void AnimalSelection(bool selected)
    {
        Animal animal = this.GetComponent<Tile>().animal;
        if (animal != null) this.GetComponent<Tile>().animal.SetSelected(selected);
    }
}
