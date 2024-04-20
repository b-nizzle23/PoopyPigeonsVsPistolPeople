




using System;
using UnityEngine;

public class PersonScript : MonoBehaviour
{

    private float xDirection;
    private float xMax = Screen.width - 50f;
    public GameObject bullet;
    public float speed = 5000;
    private float xVelocity = 0;
    private float bulletXVelocity = 0;
    public float bulletYVelocity = 0;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame

    public void shoot() {
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        b.GetComponent<BulletScript>().shoot(bulletXVelocity, bulletYVelocity);   
    }

    public void setDirection(float x) {
        xDirection = x;
    }
    private float shootTimer = 0;
    void Update() {
        shootTimer += Time.deltaTime;
        xVelocity += xDirection * 0.01f;
        xVelocity *= 0.9f;
        xVelocity = Math.Clamp(xVelocity, -7, 7);
        if (Input.GetKeyDown(KeyCode.S)){
            shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.A)) {
            setDirection(-speed);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            setDirection(speed);
        }
        transform.position = transform.position + new Vector3(xVelocity, 0, 0) * speed * Time.deltaTime;
        if (transform.position.x < 0 || transform.position.x > xMax) {
            xVelocity = 0;
        }
        transform.position = new Vector3(Math.Clamp(transform.position.x, 50, xMax), transform.position.y, transform.position.z);
    }
    private Boolean prevCircle = false;
    public void passController(GameManager.ControllerState controllerState) {
        setDirection(controllerState.joystick.x);
        bulletXVelocity = controllerState.joystick.x;
        bulletYVelocity = controllerState.joystick.y;
        if (controllerState.circle && !prevCircle  && shootTimer > 0.7) {
            shoot();
            shootTimer = 0;
        }
        prevCircle = controllerState.circle;
    }
}
