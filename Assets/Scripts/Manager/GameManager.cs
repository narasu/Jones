using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject mainMenuObject;
    public GameObject pauseObject;

    public GameObject playerObject;
    public GameObject playerInstance;
    public Transform playerSpawn;

    private GameObject[] enemies;
    private GameObject[] enemyTriggers;
    private List<GameObject> traps;

    private GameFSM fsm;

    private void Awake()
    {
        fsm = new GameFSM();
        fsm.Initialize();
        
        
        if (enemies==null) 
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyTriggers == null)
            enemyTriggers = GameObject.FindGameObjectsWithTag("Trigger");

        traps = new List<GameObject>();
    }

    private void Start()
    {
        GotoMainMenu();
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    public void StartLevel()
    {
        Debug.Log("Starting...");
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerObject);
        }
        else 
        {
            playerInstance.SetActive(true);
        }
        playerInstance.transform.SetPositionAndRotation(playerSpawn.position, playerSpawn.rotation);
        playerInstance.GetComponent<Player>().RefillTraps();

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        foreach (GameObject trigger in enemyTriggers)
        {
            trigger.SetActive(true);
        }
    }

    public void EndLevel()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
            enemy.GetComponent<Enemy>()?.ResetPosition();
            enemy.GetComponent<Enemy>()?.Idle();
        }

        for (int i = 0; i < traps.Count; i++)
            Destroy(traps[i]);
        traps.Clear();

        playerInstance.SetActive(false);
    }

    public void AddTrapToList(GameObject trap)
    {
        traps.Add(trap);
    }

    //methods for switching to each state
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
