using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM
{
    public GameObject owner { get; private set; }

    public Transform Transform { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }


#pragma warning disable 0649
    private Dictionary<StateType, State> states;
#pragma warning restore 0649

    private State currentState;

    public void Initialize(GameObject owner)
    {
        this.owner = owner;
    }

    public void AddState(StateType newType, State newState)
    {
        states.Add(newType, newState);
        states[newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.Update();
    }

    public void GotoState(StateType key)
    {
        if(!states.ContainsKey(key))
        {
            return;
        }

        currentState?.Exit();

        currentState = states[key];
        currentState.Enter();
    }
}
