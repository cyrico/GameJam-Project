using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver_Manual : CarDriver
{
    public float moveSpeed = 1;
    public float stearingSensitivity = 1; 

    protected override void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal") * stearingSensitivity, Input.GetAxis("Vertical") * moveSpeed);
        wheelSet.SetWheelsBehaviour(input);
    }
}
