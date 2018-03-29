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

    private List<Player> players;
    public Player currentPlayer {get; private set;}

    private int startNumber = 1;
    public int currentPlayerNumber {get; private set;}

    void Start () {
        players = new List<Player> ();

        prefab.gameObject.SetActive (false);
        currentPlayerNumber = startNumber;

        for (int i = 0; i < playerAmount; i++) {
            Player player = Instantiate (prefab, new Vector3 (0, 0, 0), Quaternion.identity);
            player.transform.SetParent (this.transform);
            player.name = "Player" + (i + 1);
            players.Add (player);
            player.playerId = i + 1;
        }

        if (players.Count != 0) {
            currentPlayer = players[currentPlayerNumber - startNumber];
            currentPlayer.gameObject.SetActive (true);
        }
    }

    public void endTurn () {
        currentPlayerNumber++;
        if (currentPlayerNumber > players.Count) {
            currentPlayerNumber = startNumber;
        }
        currentPlayer.gameObject.SetActive (false);
        currentPlayer = players[currentPlayerNumber - startNumber];
        currentPlayer.gameObject.SetActive (true);
    }
}