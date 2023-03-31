using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalking : EnemyBaseState
{
    public GameObject enemyReference;
    public GameObject playerReference;
    public CharacterSwap swapReference;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    public FieldOfViewScript fovReference;

    public override void EnterState(FieldOfViewScript enemy)
    {
        enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        swapReference = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CharacterSwap>();
        playerReference = swapReference.currentPlayer;
        navMeshAgent = enemyReference.GetComponent<NavMeshAgent>();
        fovReference = enemyReference.GetComponent<FieldOfViewScript>();

        navMeshAgent.speed = 2.5f;
        UpdateState(fovReference);
        navMeshAgent.isStopped = false;
    }
    public override void UpdateState(FieldOfViewScript enemy)
    {
        navMeshAgent.SetDestination(playerReference.transform.position);
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
}
