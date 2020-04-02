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
    bool eyeDetach2_3;
    bool eyeDetach1_3;

    void Start()
    {
        health = maxHealth;
        hasEyeRight = true;
        hasEyeLeft = true;
        eyeDetach2_3 = false;
        eyeDetach1_3 = false;
        alive = true;
        //Debug.Log("(2/3) * maxHealth: "+ (2/3) * maxHealth+ " , (1/3) * maxHealth: "+(1/3) * maxHealth);
    }

    void Update()
    {
        if (health <= (2f/3f) * maxHealth && !eyeDetach2_3){
            HealthEyeSeperation();
        }
        if (health <= (1f/3f) * maxHealth && !eyeDetach1_3){
            HealthEyeSeperation();
        }
        if (health<=0 && alive){
            BeginDeath();
        }
    }

    public void HealthEyeSeperation(){
        bool h2_3 = health <= (2f/3f) * maxHealth;
        bool h1_3 = health <= (1f/3f) * maxHealth; 
        //if both booleans are true on the same frame/call it will trigger only the first one
        //because OnUpdate are different if(s) and HealthEyeSeperation method will be called twice

        if (h2_3 && !eyeDetach2_3){ //if 2/3 health and lower and hasn't detach yet
            eyeDetach2_3 = true;

            if (hasEyeLeft){
                SeperateEye(false, true); //detach left
            }
            else if (hasEyeRight){
                SeperateEye(true, false); //detach right
            }
            else {
                eyeDetach2_3 = false;
                Debug.LogWarning("hasEye booleans are both false (r,l): "+hasEyeRight+" , "+hasEyeLeft+". Trying to separate for 2/3 health");
            }
        }
        else if (h1_3 && !eyeDetach1_3){
            eyeDetach2_3 = true;

            if (hasEyeRight){
                SeperateEye(true, false); //detach right
            }
            else if (hasEyeLeft){
                SeperateEye(false, true); //detach left
            }
            else {
                eyeDetach1_3 = false;
                Debug.LogWarning("hasEye booleans are both false (r,l): "+hasEyeRight+" , "+hasEyeLeft+". Trying to separate for 1/3 health");
            }
        }
    }

    public void SeperateEye(bool right = true, bool left = true){
        Debug.Log("separation "+right+" , "+left);
        if (right){
            hasEyeRight = false;
            
            eyeRight.GetComponent<CakeEye>().isAttached = false;

            GameObject g = GameObject.Instantiate(eyeRight.gameObject, eyeRight.position, Quaternion.identity);
            g.GetComponent<Collider>().isTrigger = false;
            Rigidbody r = g.AddComponent(typeof(Rigidbody)) as Rigidbody;
            r.mass = 0.01f;

            eyeRight.GetComponent<Renderer>().enabled = false;
        }
        if (left){
            hasEyeLeft = false;

            eyeLeft.GetComponent<CakeEye>().isAttached = false;

            GameObject g = GameObject.Instantiate(eyeLeft.gameObject, eyeLeft.position, Quaternion.identity);
            g.GetComponent<Collider>().isTrigger = false;
            Rigidbody r = g.AddComponent(typeof(Rigidbody)) as Rigidbody;
            r.mass = 0.01f;
            
            eyeLeft.GetComponent<Renderer>().enabled = false;
        }
    }

    public void AttachEye (Transform eye, bool onRight){
        Debug.Log("AttachEye was called");
        Transform t = onRight? eyeRight : eyeLeft;

        Renderer r = t.GetComponent<Renderer>();
        CakeEye cakeEye = t.GetComponent<CakeEye>(); 

        if (r){
            if (cakeEye){
                r.enabled = true;
                cakeEye.isAttached = true;
                hasEyeRight = onRight? true : hasEyeRight;
                hasEyeLeft = !onRight? true : hasEyeLeft; 

                Destroy(eye.gameObject);

                health += maxHealth / 3f;
                if (health > (2f/3f) * maxHealth){
                    eyeDetach2_3 = false;
                }
                else if (health > (1f/3f) * maxHealth){
                    eyeDetach1_3 = false;
                }
                else Debug.LogWarning("An Eye deatch boolean wasn't turned false when '"+t.name+"' was attached");
            }
            else Debug.LogWarning("No CakeEye found on "+t.name);
        }
        else Debug.LogWarning("No renderer found on "+t.name);
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
