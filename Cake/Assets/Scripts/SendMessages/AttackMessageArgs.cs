using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMessageArgs
{
    public Transform sender;
    public bool recieved;
    public Attack.AttackType type;


    public AttackMessageArgs(Transform sender, Attack.AttackType type=0)
    {
        this.sender = sender;
        this.type = type;
        recieved = false;
    }
}
