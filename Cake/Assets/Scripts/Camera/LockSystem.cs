using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockSystem : MonoBehaviour
{
    public bool onLock;
    public CinemachineVirtualCameraBase mainVcam;
    public CinemachineVirtualCameraBase lockVcam;
    public int mainPriority;
    void Start()
    {
        mainPriority = mainVcam.GetComponent<CinemachineFreeLook>().Priority;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2)){
            onLock = !onLock;

            if (onLock){
                lockVcam.GetComponent<CinemachineVirtualCamera>().Priority = mainPriority + 1;
            }
            else {
                lockVcam.GetComponent<CinemachineVirtualCamera>().Priority = mainPriority - 1;
            }
        }
    }
}
