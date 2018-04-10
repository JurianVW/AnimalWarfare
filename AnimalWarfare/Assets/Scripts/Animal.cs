using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    public string animalName;
    public bool hero;
    public int maxHealthPower, maxAttackPower, maxMovement;

    [Tooltip ("Real animal speed in km/h")]
    public float movementSpeed;

    [Space (10)]
    public bool selected = false;

    public int currentHealthPower { get; private set; }
    public int currentAttackPower { get; private set; }
    public int currentMovement { get; private set; }
    private Player player;

    private bool moving = false;

    public delegate void animalChange ();
    public static event animalChange AnimalStatsChange;
    public bool isDead = false;
    public float deadTime;

    [Space (10)]
    public AttackBase normalAttack;
    public AttackBase specialAttack;

    void OnEnable () {
        currentHealthPower = maxHealthPower;
        currentAttackPower = maxAttackPower;
        currentMovement = maxMovement;
        StatsChange ();
        SetSelected (false);
    }

    void Update () {
        if (moving) {
            transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, Vector3.zero, (movementSpeed / 3) * Time.deltaTime);
            if (this.transform.localPosition == Vector3.zero) {
                Debug.Log ("Triggered");
                this.GetComponent<Animator> ().SetBool ("Run", false);
                moving = false;
            }
        } else if (isDead) {
            if (Time.time - deadTime > 10) {
                Vector3 dest = new Vector3 (0, -5, 0);
                transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, dest, 0.5f * Time.deltaTime);
                this.GetComponentInParent<Selectable>().Deselect();
                if (this.transform.localPosition == dest) {
                    Destroy (this.gameObject);
                }
            }
        }
    }

    public void Move (Tile targetTile, int movementCost) {
        Tile currentTile = this.transform.GetComponentInParent<Tile> ();
        if (currentTile.position != Vector2.zero) {
            this.GetComponent<Animator> ().SetBool ("Run", true);
            this.transform.LookAt (targetTile.transform);
            currentTile.SetAnimal (null);
            targetTile.SetAnimal (this);
            this.transform.SetParent (targetTile.transform);
            moving = true;
            currentMovement -= movementCost;
            StatsChange ();
        }
    }

    public void Attack (bool normal, Animal targetAnimal) {
        if (normal) {
            normalAttack.Attack (this, targetAnimal);
        } else {
            specialAttack.Attack (this, targetAnimal);
        }
    }

    public void IncomingDamage (int attackPower) {
        this.currentHealthPower -= attackPower;
        if (this.currentHealthPower <= 0) {
            this.GetComponent<Animator> ().SetBool ("Die", true);
            this.GetComponentInParent<Tile> ().SetOccupied (false);

            isDead = true;
            deadTime = Time.time;
        }
    }

    public void EndTurn () {
        currentMovement = maxMovement;
        StatsChange ();
    }

    public void SetPlayer (Player player) {
        this.player = player;
    }
    public Player GetPlayer () {
        return player;
    }
    public void SetSelected (bool selected) {
        GameObject animalUI = this.GetComponentInChildren<AnimalUI> (true).gameObject;
        if (animalUI != null) animalUI.SetActive (selected);
    }

    private void StatsChange () {
        if (AnimalStatsChange != null) {
            AnimalStatsChange ();
        }
    }
}