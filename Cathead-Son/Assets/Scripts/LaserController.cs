using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LaserController : MonoBehaviour, ITriggerable
{

    public delegate void OnCanMove();
    public static event OnCanMove ToggleLasers;

    public float MoveSpeed;
    public LayerMask TargetLayer;

    [SerializeField] private Transform _endTF;
    [SerializeField] private Transform _startTF;

    private Vector3 _endPoint;
    private Vector3 _startPoint;

    bool canMove;

    public void OnEnable()
    {
        ToggleLasers += ToggleLaserMovement;
    }

    public void OnDisable()
    {
        ToggleLasers -= ToggleLaserMovement;
    }
    public void Start()
    {
        _endPoint = _endTF.position;
        _startPoint = _startTF.position;
        ToggleLaserMovement();
    }
    private void ToggleLaserMovement()
    {
        canMove = !canMove;
    }


    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            float time = Mathf.PingPong(Time.time * MoveSpeed, 1);
            transform.position = Vector3.Lerp(_startPoint, _endPoint, time);
        }
    }

    public void OnTrigger()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        canMove = false;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((TargetLayer.value & (1 << other.gameObject.layer)) > 0)
            if (!HidingSpot.PlayerController.IsHiding)
                WinLossManager.gameEnd = true;
    }
}
