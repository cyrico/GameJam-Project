using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollison : MonoBehaviour
{
    public PlayerHealth playerHealth;
    Rigidbody rb;
    public float minimumImpact = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        // Calculate the impact force
        float impactForce = collision.relativeVelocity.magnitude;

        // Log the impact force
        Debug.Log("Impact force: " + impactForce);
        // Apply the impact force to the player's health
        if(impactForce > minimumImpact)
        {
            //cancel rigidbody constraints
            rb.constraints = RigidbodyConstraints.None;
            playerHealth.TakeDamage(100);
            //player dies once impact force is greater than curtain amount
        }
        
        //playerHealth.TakeDamage((int)impactForce);      
    }
}
