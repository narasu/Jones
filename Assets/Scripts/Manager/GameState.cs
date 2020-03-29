using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType { MainMenu, Play, Pause, Win, Dead }

public abstract class GameState
{
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
        GameManager.Instance.trapTextObject.SetActive(true);
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.GotoPause();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Player.Instance?.SetTrap();
        }

        GameManager.Instance.trapText.text = "Traps: " + Player.Instance.CurrentTraps.ToString();
    }
    public override void Exit()
    {
        GameManager.Instance.trapTextObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.P))
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
        GameManager.Instance.winMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        GameManager.Instance.winMenuObject.SetActive(false);
    }
}
public class DeadState : GameState
{
    public override void Enter()
    {
        GameManager.Instance.deadMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        GameManager.Instance.deadMenuObject.SetActive(false);
    }
}