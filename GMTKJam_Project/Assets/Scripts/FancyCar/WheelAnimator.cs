    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimator : MonoBehaviour
{
    public Transform wheelTransform; 
    public float wheelRadius = 1;
    public bool invert = false;
    private Vector3 lastPosition;

   

    void FixedUpdate()
    {
        Vector3 delta = (transform.position - lastPosition);
        float distanceTraveled = delta.magnitude;
        lastPosition = transform.position;

        float wheelCircumference = 2 * Mathf.PI * wheelRadius;
        float numberOfRotations = distanceTraveled / wheelCircumference;

        float spinDirection = 1;
        if (invert == true) { spinDirection = -1; }
        if(Vector3.Dot(this.transform.forward, delta) < 0) { spinDirection = -spinDirection; }
        wheelTransform.transform.Rotate(new Vector3(0, 360 * numberOfRotations * spinDirection, 0));
    }
}
