using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttackCollider : MonoBehaviour
{
    public string[] targetTags;

    public Attack attack;

    void OnTriggerEnter(Collider other){
        if (targetTags.Contains(other.tag)){
            //Debug.Log("Send attack message to: "+other.name);
            other.SendMessage("OnAttacked", new MessageArgs(attack.transform), SendMessageOptions.DontRequireReceiver);
        }
    }
}
