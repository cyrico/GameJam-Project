using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver_Chaser : CarDriver
{
    public Transform target;

    public float stearingSensitivity = 1; 

    public float forwardForce = 5000;

    protected override void FixedUpdate()
    {
        Vector3 targetVector = target.transform.position - this.transform.position;
        Vector3 targetVectorOnPlane = Vector3.ProjectOnPlane(targetVector, this.transform.up);
        float angleToTarget = Vector3.SignedAngle(this.transform.forward, targetVectorOnPlane, this.transform.up);
        input.x = Mathf.Clamp(angleToTarget * stearingSensitivity, - 1, 1);

        input.y = forwardForce;

        wheelSet.SetWheelsBehaviour(input);
    }
}
