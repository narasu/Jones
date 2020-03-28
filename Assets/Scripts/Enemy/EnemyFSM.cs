using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM
{
    public Enemy owner { get; private set; }
    public Transform Transform { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }


#pragma warning disable 0649
    private Dictionary<EnemyStateType, EnemyState> states;
#pragma warning restore 0649

    private EnemyState currentState;

    public void Initialize(Enemy owner)
    {
        //build references to enemy variables
        this.owner = owner;
        Transform = owner.transform;
        NavMeshAgent = owner.navMeshAgent;

        states = new Dictionary<EnemyStateType, EnemyState>();
    }

    public void AddState(EnemyStateType newType, EnemyState newState)
    {
        states.Add(newType, newState);
        states[newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.Update();
        //Debug.Log(currentState);
    }

    public void GotoState(EnemyStateType key)
    {
        if(!states.ContainsKey(key))
            return;

        currentState?.Exit();

        currentState = states[key];
        currentState.Enter();
    }

    public EnemyState GetState()
    {
        if (currentState == null)
            return null;

        return currentState;
    }
}
