using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float minRange = -50.0f;
    public float maxRange = 50.0f;
    public float playerRadio = 1.0f;
    public float rayDistance = 10.0f;
    public Vector3 playerSize = new Vector3(1,1,1);
    public float rayHight = 10f;
    private Rigidbody rb;
    private Vector3 playerPos;
    public GameObject followCamera;

    public PlayerHealth playerHealth;

    public Transform playerModel; 

    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
        Teleport();
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per frame, it's better to use it for physics related updates
    void FixedUpdate()
    {
        //player can no longer move if dead
        if(playerHealth.dead == true)
        {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        //change player position according to input
        //transform.position += movement * speed * Time.deltaTime;
        rb.velocity = new Vector3(movement.x * speed * Time.deltaTime, rb.velocity.y, movement.z * speed * Time.deltaTime);
        //make player look at the direction of movement
        if(movement != Vector3.zero)
        {

            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            //play walking animation
            GetComponent<Animator>().SetBool("isWalking", true);
        }else{
            //stop walking animation
            GetComponent<Animator>().SetBool("isWalking", false);
        }

    }

    //teleport player to a random location
    public void Teleport()
    {
        Vector3 randomPosition = GetRandomPos();
        Debug.Log("Teleporting player to: " + randomPosition);
        transform.position = randomPosition;
        //teleport camera to player
        followCamera.transform.position = new Vector3(randomPosition.x, followCamera.transform.position.y, randomPosition.z);
        
    }
    public Vector3 GetRandomPos()
    {
        float randomX = Random.Range(minRange, maxRange);
        float randomZ = Random.Range(minRange, maxRange);
        Vector3 randomPosition = new Vector3(randomX, playerPos.y, randomZ);
        transform.position = randomPosition;

        if (IsPositionSpawnable(randomPosition))
        {
            return randomPosition;
        }
        else
        {
            return GetRandomPos();
        }
    }

    bool IsPositionSpawnable(Vector3 pos)
    {
        Quaternion boxOrientation = Quaternion.identity; // no rotation of the box
        Vector3 direction = Vector3.down; // direction of the boxcast
        RaycastHit hit;

        Vector3 startPos = new Vector3(pos.x, rayHight, pos.z);

        // Boxcast
        if (Physics.BoxCast(startPos, playerSize, direction, out hit, boxOrientation, rayHight))
        {
            // Log hit object
            if(hit.transform.gameObject.name == "Plane")
            {
                return true;
            }
        }

        return false;
    }
}
