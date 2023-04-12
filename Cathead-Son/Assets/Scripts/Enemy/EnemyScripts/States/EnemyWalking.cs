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

    public EnemyWalking(CharacterSwap _swapReference, FieldOfViewScript _fovReference, NavMeshAgent _navMeshAgent)
    {
        //this.enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        this.swapReference = _swapReference;
        //this.playerReference = swapReference.currentPlayer;
        this.navMeshAgent = _navMeshAgent;
        this.fovReference = _fovReference;
    }

    public override void EnterState(FieldOfViewScript enemy)
    {
        /*
        enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        swapReference = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CharacterSwap>();
        playerReference = swapReference.currentPlayer;
        navMeshAgent = enemyReference.GetComponent<NavMeshAgent>();
        fovReference = enemyReference.GetComponent<FieldOfViewScript>();
        */
        this.playerReference = swapReference.currentPlayer;
        this.navMeshAgent.speed = 2.5f;
        UpdateState(this.fovReference);
        this.navMeshAgent.isStopped = false;
    }
    public override void UpdateState(FieldOfViewScript enemy)
    {
        this.navMeshAgent.SetDestination(this.playerReference.transform.position);
    }
    public override void ExitState(FieldOfViewScript enemy)
    {
        this.navMeshAgent.isStopped = true;
        this.navMeshAgent.speed = 2.5f;
    }
    public override void HandleSight(FieldOfViewScript enemy)
    {

    }
    public override void HandleAudio(FieldOfViewScript enemy)
    {
        
    }
}
