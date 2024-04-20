using System;
using UnityEngine;
using UnityEngine.Timeline;

public class PigeonScript : MonoBehaviour
{

    private float xDirection;
    private float xMax = Screen.width - 50f;
    private float yMax = Screen.height - 50f;
    public GameObject poop;
    public SpriteRenderer sprite;
    public float speed = 5000;
    private float xVelocity = 0;
    private float yVelocity = 0;  
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame

    public void flap() {
        yVelocity = 10;
    }

    public void doPoopy() {
        yVelocity = 15;
        Instantiate(poop, transform.position, transform.rotation);
    }

    public void setDirection(float x) {
        xDirection = x;
        sprite.flipX = Math.Sign(x) != 1;
    }
    private float shootTimer = 0;
    void Update() {
        shootTimer += Time.deltaTime;
        yVelocity -= 0.12f;
        xVelocity += xDirection * 0.001f;
        yVelocity = Math.Clamp(yVelocity, -20, 20);
        xVelocity = Math.Clamp(xVelocity, -10, 10);
        if (Input.GetKeyDown(KeyCode.Space)) {
            flap();
        }
        if (Input.GetKeyDown(KeyCode.S)){
            doPoopy();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            setDirection(-speed);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            setDirection(speed);
        }

        transform.position = transform.position + new Vector3(xVelocity, yVelocity, 0) * speed * Time.deltaTime;
        if (transform.position.x < 0 || transform.position.x > xMax) {
            xVelocity = 0;
        }
        if (transform.position.y < 50 || transform.position.y > yMax) {
            yVelocity *= 0.5f;
        }
        transform.position = new Vector3(Math.Clamp(transform.position.x, 50, xMax), Math.Clamp(transform.position.y, 50, yMax), transform.position.z);
    }
    private bool prevCircle = false;
    public void passController(GameManager.ControllerState controllerState) {
        setDirection(controllerState.joystick.x);
        if (controllerState.circle && !prevCircle && shootTimer > 0.7) {
            doPoopy();
            shootTimer = 0;
        }
        prevCircle = controllerState.circle;
    }
}
