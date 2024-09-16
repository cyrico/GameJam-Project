using System.Security.Cryptography;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition= Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //this part makes the camera shit idk why
        //transform.LookAt(player);

    }

}
