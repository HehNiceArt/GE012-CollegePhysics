using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera1, VirtualCamera2, VirtualCamera3;
    private int activeCameraIndex = 0; 

    private void Start()
    {
        SetCameraPriorities(0); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            CycleCameras();
        }
    }

    void CycleCameras()
    {
        activeCameraIndex = (activeCameraIndex + 1) % 3;
        SetCameraPriorities(activeCameraIndex);
    }

    void SetCameraPriorities(int index)
    {
        if (index == 0)
        {
            VirtualCamera1.Priority = 10;
            VirtualCamera2.Priority = 5;
            VirtualCamera3.Priority = 2;
        }
        else if (index == 1)
        {
            VirtualCamera1.Priority = 5;
            VirtualCamera2.Priority = 10;
            VirtualCamera3.Priority = 2;
        }
        else if (index == 2)
        {
            VirtualCamera1.Priority = 2;
            VirtualCamera2.Priority = 5;
            VirtualCamera3.Priority = 10;
        }
    }
}
