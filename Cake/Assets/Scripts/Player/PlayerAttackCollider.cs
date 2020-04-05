using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttackCollider : MonoBehaviour
{
    public string[] targetTags;

    public Attack attack;
    public Attack.AttackType attackType = 0; //0=bite, 1=tongue swing

    void OnTriggerEnter(Collider other){
        if (targetTags.Contains(other.tag)){
            //Debug.Log("Send attack message to: "+other.name);
            other.SendMessage("OnAttacked", new AttackMessageArgs(attack.transform, attackType), SendMessageOptions.DontRequireReceiver);
        }
    }
}
