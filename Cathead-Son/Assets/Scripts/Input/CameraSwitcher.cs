using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    public static List<CinemachineVirtualCameraBase> cameras = new List<CinemachineVirtualCameraBase>();

    public static CinemachineVirtualCameraBase _activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCameraBase camera)
    {
        return camera == _activeCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCameraBase camera)
    {
        camera.Priority = 10;
        _activeCamera = camera;
        Debug.Log("Switching camera ");
        foreach (CinemachineVirtualCameraBase cx in cameras)
        {
            if (!IsActiveCamera(cx))
            {
                Debug.Log("Hiding other camera " + cx);
                cx.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCameraBase camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCameraBase camera)
    {
        cameras.Remove(camera);
    }
}
