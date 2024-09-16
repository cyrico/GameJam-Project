using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    //[SerializeField] GameObject enemyPrefab;
    public List<GameObject> posibleSpawns; 
    [SerializeField] GameObject player;
    [SerializeField] float minRadious = 5f;
    [SerializeField] float maxRadious = 15f;
    [SerializeField] float rayHight = 10f;
    [SerializeField] Vector3 carSize = new Vector3(1,1,1);
    [SerializeField] int minspawnTime = 1;
    [SerializeField] int maxspawnTime = 5;
     Vector3 playerPos;
    
    // Start is called before the first frame update
    void Start()
    {

        //spwan cars every few seconds
        InvokeRepeating("SpawnCars", minspawnTime, maxspawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //GetPlayerPos();
        //press space to spawn a car
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCars();
        }
    }

    //get a random position outside the radious of player
    public Vector3 GetRandomPos()
    {
        playerPos = player.transform.position;
        Vector3 randomPos = Random.insideUnitSphere.normalized * maxRadious;//made it so that randomPos MUST be at maxRadious


        //check if the postion is atleast "minRadious" away form the player.
        //if (randomPos.magnitude < minRadious){ return GetRandomPos();  }

        randomPos.z += playerPos.z; 
        randomPos.x += playerPos.x;
        // this will ensure all random points are on the same plane as the player
        randomPos.y = playerPos.y;
        //Debug.Log(randomPos);
        //Debug.Log(IsPositionSpawnable(randomPos));

        

        //check if the position is spawnable
        if (IsPositionSpawnable(randomPos))
        {
            return randomPos;
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
        if (Physics.BoxCast(startPos, carSize, direction, out hit, boxOrientation, rayHight))
        {
            // Log hit object
            //Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.name == "Plane")
            {
                return true;
            }
        }

        return false;
    }

    //spwan cars at random
    public void SpawnCars()
    {
        Vector3 randomPos = GetRandomPos();
        GameObject car = Instantiate(GetPrefab(), randomPos, Quaternion.identity);
        //make the car look at the player
        car.transform.LookAt(player.transform);
    }


    private GameObject GetPrefab() 
    {
        return posibleSpawns[Random.Range(0, posibleSpawns.Count)];
    }
}
