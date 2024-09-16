using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public bool dead = false;
    public bool destroyWhenDead = false;
    // Start is called before the first frame update

     public bool GetStatus()
    {
        return dead;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            dead = true;
            Debug.Log("Player is dead");
        }
    }

    //this is for the npcs who also use this script, they get deleted when dead
    void FixedUpdate()
    {
        if(dead && destroyWhenDead)
        {
            Destroy(this.gameObject);
        }
    }

}
