using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverer : MonoBehaviour
{
    public Rigidbody body;

    public LayerMask layerMask;

    [Header("Displacement Settings")]
    public float desiredDisplacement = 2;
    public float k = 1;//spring strength
    public float damping = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit displacementHit;
        if (Physics.Raycast(this.transform.position, Vector3.down, out displacementHit, desiredDisplacement, layerMask))
        {
            body.AddForce(GetDisplacementForce(desiredDisplacement, displacementHit.distance) * Time.deltaTime);
        }
    }

    private Vector3 GetDisplacementForce(float desiredDisplacement, float currentDisplacement)
    {
        float displacement = (desiredDisplacement - currentDisplacement);
        float springForce = -k * displacement;
        float dampingForce = damping * body.velocity.y;
        return Vector3.down * (springForce + dampingForce);
    }
}
