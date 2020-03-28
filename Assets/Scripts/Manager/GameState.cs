using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType { MainMenu, Play, Pause, Win, Dead }

public abstract class GameState
{
    /*
    // will have to consider if this is worth using, it's inefficient since the constructor gets called repeatedly for each subclass

    protected GameManager gameManager;

    public GameState()
    {
        gameManager = GameManager.Instance; 
        //Debug.Log(gameManager);
    }
    */
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class MainMenuState : GameState 
{
    public override void Enter()
    {
        //open main menu, freeze game, unlock cursor
        GameManager.Instance.mainMenuObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        //close menu, unfreeze game
        GameManager.Instance.mainMenuObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
public class PlayState : GameState
{
    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //GameManager.Instance.playerInstance?.SetActive(true);
        //if (GameManager.Instance.playerInstance != null)
        //    GameManager.Instance.playerInstance.SetActive(true);


        // todo: set hud active
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.GotoPause();
        }
            
    }
    public override void Exit()
    {
        // todo: set hud inactive
        GameManager.Instance.playerInstance?.SetActive(false);
    }
}
public class PauseState : GameState
{
    public override void Enter()
    {
        //open pause menu, freeze game, unlock cursor
        GameManager.Instance.pauseObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.GotoPlay();
    }
    public override void Exit()
    {
        GameManager.Instance.pauseObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
public class WinState : GameState
{
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
public class DeadState : GameState
{
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}