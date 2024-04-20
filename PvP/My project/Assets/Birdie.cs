using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Birdie : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Animator animator;
    public GameObject poop;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.velocity = Vector2.left * speed * Time.deltaTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * speed;
            animator.Play("Trigger");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(poop, transform.position, transform.rotation);
        }

        if ((Input.GetKey(KeyCode.A) && transform.localScale.x != 1) || (Input.GetKey(KeyCode.D) && transform.localScale.x != -1))
        {
            transform.localScale *= new Vector2(-1, 1);
        }
        transform.position = transform.position + (Vector3.left  * (transform.localScale.x) * speed * Time.deltaTime);

    }
}
