using UnityEngine;

public class PigeonScript : MonoBehaviour
{

    private float x;
    private float y;
    private float xMax = Screen.width / 2 - 50f;
    private float yMax = Screen.height / 2 - 15f;
    public float speed = 0.0005f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (x != 0 || y != 0)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3(x, y, 0));
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        }
    }

    public void passController(GameManager.ControllerState controllerState) {
        this.x = controllerState.joystick.x;
        this.y = controllerState.joystick.y;
    }
}
