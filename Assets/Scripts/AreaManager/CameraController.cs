using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : SingleTon<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        SetPlaterCameraFollow();
    }

    public void SetPlaterCameraFollow()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}