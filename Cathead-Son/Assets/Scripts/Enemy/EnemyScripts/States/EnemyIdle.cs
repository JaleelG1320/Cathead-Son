using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyBaseState
{
    public GameObject enemyReference;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    public GameObject playerReference;
     public CharacterSwap swapReference;
    private int destinationPoint = 0;
    public Transform[] points;
    public FieldOfViewScript fovReference;

    

    public EnemyIdle(Transform[] _points, CharacterSwap _swapReference, FieldOfViewScript _fovReference, NavMeshAgent _navMeshAgent)
    {
        this.points = _points;
        this.swapReference = _swapReference;
        this.fovReference = _fovReference;
        //this.enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        //playerReference = this.swapReference.currentPlayer;
        this.navMeshAgent = _navMeshAgent;
    }


    public override void EnterState(FieldOfViewScript enemy)
    {
        /*
        enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        swapReference = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CharacterSwap>();
        playerReference = swapReference.currentPlayer;
        navMeshAgent = enemyReference.GetComponent<NavMeshAgent>();
        fovReference = enemyReference.GetComponent<FieldOfViewScript>();
        points = fovReference.idlePositions;
        */
        this.playerReference = this.swapReference.currentPlayer;
        UpdateState(this.fovReference);
        this.navMeshAgent.isStopped = false;
        this.fovReference.currentlySwitching = false;
    }
    public override void UpdateState(FieldOfViewScript enemy)
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if ((!this.navMeshAgent.pathPending) && (this.navMeshAgent.remainingDistance < 0.5f))
        {
            GoToNextPoint();
        }  
        
    }
    public override void ExitState(FieldOfViewScript enemy)
    {
        this.navMeshAgent.isStopped = true;
        //this.navMeshAgent.speed = 2.5f;
    }
    public override void HandleSight(FieldOfViewScript enemy)
    {

    }
    public override void HandleAudio(FieldOfViewScript enemy)
    {
        
    }

    void GoToNextPoint() 
    {
        // Returns if no points have been set up
        if (this.points.Length == 0)
        {
            return;
        }
        // Set the agent to go to the currently selected destination.
        this.navMeshAgent.destination = points[destinationPoint].position;
        this.navMeshAgent.SetDestination(navMeshAgent.destination);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

}
