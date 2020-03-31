using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerAnimationReciever : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public void deathEnd(){
        playerHealth.Die();
    }
}
