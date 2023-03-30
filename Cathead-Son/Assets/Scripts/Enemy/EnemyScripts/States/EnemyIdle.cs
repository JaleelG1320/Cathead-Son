using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyBaseState
{
    public GameObject enemyReference;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private int destinationPoint = 0;
    public Transform[] points;
    public FieldOfViewScript fovReference;


    public override void EnterState(FieldOfViewScript enemy)
    {
        enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        navMeshAgent = enemyReference.GetComponent<NavMeshAgent>();
        fovReference = enemyReference.GetComponent<FieldOfViewScript>();
        points = fovReference.idlePositions;
        UpdateState(fovReference);
        navMeshAgent.isStopped = false;
        fovReference.currentlySwitching = false;
    }
    public override void UpdateState(FieldOfViewScript enemy)
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if ((!navMeshAgent.pathPending) && (navMeshAgent.remainingDistance < 0.5f))
        {
            GoToNextPoint();
        }  
        
    }
    public override void ExitState(FieldOfViewScript enemy)
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 2.5f;
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
        if (points.Length == 0)
        {
            return;
        }
        // Set the agent to go to the currently selected destination.
        navMeshAgent.destination = points[destinationPoint].position;
        navMeshAgent.SetDestination(navMeshAgent.destination);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

}
