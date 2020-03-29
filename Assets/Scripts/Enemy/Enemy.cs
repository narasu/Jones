using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    
    private EnemyFSM fsm = new EnemyFSM();

    public Transform target;
    public float hitRange;

    [HideInInspector] public Vector3 startingPoint;

    [HideInInspector] public AudioSource audioSource;
    public AudioClip growl;
    public AudioClip footsteps;
    public AudioClip dying;
    [SerializeField] private float footstepInterval = 0.3f;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        startingPoint = transform.position;

    }
    private void Start()
    {
        //start state machine
        fsm.Initialize(this);

        //add states
        fsm.AddState(EnemyStateType.Idle, new EnemyIdle());
        fsm.AddState(EnemyStateType.Chase, new EnemyChase());
        //fsm.AddState(EnemyStateType.Return, new EnemyReturn());
        fsm.AddState(EnemyStateType.Dead, new EnemyDead());

        //start in idle state
        Idle();
    }

    private void Update()
    {
        fsm.UpdateState();

        if (Input.GetKeyDown(KeyCode.F))
            ResetPosition();
    }

    //footstep sound effect plays every footstepInterval
    public IEnumerator Footsteps()
    {
        while (true)
        {
            MakeNoise(footsteps);
            yield return new WaitForSeconds(footstepInterval);
        }
    }

    public void MakeNoise(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, 1);
    }

    public void ResetPosition()
    {
        //transform.position = startingPoint;
        navMeshAgent.Warp(startingPoint);
    }

    public void Idle()
    {
        fsm.GotoState(EnemyStateType.Idle);
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

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Return();
        }
    }
    */
}
