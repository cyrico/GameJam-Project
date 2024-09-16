using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollison : MonoBehaviour
{
    Rigidbody rb;
    public float minimumImpact = 5;
    public float deathDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        // Calculate the impact force
        float impactForce = collision.relativeVelocity.magnitude;
        //get collision gameobject tag
        string tag = collision.gameObject.tag;

        // Log the impact force
        Debug.Log("Player Impact force: " + impactForce);
        // Apply the impact force to the player's health
        if(impactForce > minimumImpact && (tag == "Enemy" || tag == "Bullet"))
        {
            //cancel rigidbody constraints
            rb.constraints = RigidbodyConstraints.None;
            //destroy after 2 seconds
            Destroy(this.gameObject, deathDelay);
            Debug.Log("NPC is dead");
        }
        
        //playerHealth.TakeDamage((int)impactForce);      
    }
}
