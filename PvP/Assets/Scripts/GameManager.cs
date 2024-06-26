using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{


    public Canvas canvas;
    public GameObject nameObject;
    public GameObject personObject;
    public GameObject pigeonObject;
    private PlayerJoin[] playerJoins;

    public Dictionary<string, GameObject> people = new();
    public Dictionary<string, GameObject> pigeons = new();
    public Dictionary<string, string> teams = new();

    // the flipper is true for people, and false for pigeons. It flips back and forth as they join.
    private bool joinFlipper = true;

    // Madder functions that you may call
    // These functions should be conditionally called based on if this is inside a WebGL build, not the editor
    [DllImport("__Internal")]
    private static extern void MessageToPlayer(string userName, string message);
    [DllImport("__Internal")]
    private static extern void MessageToAllPlayers(string message);
    [DllImport("__Internal")]
    private static extern void Exit();
    [DllImport("__Internal")]
    private static extern void UpdateStats(string userName, string stats);

    void Start()
    {
        // PlayerJoin playerJoin = new() {
        //     name = "Player 0",
        //     stats = new GameStats()
        // };
        // string jsonPlayerJoin = JsonUtility.ToJson(playerJoin);
        // PlayerJoined(jsonPlayerJoin);
    }

    void Update()
    {
        //// Testing Madder functions
        //// TODO: This code should be commented out or removed before submission

        //// Test RoomCode
        //// TODO: Any of the following code may be modified or deleted
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    RoomCode("ABCD");
        //}

        // Test PlayerJoined
        // TODO: Any of the following code may be modified or deleted
        // if (Input.GetKeyDown(KeyCode.J))
        // {
          
        // }

        //// Test PlayerLeft
        //// TODO: Any of the following code may be modified or deleted
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (playerJoins.Length == 0)
        //    {
        //        return;
        //    }
        //    PlayerLeft("Player 0");
        //}

        //// Test PlayerControllerState for Player 0
        //// TODO: Any of the following code may be modified or deleted
        // if (false) {
        //    Joystick joystick = new Joystick(0, 0);
        //    if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        //    {
        //        joystick.y = 100;
        //    }
        //    if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        //    {
        //        joystick.y = -100;
        //    }
        //    if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        //    {
        //        joystick.x = -100;
        //    }
        //    if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        //    {
        //        joystick.x = 100;
        //    }
        //    ControllerState controllerState = new ControllerState();
        //    controllerState.name = "Player 0";
        //    controllerState.joystick = joystick;
        //    controllerState.circle = false;
        //    controllerState.triangle = false;
        //    controllerState.plus = false;
        //    string jsonControllerState = JsonUtility.ToJson(controllerState);
        //    PlayerControllerState(jsonControllerState);
        // }

        //// Test HandleExit
        //// TODO: Any of the following code may be modified or deleted
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    HandleExit();
        //}
    }

    // TODO: The following function may be modified or deleted
    void HandleExit()
    {
        // Remove all player names from canvas
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }

        // Reset playerJoins array
        playerJoins = new PlayerJoin[0];
    }

    /*
    * Madder function: RoomCode
    * This function is called when the uniquely generated code is received from the server
    * You will typically use this code to display the room code on the screen
    */
    public TextMeshProUGUI textMesh;
    public void RoomCode(string roomCode) 
    {
        textMesh.text = "Room Code : " + roomCode;
    }

    /*
    * Madder function: PlayerJoined
    * This function is called when a new player joins the game
    * You will typically use this function to create a character instance for this player
    *   and keep track of the player's stats
    */

    public void PlayerJoined(string jsonPlayerJoin)
    {
        // Destructure jsonPlayerJoin
        PlayerJoin playerJoin = JsonUtility.FromJson<PlayerJoin>(jsonPlayerJoin);

        // TODO: Any of the following code may be modified or deleted

        // Initialize player stats if they are null or have missing fields
        // if (playerJoin.stats == null)
        // {
        //     playerJoin.stats = new GameStats();
        // }
        // if (playerJoin.stats.gamesPlayed == null)
        // {
        //     playerJoin.stats.gamesPlayed = new Stat("Games Played", 0);
        // }

        // Create player name on canvas
        
        
        // Add player to playerJoins array
        if (joinFlipper) {
            GameObject person = Instantiate(personObject, new Vector3(500, 0, 0), Quaternion.identity);
            people.Add(playerJoin.name, person);
            teams.Add(playerJoin.name, "person");
        } else {
            GameObject pigeon = Instantiate(pigeonObject, new Vector3(500, 0, 0), Quaternion.identity);
            pigeons.Add(playerJoin.name, pigeon);
            teams.Add(playerJoin.name, "pigeon");
        }
        joinFlipper = !joinFlipper;
        
        // Add game played to player stats
        // playerJoin.stats.addGamePlayed();
        // Update player stats on server
        // #if UNITY_WEBGL && !UNITY_EDITOR // Only call this function if this is a WebGL build
        // string jsonStats = JsonUtility.ToJson(playerJoin.stats);
        // UpdateStats(playerJoin.name, jsonStats);
        // #endif
    }

    /*
    * Madder function: PlayerLeft
    * This function is called when a player leaves the game
    * You will typically use this function to remove the character instance of this player
    */
    public void PlayerLeft(string playerName)
    {
        // TODO: Any of the following code may be modified or deleted

        // Remove player from playerJoins array
        PlayerJoin[] newPlayerJoins = new PlayerJoin[playerJoins.Length - 1];
        int j = 0;
        for (int i = 0; i < playerJoins.Length; i++)
        {
            if (playerJoins[i].name != playerName)
            {
                newPlayerJoins[j] = playerJoins[i];
                j++;
            }
            else
            {
                // Exit game if first player (host) leaves
                #if UNITY_WEBGL && !UNITY_EDITOR // Only call this function if this is a WebGL build
                if (i == 0)
                {
                    Exit();
                }
                #endif
                HandleExit();
                return;
            }
        }
        playerJoins = newPlayerJoins;

        // Remove player name from canvas
        foreach (Transform child in canvas.transform)
        {
            if (child.GetComponent<NameScript>().GetName() == playerName)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    /*
    * Madder function: PlayerControllerState
    * This function is called when the controller state of a player is updated
    * You will typically use this function to move the character instance of this player
    *   or perform any other action based on button activity
    */
    public void PlayerControllerState(string jsonControllerState)
    {

        // Destructure jsonControllerState
        ControllerState controllerState = JsonUtility.FromJson<ControllerState>(jsonControllerState);
        // TODO: Any of the following code may be modified or deleted

        // Move player based on joystick
        // foreach (Transform child in canvas.transform)
        // {
        //     if (child.GetComponent<NameScript>().GetName() == controllerState.name)
        //     {
        if (teams[controllerState.name] == "person") {
            people[controllerState.name].GetComponent<PersonScript>().passController(controllerState);
        } else {
            GameObject p = pigeons[controllerState.name];

            PigeonScript script = p.GetComponent<PigeonScript>();
            script.passController(controllerState);
        }
        //     }
        // }
    }

    /*
    * Madder class: Message
    * This class is used to serialize messages sent to controllers (both individual and all controllers)
    * The following Message names work with Madder controllers:
    *   - "vibrate": Vibrate the player's controller
    * The message parameter has no current use for Madder controllers
    */
    public class Message {
        public string name;
        public string message;
    }

    /*
    * Madder class: Stat
    * This class is used to store and update a stat of a player across sessions
    * The title is the name of the stat and is REQUIRED
    * The value is the value of the stat and is REQUIRED
    * You may create children classes of Stat to store more complex stats
    */
    [System.Serializable]
    public class Stat {
        public string title;
        public int value;

        public Stat(string initTitle, int initValue) {
            title = initTitle;
            value = initValue;
        }
    }
    // TODO: Add any additional children classes of Stat here

    /*
    * Madder class: GameStats
    * This class is used to store and update the stats of a player for your game across sessions
    * All fields must be of type Stat or a child class of Stat
    * No fields or methods are required and you can add any additional fields or methods
    */
    [System.Serializable]
    public class GameStats {
        // TODO: Add/Remove any fields of type Stat or a child class of Stat here
        public Stat gamesPlayed;
        public GameStats() {
            gamesPlayed = new Stat("Games Played", 0);
        }
        public void addGamePlayed() {
            gamesPlayed.value++;
        }
    }

    /*
    * Madder class: PlayerJoin
    * This class is used to serialize the data sent to the PlayerJoined function
    * This class should not be altered
    */
    public class PlayerJoin {
        public string name;
        public GameStats stats; 
    }

    /*
    * Madder class: Joystick
    * This class is used to serialize the joystick data sent to the PlayerControllerState function
    * This class should not be altered for the Madder controller
    */
    [System.Serializable]
    public class Joystick {
        public int x;
        public int y; 
        public Joystick(int initX, int initY) {
            x = initX;
            y = initY;
        }
    }

    /*
    * Madder class: ControllerState
    * This class is used to serialize the data sent to the PlayerControllerState function
    * This class should not be altered for the Madder controller
    */
    public class ControllerState {
        public string name;
        public Joystick joystick;
        public bool circle;
        public bool triangle;
        public bool plus;
    }
}
