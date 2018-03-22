using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    public Material deselectedMaterial, selectedMaterial;

    void OnMouseOver()
    {
        this.GetComponent<Renderer>().material = selectedMaterial;
    }

    void OnMouseExit()
    {
        this.GetComponent<Renderer>().material = deselectedMaterial;
    }
}
