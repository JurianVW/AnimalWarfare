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
            OnMouseClick(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            OnMouseClick(false);
        }
    }

    public void OnMouseClick(bool button)
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
                    OnNewSelection(button);
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
                    currentSelection.GetComponent<Selectable>().Current();
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

    private void OnNewSelection(bool normalAttack)
    {
        Debug.Log(currentSelection);
        Debug.Log(newSelection);
        if (newSelection != null)
        {
            if (!newSelection.occupied && availableTiles.ContainsKey(newSelection))
            {
                currentSelection.GetComponent<Selectable>().Deselect();
                currentSelection.animal.Move(newSelection, availableTiles[newSelection]);
                currentSelection = newSelection;
                DeselectAll();
                OnCurrentSelection();
            }
            else if (currentSelection.animal.hero && newSelection.animal != null)
            {
                if (!newSelection.animal.hero)
                {
                    if (grid.GetNeighbours(currentSelection).Contains(newSelection)
                            && newSelection.animal.GetPlayer() == null
                            && turnManager.currentPlayer.animalCount < 3)
                    {
                        switch (turnManager.currentPlayer.playerId)
                        {
                            case 1:
                                newSelection.animal.GetComponentInChildren<Renderer>().material.color = turnManager.currentPlayer.playerColor;
                                currentSelection.animal.GetPlayer().animalCount++;
                                newSelection.animal.SetPlayer(turnManager.currentPlayer);
                                turnManager.EndTurn();
                                turnManager.animalTurnManager.AddAnimal(newSelection.animal);
                                DeselectAll();
                                break;
                            case 2:
                                newSelection.animal.GetComponentInChildren<Renderer>().material.color = turnManager.currentPlayer.playerColor;
                                currentSelection.animal.GetPlayer().animalCount++;
                                newSelection.animal.SetPlayer(turnManager.currentPlayer);
                                turnManager.EndTurn();
                                turnManager.animalTurnManager.AddAnimal(newSelection.animal);
                                DeselectAll();
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
                    currentSelection.animal.Attack(normalAttack, newSelection.animal);
                    turnManager.EndTurn();
                }
                else if (newSelection.animal.GetPlayer().playerId != currentSelection.animal.GetPlayer().playerId)
                {
                    currentSelection.animal.Attack(normalAttack, newSelection.animal);
                    turnManager.EndTurn();
                }
            }
            newSelection = null;
        }
    }

    private void DeselectAll()
    {
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