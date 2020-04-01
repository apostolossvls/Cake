using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeEye : MonoBehaviour
{
    public bool isRight;
    public bool isAttached = true;

    void Start()
    {
        isAttached = true;
    }

    void OnAttacked(MessageArgs msg){
        if (!isAttached){
            msg.recieved = true;
            PlayerHealth playerHealth = msg.sender.GetComponentInParent<PlayerHealth>();
            if (playerHealth){
                playerHealth.AttachEye(transform, isRight);
            }
        }
    }
}
