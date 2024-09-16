using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    Rigidbody body;
   
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public float minShootInterval = 0.5f;
    public float maxShootInterval = 2f;
    private enum State
    {
        Idle,
        Shoot
    }
    private bool isShooting = false;
    private State currentState;

    // Start is called before the first frame update
    void Awake()
    {
        //every 15 seconds switch angles (hopefully)
        InvokeRepeating("SwitchDirection", 2f, 7f);
        body = GetComponent<Rigidbody>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    switch (currentState)
    {
        case State.Idle:
            if (isShooting)
            {
                StopCoroutine("Shoot");
                isShooting = false;
                GetComponent<Animator>().SetBool("isShooting", false);
            }
            Idle();
            break;
        case State.Shoot:
            if (!isShooting)
            {
                StartCoroutine("Shoot");
                isShooting = true;
                GetComponent<Animator>().SetBool("isShooting", true);
            }
            break;
    }
    }

    private void SwitchDirection()
    {
        // Debug.Log("Turn");
        float random = Random.Range(0.0f, 360.0f);
        body.transform.Rotate(0f, random, 0.0f, Space.Self);
    }

    void Idle(){
        //this is the default behaviour for the npcs, they just walk around and turn
        GetComponent<Animator>().SetBool("isWalking", true);
        Vector3 fwd = transform.forward;
        RaycastHit hit;

        //uses a raycast to see if there's an object in front of it
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            //if there is an object in front of it, it will check the tag of the object
            if(hit.collider != null){
                GameObject hitObject = hit.collider.gameObject;
                // check the tag of the object that was hit
                if (hitObject.tag == "Enemy" || hitObject.tag == "Player")
                {
                    //if this npc even has a gun
                    if( bulletSpawnPoint != null) 
                    {
                        //if the object is the player or enemy, it will shoot
                        currentState = State.Shoot;
                        GetComponent<Animator>().SetBool("isWalking", false);
                        Debug.Log("shoot state");
                    }

                }else{
                    //if the object is not the player or enemy, it will turn
                    SwitchDirection();
                    currentState = State.Idle;
                }
                
            }else{
                //if there is no object in front of it, it will turn
                SwitchDirection();
                currentState = State.Idle;
                Debug.Log("ilde state");
            }
        }


        Vector3 movementVelocity = transform.forward * moveSpeed * Time.deltaTime;
        body.velocity = new Vector3(movementVelocity.x, body.velocity.y, movementVelocity.y);
    }

private IEnumerator Shoot()
{
    // Shooting behavior code goes here
    while (true)
    {
        Vector3 fwd = transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            GameObject hitObject = hit.collider.gameObject;
            // check the tag of the object that was hit
            if (hitObject.tag == "Enemy" || hitObject.tag == "Player")
            {
                // Instantiate a new bullet
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                // Add velocity to the bullet
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                // Wait for random interval before next shot
                yield return new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
            }
            else
            {
                // If no enemy or player in sight, return to idle state
                currentState = State.Idle;
                GetComponent<Animator>().SetBool("isShooting", false);
                yield break; // this will exit the coroutine
            }
        }
        else
        {
            // If no enemy or player in sight, return to idle state
            currentState = State.Idle;
            GetComponent<Animator>().SetBool("isShooting", false);
            yield break; // this will exit the coroutine
        }
    }
}
    
}


