using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespwaner : MonoBehaviour
{
    public float lifespan = 60f; // The lifespan of the enemy in seconds

    void Start()
    {
        // Start the DestroyAfterTime coroutine
        StartCoroutine(DestroyAfterTime(lifespan));
    }

    IEnumerator DestroyAfterTime(float seconds)
    {
        // Wait for the specified amount of seconds
        yield return new WaitForSeconds(seconds);

        // Then destroy the enemy
        Destroy(gameObject);
    }
}
