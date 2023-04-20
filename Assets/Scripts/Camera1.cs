using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class Camera1 : MonoBehaviour
{
    public CinemachineVirtualCamera FirstPersonCamera;
    public CinemachineVirtualCamera ThirdPersonCamera;
    public bool FirstPerson;
    public GameObject FPC;
    public GameObject TPC;

    private void FixedUpdate()
    {
        CameraController();  
    }

    private void CameraController()
    {
        if (Input.GetButtonDown("Fire2") && !FirstPerson)
        {
            FirstPersonCamera.Priority = 1;
            ThirdPersonCamera.Priority = 0;
            FirstPerson = true;
            Debug.Log("Primera persona");

        }
        else if(Input.GetButtonDown("Fire2") && FirstPerson)
        {
            ThirdPersonCamera.Priority = 1;
            FirstPersonCamera.Priority = 0;
            FirstPerson = false;
            Debug.Log("Tercera persona");

        }
    }
}
