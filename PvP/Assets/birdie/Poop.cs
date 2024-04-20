using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start() {
        myRigidBody.velocity = new Vector3(0, -150, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "poop") {
            Destroy (gameObject);
        }
        
    }
}
