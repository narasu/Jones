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

public class EnemyIdle : State
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = true;
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        owner.NavMeshAgent.isStopped = false;
    }
}

public class EnemyChase : State
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        //owner.NavMeshAgent.SetDestination()
    }

    public override void Exit()
    {
        
    }
}

public class EnemyReturn : State
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        //owner.NavMeshAgent.SetDestination()
    }

    public override void Exit()
    {

    }
}