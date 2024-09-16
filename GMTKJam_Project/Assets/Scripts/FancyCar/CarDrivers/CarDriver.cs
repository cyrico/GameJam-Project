using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    public Vector2 input;

    public WheelAndSpringSet wheelSet;

    protected virtual void FixedUpdate()
    {
        wheelSet.SetWheelsBehaviour(input);
    }
}
