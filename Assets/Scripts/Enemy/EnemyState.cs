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

//Standing still at starting position
public class EnemyIdle : EnemyState
{
    public override void Enter()
    {
        if (enemy.isActiveAndEnabled)
        {
            owner.NavMeshAgent.isStopped = true;
        }
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}

//Chasing the player
public class EnemyChase : EnemyState
{
    Coroutine footsteps;
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = false;
        footsteps = enemy.StartCoroutine("Footsteps");
        
    }

    public override void Update()
    {
        owner.NavMeshAgent.SetDestination(enemy.target.position);
        
        
        //the player's current position
        float distA = Vector3.Distance(enemy.transform.position, enemy.target.position);

        // the player's next position
        float distB = Vector3.Distance(enemy.transform.position, Player.Instance.NextPos);

        

        //if the player is walking away, go to player's next position
        if (distB > distA)
        {
            owner.NavMeshAgent.SetDestination(Player.Instance.NextPos);
        }
        else //if not, go to player's current position
        {
            owner.NavMeshAgent.SetDestination(enemy.target.position);
        }

        //if the enemy hits the player
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) < enemy.hitRange)
        {
            GameManager.Instance.GotoDead();
        }
    }

    public override void Exit()
    {
        enemy.StopCoroutine(footsteps);
    }
}

//Dead state
public class EnemyDead : EnemyState
{
    public override void Enter()
    {
        owner.NavMeshAgent.isStopped = true;
        enemy.MakeNoise(enemy.dying);
        Debug.Log("oh nooo am ded");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}