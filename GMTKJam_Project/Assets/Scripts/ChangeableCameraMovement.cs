using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableCameraMovement : MonoBehaviour
{
    public Transform player;
    //public float smoothSpeed = 0.125f;

    public float translationSpeed = 1;
    public float rotationSpeed= 1 ; 


    public bool a;

    public KeyCode changeKey;

    public Transform camPointA;
    public Transform camPointB;

    /*
    //3rd person
    public Vector3 localOffsetA;


    //topdown
    public Vector3 offsetB;
    */

    private void Update()
    {
        if (Input.GetKeyDown(changeKey))
        {
            a = !a;
        }
    }

    void FixedUpdate()
    {


        //position
        /*
        Vector3 offset = player.transform.position + player.transform.TransformPoint(localOffsetA);
        if(a == false) { offset = offsetB; }


        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //this part makes the camera shit idk why
        //transform.LookAt(player);

        Quaternion desiredOrientation = Quaternion.RotateTowards(this.transform.rotation, player.transform.rotation, 10 * Time.deltaTime);
        */



        


        Vector3 desiredPosition = camPointA.position; 
        if(a == false){ desiredPosition = camPointB.position; }
        this.transform.position = Vector3.MoveTowards(this.transform.position, desiredPosition, translationSpeed * Time.deltaTime);

        Quaternion desiredOrientation = camPointA.rotation;
        if (a == false) { desiredOrientation = camPointB.rotation; }
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, desiredOrientation, rotationSpeed * Time.deltaTime);
    }
}
