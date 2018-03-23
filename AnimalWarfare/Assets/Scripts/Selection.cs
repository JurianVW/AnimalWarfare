using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    Tile currentSelection;
    Tile moveSelection;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (currentSelection == null)
                {
                    Debug.Log("Click current");
                    currentSelection = hit.collider.GetComponentInParent<Tile>();
                    OnCurrentSelection();
                }
                else if (moveSelection == null)
                {
                    Debug.Log("Click move");
                    moveSelection = hit.collider.GetComponentInParent<Tile>();
                    OnMoveSelection();
                }

            }
            else
            {
                if (currentSelection != null)
                {
                    DeselectAll();
                }
            }
        }
    }

    private void OnCurrentSelection()
    {
        if (currentSelection != null)
        {
            if (currentSelection.animal != null)
            {
                currentSelection.transform.GetComponent<Selectable>().Select();
                Debug.Log("Select");
            }
            else
            {
                currentSelection = null;
            }
        }
    }

    private void OnMoveSelection()
    {
        if (moveSelection != null)
        {
            if (!moveSelection.occupied)
            {
                Debug.Log("MoveSelection");
                currentSelection.animal.Move(moveSelection);
            }
        }
        DeselectAll();
    }

    private void DeselectAll()
    {
        if (currentSelection != null) currentSelection.transform.GetComponent<Selectable>().Deselect();
        if (moveSelection != null) moveSelection.transform.GetComponent<Selectable>().Deselect();
        currentSelection = null;
        moveSelection = null;
    }
}
