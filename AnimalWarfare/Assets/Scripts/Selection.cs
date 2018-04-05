using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    Tile currentSelection;
    Tile newSelection;
    private Dictionary<Tile, int> availableTiles;
    public Grid grid;
    public TurnManager turnManager;
    bool tileChanged = false;

    public void SetCurrentSelection(Tile tile)
    {
        currentSelection = tile;
        tileChanged = true;
        Debug.Log(tile);
    }

    void Update()
    {
        if (tileChanged)
        {
            OnCurrentSelection();
            tileChanged = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponentInParent<Tile>())
                {
                    if (newSelection == null)
                    {
                        Debug.Log("Click new");
                        newSelection = hit.collider.GetComponentInParent<Tile>();
                        OnNewSelection();
                    }
                }
            }
        }
    }

    public void OnCurrentSelection()
    {
        if (currentSelection != null)
        {
            if (currentSelection.animal != null)
            {
                if (!currentSelection.animal.isDead && currentSelection.animal.GetPlayer() == turnManager.currentPlayer)
                {
                    availableTiles = grid.CalculatePossibleTiles(currentSelection, currentSelection.animal.currentMovement);
                    foreach (Tile tile in availableTiles.Keys)
                    {
                        tile.GetComponent<Selectable>().Select();
                    }
                }
                else
                {
                    DeselectAll();
                }
            }
            else
            {
                DeselectAll();
            }
        }
    }

    private void OnNewSelection()
    {
        Debug.Log(currentSelection);
        Debug.Log(newSelection);
        if (newSelection != null)
        {
            if (!newSelection.occupied && availableTiles.ContainsKey(newSelection))
            {
                currentSelection.animal.Move(newSelection, availableTiles[newSelection]);
                currentSelection = newSelection;
            }
            else if (currentSelection.animal.hero && newSelection.animal != null)
            {
                if (!newSelection.animal.hero)
                {
                    if (grid.GetNeighbours(currentSelection).Contains(newSelection))
                    {
                        switch (turnManager.currentPlayer.playerId)
                        {
                            case 1:
                                newSelection.animal.GetComponentInChildren<Renderer>().material.color = Color.red;
                                newSelection.animal.SetPlayer(turnManager.currentPlayer);
                                turnManager.EndTurn();
                                turnManager.animalTurnManager.AddAnimal(newSelection.animal);
                                break;
                            case 2:
                                newSelection.animal.GetComponentInChildren<Renderer>().material.color = Color.blue;
                                newSelection.animal.SetPlayer(turnManager.currentPlayer);
                                turnManager.EndTurn();
                                turnManager.animalTurnManager.AddAnimal(newSelection.animal);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else if (!currentSelection.animal.hero)
            {
                if (newSelection.animal.GetPlayer() == null)
                {
                    currentSelection.animal.Attack(true, newSelection.animal);
                    turnManager.EndTurn();
                }
                else if (newSelection.animal.GetPlayer().playerId != currentSelection.animal.GetPlayer().playerId)
                {
                    currentSelection.animal.Attack(true, newSelection.animal);
                    turnManager.EndTurn();
                }
            }
        }
        DeselectAll();
    }

    private void DeselectAll()
    {
        if (currentSelection != null) currentSelection.transform.GetComponent<Selectable>().Deselect();
        if (newSelection != null) newSelection.transform.GetComponent<Selectable>().Deselect();
        newSelection = null;
        //currentSelection = null;

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