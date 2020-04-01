using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageArgs
{
    public Transform sender;
    public bool recieved;

    public MessageArgs(Transform sender){
        this.sender = sender;
        recieved = false;
    }
}
