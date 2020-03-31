using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public Animator animator;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health<=0){
            BeginDeath();
        }
    }

    public void BeginDeath(){
        if (!animator){
            BeginDeath();
        }
        else {
            animator.SetBool("Death", true);
        }
    }

    public void Die(){
        Destroy(gameObject);
    }
}
