using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    public Material deselectedMaterial, selectedMaterial;
    bool selected = false;

    void OnMouseOver()
    {
        this.GetComponent<Renderer>().material = selectedMaterial;
    }

    void OnMouseExit()
    {
        if (!selected) this.GetComponent<Renderer>().material = deselectedMaterial;
    }

    public void Select()
    {
        selected = true;
        this.GetComponent<Renderer>().material = selectedMaterial;
    }

    public void Deselect()
    {
        selected = false;
        this.GetComponent<Renderer>().material = deselectedMaterial;
    }
}
