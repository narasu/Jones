using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType { Idle, Chase, Return, Dead }
public abstract class State
{
    protected FSM owner;

    public void Initialize(FSM owner)
    {
        this.owner = owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
