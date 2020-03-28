using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM
{
    
    private Dictionary<GameStateType, GameState> states;

    public GameStateType CurrentStateType { get; private set; }
    private GameState currentState;
    private GameState previousState;

    public void Initialize()
    {
        states = new Dictionary<GameStateType, GameState>();

        states.Add(GameStateType.MainMenu, new MainMenuState());
        states.Add(GameStateType.Play, new PlayState());
        states.Add(GameStateType.Pause, new PauseState());
        states.Add(GameStateType.Win, new WinState());
        states.Add(GameStateType.Dead, new DeadState());
    }

    public void UpdateState()
    {
        currentState?.Update();
        //Debug.Log(currentState);
    }

    public void GotoState(GameStateType key)
    {
        if (!states.ContainsKey(key))
            return;

        currentState?.Exit();

        previousState = currentState;
        CurrentStateType = key;
        currentState = states[CurrentStateType];

        currentState.Enter();
    }
}
