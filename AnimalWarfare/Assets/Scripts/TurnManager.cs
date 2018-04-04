using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {
    [Range (1, 2)]
    public int playerAmount;
    public Player prefab;

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

    private int startNumber = 1;
    public int currentPlayerNumber { get; private set; }

    void Start () {
        players = new List<Player> ();
        colors = new List<Color> () { Color.red, Color.blue, Color.green, Color.yellow };

        prefab.gameObject.SetActive (false);
        currentPlayerNumber = startNumber;

        for (int i = 0; i < playerAmount; i++) {
            Player player = Instantiate (prefab, new Vector3 (0, 0, 0), Quaternion.identity);
            player.transform.SetParent (this.transform);
            player.name = "Player" + (i + 1);
            player.playerColor = colors[i];
            players.Add (player);
            player.playerId = i + 1;
        }

        if (players.Count != 0) {
            currentPlayer = players[currentPlayerNumber - startNumber];
            currentPlayer.gameObject.SetActive (true);
        }

        SpawnHeroes ();
    }

    public void EndTurn () {
        currentPlayerNumber++;
        if (currentPlayerNumber > players.Count) {
            currentPlayerNumber = startNumber;
        }
        currentPlayer.gameObject.SetActive (false);
        currentPlayer = players[currentPlayerNumber - startNumber];
        currentPlayer.gameObject.SetActive (true);

        grid.EndTurn();
    }

    private void SpawnHeroes () {
        Tile tileLeft = grid.GetTile (new Vector2 (0, grid.gridHeight / 2));
        Tile tileRight = grid.GetTile (new Vector2 (grid.gridWidth - 1, grid.gridHeight / 2));
        if (!tileLeft.occupied) {
            Animal hero = Instantiate (leftHero);
            hero.SetPlayer (players[0]);
            hero.GetComponentInChildren<Renderer> ().material.color = hero.GetPlayer ().playerColor;
            hero.transform.SetParent (tileLeft.transform);
            hero.transform.localEulerAngles = new Vector3 (0, 90, 0);
            hero.transform.localPosition = new Vector3 (0, 0, 0);
            tileLeft.SetOccupied (true);
            tileLeft.SetAnimal (hero);
            leftHeroUI.SetAnimal(hero);
        }
        if (!tileRight.occupied) {
            Animal hero = Instantiate (rightHero);
            hero.SetPlayer (players[1]);
            hero.GetComponentInChildren<Renderer> ().material.color = hero.GetPlayer ().playerColor;
            hero.transform.SetParent (tileRight.transform);
            hero.transform.localEulerAngles = new Vector3 (0, 270, 0);
            hero.transform.localPosition = new Vector3 (0, 0, 0);
            tileRight.SetOccupied (true);
            tileRight.SetAnimal (hero);
            rightHeroUI.SetAnimal(hero);
        }
    }
}