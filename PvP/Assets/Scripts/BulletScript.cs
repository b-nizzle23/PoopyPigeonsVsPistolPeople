using System;
using UnityEngine;
using UnityEngine.Timeline;

public class BulletScript : MonoBehaviour
{

    private int speed = 5;
    private float xVelocity = 0;
    private float yVelocity = 0;

    public void shoot(float xVelocity, float yVelocity) {
        Debug.Log("Shooting");
        this.xVelocity = xVelocity;
        this.yVelocity = yVelocity;
    }

    void Update() {
        yVelocity -= 0.18f;

        transform.position = transform.position + new Vector3(xVelocity, yVelocity, 0) * speed * Time.deltaTime;
        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    

}
