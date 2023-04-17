using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossEventTrigger : MonoBehaviour, ITriggerable
{
    public CinemachineVirtualCameraBase RoomCamera;
    public GameObject Anvil;
    public Transform BossPos;
    public float EventWaitTime = 3f;
    public float EventTotalTime = 6f;

    public int sequenceNum;
    public static Transform[] idlePositions = new Transform[3];
    GameObject boss;
    public static int bossHitCount = 0;
    public void Awake()
    {
        bossHitCount = 0;
        idlePositions[sequenceNum] = BossPos;
        RoomCamera = GetComponentInChildren<CinemachineVirtualCameraBase>();
        CameraSwitcher.Register(RoomCamera.gameObject.GetComponent<CinemachineVirtualCameraBase>());
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    int lastCount;
    public void OnTrigger()
    {
        lastCount = bossHitCount;
        CameraSwitcher.SwitchCamera(RoomCamera);
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
        Invoke(nameof(DropAnvil), EventWaitTime);
        Invoke(nameof(SwitchBack), EventTotalTime);
    }

    public void DropAnvil()
    {
        Anvil.transform.position = new Vector3(BossPos.transform.position.x, Anvil.transform.position.y, BossPos.transform.position.z);
        Anvil.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void SwitchBack()
    {
        if (bossHitCount <= lastCount)
        {
            // Player Missed! Game Over!
            Destroy(this);
            return;
        }
        if(bossHitCount >= 3)
        {
            // Boss Hit 3 Times! Game won!
            Destroy(this);
            return;
        }

        switch (sequenceNum)
        {
            case 0:
                boss.GetComponent<FieldOfViewScript>().idlePositions[0] = idlePositions[1];
                break;
            case 1:
                boss.GetComponent<FieldOfViewScript>().idlePositions[0] = idlePositions[2];
                break;
        }

        CameraSwitcher.SwitchCamera(CameraSwitcher.cameras[CameraSwitcher.cameras.Count - 1]);
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
    }

}
