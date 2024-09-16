using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public Rigidbody body;
    public Vector3 force = new Vector3(0, 0, 30);
    public float despawnTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletDelete());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddRelativeForce(force * Time.deltaTime, ForceMode.VelocityChange);
    }

    IEnumerator BulletDelete()
    {
        //bullet deletes after 25 seconds
        yield return new WaitForSeconds(10.0f);

        Destroy(this);

    }

    //despawn bullet on collision
    private void OnCollisionEnter(Collision other) {
        Destroy(this);
    }

    //despawn bullet after 10 seconds
    private void Update() {
        Destroy(this, despawnTime);
    }
}
