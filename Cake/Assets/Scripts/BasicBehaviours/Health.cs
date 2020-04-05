using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 1f;
    public float health;
    public bool alive = true;
    public Animator animator;

    void Start()
    {
        health = maxHealth;
        alive = true;
    }

    void Update()
    {
        if (health<=0 && alive){
            BeginDeath();
        }
    }

    public void OnAttacked(AttackMessageArgs msg){
        Debug.Log(gameObject.name+ ": Got attacked");
        msg.recieved = true;
        Attack attack = msg.sender.GetComponent<Attack>();
        if (attack){
            attack.DealDamageTo(this, msg.type);
        }
    }

    public void BeginDeath(){
        alive = false;
        if (!animator){
            Die();
        }
        else {
            animator.SetTrigger("Death");
        }
    }

    public void Die(){
        Debug.Log(gameObject.name+ ": Death completed");
        Destroy(gameObject);
    }
}
