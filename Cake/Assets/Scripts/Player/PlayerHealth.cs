using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float health;
    public bool alive = true;
    public Animator animator;
    public Transform eyeRight;
    public Transform eyeLeft;
    bool hasEyeRight;
    bool hasEyeLeft;

    void Start()
    {
        health = maxHealth;
        hasEyeRight = true;
        hasEyeLeft = true;
        alive = true;
        //Debug.Log("(2/3) * maxHealth: "+ (2/3) * maxHealth+ " , (1/3) * maxHealth: "+(1/3) * maxHealth);
    }

    void Update()
    {
        if (health <= (2f/3f) * maxHealth && hasEyeLeft){
            SeperateEye(false, true);
        }
        if (health <= (1f/3f) * maxHealth && hasEyeRight){
            SeperateEye(true, false);
        }
        if (health<=0 && alive){
            BeginDeath();
        }
    }

    public void SeperateEye(bool right = true, bool left = true){
        Debug.Log("separation "+right+" , "+left);
        if (right){
            hasEyeRight = false;
            
            GameObject g = GameObject.Instantiate(eyeRight.gameObject, eyeRight.position, Quaternion.identity);
            g.GetComponent<Collider>().isTrigger = false;
            Rigidbody r = g.AddComponent(typeof(Rigidbody)) as Rigidbody;
            r.mass = 0.01f;

            eyeRight.GetComponent<Renderer>().enabled = false;
        }
        if (left){
            hasEyeLeft = false;

            GameObject g = GameObject.Instantiate(eyeLeft.gameObject, eyeLeft.position, Quaternion.identity);
            g.GetComponent<Collider>().isTrigger = false;
            Rigidbody r = g.AddComponent(typeof(Rigidbody)) as Rigidbody;
            r.mass = 0.01f;
            
            eyeLeft.GetComponent<Renderer>().enabled = false;
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
        Debug.Log("Death completed");
    }
}
