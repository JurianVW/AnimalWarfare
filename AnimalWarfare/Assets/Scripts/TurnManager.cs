using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {
    [Range (1, 2)]
    public int playerAmount;
    public Player prefab;
    public Selection selection;

    [Space (10)]
    public Grid grid;

    [Space (10)]
    public Animal leftHero;
    public Animal rightHero;
    public AnimalUI leftHeroUI;
    public AnimalUI rightHeroUI;
    private List<Color> colors;
    private List<Player> players;
    public Player currentPlayer { get; private set; }
    public int currentPlayerNumber { get; private set; }
    int startingPlayer = 0;
    public AnimalTurnManager animalTurnManager;

    void Start () {
        players = new List<Player> ();
        colors = new List<Color> () { Color.red, Color.blue, Color.green, Color.yellow };
        startingPlayer = Random.Range(0,2);
        prefab.gameObject.SetActive (false);
        currentPlayerNumber = 0;

        for (int i = 0; i < playerAmount; i++) {
            Player player = Instantiate (prefab, new Vector3 (0, 0, 0), Quaternion.identity);
            player.transform.SetParent (this.transform);
            player.name = "Player" + (i + 1);
            player.playerColor = colors[i];
            players.Add (player);
            player.playerId = i + 1;
        }

        if (players.Count != 0) {
            currentPlayer = players[currentPlayerNumber];
            currentPlayer.gameObject.SetActive (true);
        }

        SpawnHeroes ();
        SelectFirstPlayer();
    }

    public void SelectFirstPlayer(){
        selection.SetCurrentSelection(animalTurnManager.GetStartingAnimal().GetComponentInParent<Tile>());
    }

    public void EndTurn () {
        currentPlayerNumber++;
        if (currentPlayerNumber >= players.Count) {
            currentPlayerNumber = 0;
        }
        currentPlayer.gameObject.SetActive (false);
        currentPlayer = players[currentPlayerNumber];
        currentPlayer.gameObject.SetActive (true);

        grid.EndTurn();
        selection.SetCurrentSelection(animalTurnManager.GetNextAnimal().GetComponentInParent<Tile>());
    }

    private void SpawnHeroes () {
        Tile tileLeft = grid.GetTile (new Vector2 (0, grid.gridHeight / 2));
        Tile tileRight = grid.GetTile (new Vector2 (grid.gridWidth - 1, grid.gridHeight / 2));
        Animal p1;
        Animal p2;
        if(startingPlayer == 0){
            p1 = leftHero;
            p2 = rightHero;
        }
        else{
            p1 = rightHero;
            p2 = leftHero;
        }
        if (!tileLeft.occupied) {
            Animal hero = Instantiate (p1);
            hero.SetPlayer (players[0]);
            hero.GetComponentInChildren<Renderer> ().material.color = hero.GetPlayer ().playerColor;
            hero.transform.SetParent (tileLeft.transform);
            hero.transform.localEulerAngles = new Vector3 (0, 90, 0);
            hero.transform.localPosition = new Vector3 (0, 0, 0);
            tileLeft.SetOccupied (true);
            tileLeft.SetAnimal (hero);
            leftHeroUI.SetAnimal(hero);
            animalTurnManager.addAnimal(hero);
        }
        if (!tileRight.occupied) {
            Animal hero = Instantiate (p2);
            hero.SetPlayer (players[1]);
            hero.GetComponentInChildren<Renderer> ().material.color = hero.GetPlayer ().playerColor;
            hero.transform.SetParent (tileRight.transform);
            hero.transform.localEulerAngles = new Vector3 (0, 270, 0);
            hero.transform.localPosition = new Vector3 (0, 0, 0);
            tileRight.SetOccupied (true);
            tileRight.SetAnimal (hero);
            rightHeroUI.SetAnimal(hero);
            animalTurnManager.addAnimal(hero);
        }
    }
}