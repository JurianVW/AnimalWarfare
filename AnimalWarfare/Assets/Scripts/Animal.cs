using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public bool hero;
    public int healthPower, attackPower, maxMovement;
    public int currentHealthPower;
    public float movementSpeed;
    public bool moving = false;
    private float currentMovement;
    private Player player;

    void Start()
    {
        currentMovement = maxMovement;
    }

    void Update()
    {
        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, Vector3.zero, (movementSpeed / 3) * Time.deltaTime);
            if (this.transform.localPosition == Vector3.zero){
                Debug.Log("Triggered");
                this.GetComponent<Animator>().SetBool("Run", false);
                moving = false; 
            } 
        }
    }

    public void Move(Tile targetTile)
    {
        Debug.Log("Move");
        int range = 0;
        Tile currentTile = this.transform.GetComponentInParent<Tile>();
        int xrange = (int)Mathf.Abs((targetTile.position.x - currentTile.position.x));
        int yrange = (int)Mathf.Abs((currentTile.position.y - targetTile.position.y));

        if (xrange >= yrange)
        {
            range = xrange;
        }
        else{
            range = yrange;
        }

        Debug.Log("Range: " + range);
        if (range <= currentMovement)
        {
            Debug.Log("Valid");
            if (currentTile.position != Vector2.zero)
            {
                this.GetComponent<Animator>().SetBool("Run",true);
                this.transform.LookAt(targetTile.transform);
                currentTile.SetAnimal(null);
                targetTile.SetAnimal(this);
                this.transform.SetParent(targetTile.transform);
                moving = true;
            }
        }
    }

    private bool validMovement(Vector2 current, Vector2 target)
    {
        return Mathf.Abs((target.x + target.y) - (current.x + current.y)) <= currentMovement;
    }

    public void SetPlayer(Player player){
        this.player = player;
    }
}
