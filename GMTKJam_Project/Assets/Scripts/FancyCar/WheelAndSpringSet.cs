using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAndSpringSet : MonoBehaviour
{
    public List<WheelAndSpringUnit> wheelAndSpringUnits;

    //calculateForce -> exertForce
    //^ hopefully stops tires form fighting with eachother

    public void SetWheelsBehaviour(Vector2 input) 
    {
        foreach (WheelAndSpringUnit unit in wheelAndSpringUnits)
        {
            //unit.ExertTotalForce();
        }

        foreach (WheelAndSpringUnit unit in wheelAndSpringUnits)
        {
            unit.SetWheelBehaviour(input);
            //unit.UpdateTotalForce();
        }

 
    }
}
