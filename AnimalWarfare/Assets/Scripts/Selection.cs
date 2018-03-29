using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    Tile currentSelection;
    Tile moveSelection;
    private Dictionary<Tile, int> availableTiles;
    public Grid grid;
    public TurnManager turnManager;

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
                if (!currentSelection.animal.isDead)
                {
                    availableTiles = grid.CalculatePossibleTiles(currentSelection, currentSelection.animal.currentMovement);
                    currentSelection.transform.GetComponent<Selectable>().Select();
                    foreach (Tile tile in availableTiles.Keys)
                    {
                        tile.GetComponent<Selectable>().Select();
                    }
                }
                else{
                    currentSelection = null;
                }
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
            if (!moveSelection.occupied && availableTiles.ContainsKey(moveSelection))
            {
                Debug.Log("MoveSelection");
                currentSelection.animal.Move(moveSelection, availableTiles[moveSelection]);
            }
            else if (currentSelection.animal.hero && !moveSelection.animal.hero)
            {
                if (grid.GetNeighbours(currentSelection).Contains(moveSelection))
                {
                    switch (turnManager.currentPlayerNumber)
                    {
                        case 1:
                            moveSelection.animal.GetComponentInChildren<Renderer>().material.color = Color.red;
                            moveSelection.animal.SetPlayer(turnManager.currentPlayer);
                            break;
                        case 2:
                            moveSelection.animal.GetComponentInChildren<Renderer>().material.color = Color.blue;
                            moveSelection.animal.SetPlayer(turnManager.currentPlayer);
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (!currentSelection.animal.hero)
            {
                if (moveSelection.animal.GetPlayer() == null)
                {
                    currentSelection.animal.Attack(moveSelection.animal);
                }
                else if (moveSelection.animal.GetPlayer().playerId != currentSelection.animal.GetPlayer().playerId)
                {
                    currentSelection.animal.Attack(moveSelection.animal);
                }
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

        if (availableTiles != null)
        {
            foreach (Tile tile in availableTiles.Keys)
            {
                tile.GetComponent<Selectable>().Deselect();
            }
        }
        availableTiles = null;
    }
}
