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

//standing still at starting position
public class EnemyIdle : EnemyState
{
    public override void Enter()
    {
        if (enemy.isActiveAndEnabled)
            owner.NavMeshAgent.isStopped = true;
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}

//chasing the player
public class EnemyChase : EnemyState
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        owner.NavMeshAgent.SetDestination(enemy.target.position);
        
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.hitRange)
        {
            //Debug.Log("Close enough");
            //owner.NavMeshAgent.isStopped = true;
            enemy.Return();
        }
    }

    public override void Exit()
    {
        
    }
}

//returning to starting position
public class EnemyReturn : EnemyState
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
        owner.NavMeshAgent.SetDestination(enemy.startingPoint);
        
        //Debug.Log("Returning");
    }

    public override void Update()
    {
        if (enemy.transform.position == enemy.startingPoint)
        {
            enemy.Idle();
        }
    }

    public override void Exit()
    {

    }
}

//dead
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