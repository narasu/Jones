using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType { Idle, Chase, Return, Dead }
public abstract class EnemyState
{
    protected EnemyFSM owner;
    protected Enemy enemy;
    public void Initialize(EnemyFSM owner)
    {
        this.owner = owner;
        enemy = owner.owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class EnemyIdle : EnemyState
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

    }
}

public class EnemyChase : EnemyState
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        owner.NavMeshAgent.SetDestination(enemy.target.position);
    }

    public override void Exit()
    {
        
    }
}

public class EnemyReturn : EnemyState
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        owner.NavMeshAgent.SetDestination(enemy.startingPoint);
    }

    public override void Exit()
    {

    }
}

public class EnemyDead : EnemyState
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = true;
        Debug.Log("oh nooo am ded");
        
    }

    public override void Update()
    {
        //owner.NavMeshAgent.SetDestination()
    }

    public override void Exit()
    {

    }
}