using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FieldOfViewScript : MonoBehaviour
{
    [Header("Enemy Properties")]
    public float radius;
    public float hearRadius;
    [Range(0, 360)] public float angle;


    [Header("References")]
    public GameObject playerRef;
    public CharacterSwap swapReference;
    private PlayerController controllerRef;
    public Sprite enemyIdleImage;
    public Sprite enemyWalkingImage;
    public Sprite enemyRunningImage;
    public Sprite enemyStunnedImage;
    public SpriteRenderer canvasImage;

    [Header("Layer Masks")]
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    [Header("States")]
    public bool canSeePlayer;
    //public bool canHearPlayer;
    //public bool isStunned;
    public bool isIdle;

    [Header("State Machine")]
    EnemyBaseState enemyBaseState;
    EnemyBaseState currentState;
    EnemyWalking enemyWalkingReference = new EnemyWalking();
    EnemyRunning enemyRunningReference = new EnemyRunning();
    EnemyIdle enemyIdleReference = new EnemyIdle();
    public Transform[] idlePositions;
    public bool currentlySwitching;
    private Animator animator;
    [SerializeField] private AnimationClip enemyIdle;
    [SerializeField] private AnimationClip enemyWalking;
    [SerializeField] private AnimationClip enemyRunning;



    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
        playerRef = GameObject.FindGameObjectWithTag("CurrentPlayer");
        controllerRef = playerRef.GetComponent<PlayerController>();
        enemyBaseState = enemyIdleReference;
        canvasImage.sprite = enemyIdleImage;
        enemyBaseState.EnterState(this);
        StartCoroutine(FOVRoutine());
        currentlySwitching = false;
        //animator = gameObject.GetComponentInChildren<Animator>();
        //animator.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        playerRef = swapReference.currentPlayer; 
        //Debug.Log("canHearPlayer is: " + canHearPlayer);
        Debug.Log("canSeePlayer is: " + canSeePlayer);
        Debug.Log("isIdle is: " + isIdle);

        if (canSeePlayer)
        {
            //canHearPlayer = false;
            isIdle = false;
            SwitchState(enemyWalkingReference);
            canvasImage.sprite = enemyWalkingImage;
        }

        /*
        if (canHearPlayer)
        {
            isIdle = false;
            SwitchState(enemyWalkingReference);
            canvasImage.sprite = enemyWalkingImage;
            //animator.SetBool("isIdle", false);
            //animator.SetBool("isWalking", true);
        }
        */

        else
        {
            isIdle = true;
            SwitchState(enemyIdleReference);
            canvasImage.sprite = enemyIdleImage;
            //animator.SetBool("isRunning", false);
            //animator.SetBool("isWalking", false);
            //animator.SetBool("isIdle", true);
        }



        // if (canHearPlayer && !(playerRef.GetComponent<ThirdPersonController>().forceDirection.normalized == Vector3.zero))
        // {
        //     isIdle = false;
        //     SwitchState(enemyWalkingReference);
        //     canvasImage.sprite = enemyWalkingImage;
        //     //animator.SetBool("isIdle", false);
        //     //animator.SetBool("isWalking", true);
        //     if (canSeePlayer)
        //     {
        //         isIdle = false;
        //         SwitchState(enemyRunningReference);
        //         canvasImage.sprite = enemyRunningImage;
        //         //animator.SetBool("isRunning", true);
        //         //animator.SetBool("isWalking", false);
        //     }
        // }
        // else
        // {
            
        // }

        canvasImage.transform.LookAt(playerRef.transform);
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);    

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            //FieldOfAudioCheck(); 
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else 
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

/*
   private void FieldOfAudioCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, hearRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (!(swapReference.currentPlayer.GetComponent<Rigidbody>().velocity.normalized == Vector3.zero))
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canHearPlayer = true;
                }
                else
                {
                    canHearPlayer = false;
                }
            }
            else 
            {
                canHearPlayer = false;
            }
        }
        else if (canHearPlayer)
        {
            canHearPlayer = false;
        }
    }
*/

    private void SwitchState(EnemyBaseState enemy)
    {
        
        Debug.Log("Switched state to: " + enemy.ToString());
        currentlySwitching = true;
        enemyBaseState.ExitState(this);

        enemyBaseState = enemy;

        enemyBaseState.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "CurrentPlayer")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("LoseScene");
        }
    }
}
