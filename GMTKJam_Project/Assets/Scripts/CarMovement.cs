using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    public Rigidbody body;
    public Vector3 force = new Vector3(0, 0, 10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddRelativeForce(force * Time.deltaTime, ForceMode.VelocityChange);
    }
}
