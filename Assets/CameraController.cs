using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FrameWork.Utils;
using UnityEngine;

public class CameraController : UnitySingleton<CameraController>
{
    private CinemachineVirtualCamera _cinemachineVirtual;

    protected override void Awake()
    {
        base.Awake();
        _cinemachineVirtual = this.GetComponent<CinemachineVirtualCamera>();
        if (!_cinemachineVirtual)
        {
            DebugLogger.Log("Camera is null");
        }
    }

    public void GamePlayerEnter(GameObject target)
    {
        _cinemachineVirtual.Follow = target.transform;
    }
}
