using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FieldOfViewScript : MonoBehaviour
{
    public float radius;
    public float hearRadius;
    [Range(0, 360)] public float angle;

    public GameObject playerRef;
    private PlayerController controllerRef;
    public Sprite enemyIdleImage;
    public Sprite enemyWalkingImage;
    public Sprite enemyRunningImage;
    public Sprite enemyStunnedImage;
    public SpriteRenderer canvasImage;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool canHearPlayer;
    public bool isStunned;
    public bool isIdle;

    [Header("State Machine")]
    EnemyBaseState enemyBaseState;
    EnemyBaseState currentState;
    EnemyWalking enemyWalkingReference = new EnemyWalking();
    EnemyRunning enemyRunningReference = new EnemyRunning();
    EnemyIdle enemyIdleReference = new EnemyIdle();
    EnemyStunned enemyStunnedReference = new EnemyStunned();  
    public Transform[] idlePositions;
    public bool currentlySwitching;
    private Animator animator;
    [SerializeField] private AnimationClip enemyIdle;
    [SerializeField] private AnimationClip enemyWalking;
    [SerializeField] private AnimationClip enemyRunning;



    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        controllerRef = playerRef.GetComponent<PlayerController>();
        enemyBaseState = enemyIdleReference;
        canvasImage.sprite = enemyIdleImage;
        enemyBaseState.EnterState(this);
        StartCoroutine(FOVRoutine());
        currentlySwitching = false;
        animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canHearPlayer && (controllerRef.currentInput != Vector2.zero || !controllerRef.isCrouching))
        {
            isIdle = false;
            SwitchState(enemyWalkingReference);
            canvasImage.sprite = enemyWalkingImage;
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
            if (canSeePlayer)
            {
                isIdle = false;
                SwitchState(enemyRunningReference);
                canvasImage.sprite = enemyRunningImage;
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            SwitchState(enemyIdleReference);
            canvasImage.sprite = enemyIdleImage;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }

        if (isStunned)
        {
            SwitchState(enemyStunnedReference);
            canvasImage.sprite = enemyStunnedImage;
        }

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
            FieldOfAudioCheck(); 
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
                canSeePlayer = true;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void FieldOfAudioCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, hearRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
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
                canHearPlayer = true;
            }
        }
        else if (canHearPlayer)
        {
            canHearPlayer = false;
        }
    }

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
        if (collision.gameObject.tag == "Stun")
        {
            isStunned = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("LoseScene");
        }
    }
}
