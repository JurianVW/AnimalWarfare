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

    void Start()
    {
        currentMovement = maxMovement;
    }

    void Update()
    {
        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, Vector3.zero, (movementSpeed / 3) * Time.deltaTime);
            if (this.transform.position == Vector3.zero) moving = false;
        }
    }

    public void Move(Tile targetTile)
    {
        Debug.Log("Move");
        Tile currentTile = this.transform.GetComponentInParent<Tile>();
        int range = Mathf.RoundToInt(Mathf.Abs((targetTile.position.x + targetTile.position.y) - (currentTile.position.x + currentTile.position.y)));
        if (range <= currentMovement)
        {
            Debug.Log("Valid");
            if (currentTile.position != Vector2.zero)
            {
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
}
