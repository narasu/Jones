using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }
    [Header("UI objects")]
    public GameObject mainMenuObject;
    public GameObject pauseObject;
    public GameObject deadMenuObject;
    public GameObject winMenuObject;
    public GameObject trapTextObject;
    [HideInInspector] public TextMeshProUGUI trapText;

    [Header("Player")]
    [SerializeField] private GameObject playerObject;
    private GameObject playerInstance;
    [SerializeField] private Transform playerSpawn;

    private Vector3 playerSpawnPos; //these variables store position and rotation values for the spawn point
    private Quaternion playerSpawnRot; //necessary because the game seems to have trouble accessing these values directly from the Transform

    //these variables will remember all enemies, detection triggers and traps so they can easily be modified later
    private GameObject[] enemies;
    private GameObject[] enemyTriggers;
    private List<GameObject> traps;

    private GameFSM fsm;

    private void Awake()
    {
        fsm = new GameFSM();
        fsm.Initialize();

        playerSpawnPos = playerSpawn.position;
        playerSpawnRot = playerSpawn.rotation;

        //store all enemies and triggers in arrays
        if (enemies==null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        if (enemyTriggers == null)
        {
            enemyTriggers = GameObject.FindGameObjectsWithTag("Trigger");
        }

        //initialize list of traps
        traps = new List<GameObject>();
        
        //store textmeshpro component for trap text
        trapText = trapTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GotoMainMenu();
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    //Initialize the scene (this method is called from UI onclick)
    public void StartLevel()
    {
        Debug.Log("Starting...");
        //instantiate player object if none exists, else activate it
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerObject);
        }
        else 
        {
            playerInstance.SetActive(true);
        }

        //move player to spawnpoint and refill traps
        playerInstance.transform.SetPositionAndRotation(playerSpawnPos, playerSpawnRot);
        playerInstance.GetComponent<Player>().RefillTraps();

        //activate all enemies
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        //activate all detection triggers
        foreach (GameObject trigger in enemyTriggers)
        {
            trigger.SetActive(true);
        }
    }

    //End the scene
    public void EndLevel()
    {
        //reset all enemies to their starting positions and state
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().ResetPosition();
            enemy.GetComponent<Enemy>().Idle();
            enemy.SetActive(false);
        }

        //remove all traps from the level
        for (int i = 0; i < traps.Count; i++)
        {
            Destroy(traps[i]);
        }
        traps.Clear();

        //reset player object position and deactivate
        playerInstance.transform.SetPositionAndRotation(playerSpawn.position, playerSpawn.rotation);
        playerInstance.SetActive(false);
    }

    //Called when the player sets a trap so it is immediately added to the list
    public void AddTrapToList(GameObject trap)
    {
        traps.Add(trap);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Methods for switching to each state
    public void GotoMainMenu()
    {
        fsm.GotoState(GameStateType.MainMenu);
    }
    public void GotoPlay()
    {
        fsm.GotoState(GameStateType.Play);
    }
    public void GotoPause()
    {
        fsm.GotoState(GameStateType.Pause);
    }
    public void GotoWin()
    {
        fsm.GotoState(GameStateType.Win);
    }
    public void GotoDead()
    {
        fsm.GotoState(GameStateType.Dead);
    }

}
