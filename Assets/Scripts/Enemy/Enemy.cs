using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    
    private EnemyFSM fsm = new EnemyFSM();

#pragma warning disable 0649
    public Transform target;
#pragma warning restore 0649
    public Vector3 startingPoint;
    
    [SerializeField]private float speed;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        startingPoint = transform.position;
        
        //start state machine
        fsm.Initialize(this);

        //add states
        fsm.AddState(EnemyStateType.Idle, new EnemyIdle());
        fsm.AddState(EnemyStateType.Chase, new EnemyChase());
        fsm.AddState(EnemyStateType.Return, new EnemyReturn());
        fsm.AddState(EnemyStateType.Dead, new EnemyDead());

        //start in idle state
        fsm.GotoState(EnemyStateType.Idle);
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    public void ChasePlayer()
    {
        fsm.GotoState(EnemyStateType.Chase);
    }

    public void Return()
    {
        fsm.GotoState(EnemyStateType.Return);
    }

    public void Die()
    {
        fsm.GotoState(EnemyStateType.Dead);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag=="Player")
        {
            Return();
        }
    }
}
