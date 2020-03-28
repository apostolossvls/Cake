using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations;

public class LockSystem : MonoBehaviour
{
    public bool onLock;
    public Transform player;
    public Transform target;
    public CinemachineVirtualCameraBase mainVcam;
    public CinemachineVirtualCameraBase lockVcam;
    public LayerMask layerMask;
    public Transform lockIcon;
    public float lockRadius = 20f;
    public float lockAngle =  15.0f;
    public int mainPriority;
    ConstraintSource constraintSource;

    void Start()
    {
        mainPriority = mainVcam.GetComponent<CinemachineFreeLook>().Priority;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2)){
            onLock = !onLock;
            target = FindTarget();
            SwitchCamera(target);
        }

        if (lockIcon && target!=player){
            lockIcon.rotation = transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
        }
    }

    Transform FindTarget(){
        Transform t = player;

        Collider[] hitColliders = Physics.OverlapSphere(player.position, lockRadius, layerMask);

        float dis = int.MaxValue;

        int i = 0;
        while (i < hitColliders.Length)
        {
            //FOR EACH OBJECT
            Vector3 targetDir = hitColliders[i].transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            if (angle < lockAngle){
                float tempDis = Vector3.Distance(player.position, hitColliders[i].transform.position);
                if (tempDis < dis){
                    t = hitColliders[i].transform;
                    dis = tempDis;
                }
            }

            i++;
        }

        return t;
    }

    void SwitchCamera(Transform t){
        if (onLock && t){
            if (t!=player){
                lockIcon.gameObject.SetActive(true);
                PositionConstraint pc = lockIcon.GetComponent<PositionConstraint>();
                if (pc){
                    if (pc.sourceCount > 0) pc.RemoveSource(0);
                    constraintSource.sourceTransform = t;
                    constraintSource.weight = 1;
                    pc.AddSource(constraintSource);
                    //pc.translationOffset = Vector3.zero;
                    //pc.translationAtRest = Vector3.zero;
                }
                lockVcam.GetComponent<CinemachineVirtualCamera>().Priority = mainPriority + 1;
                return;
            }
        }

        lockIcon.gameObject.SetActive(false);
        lockVcam.GetComponent<CinemachineVirtualCamera>().Priority = mainPriority - 1;
    }
}
