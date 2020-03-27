using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rig;
    public Transform normalBitePivot;
    public float biteForwardForce = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (!IsGroundedBite()){
                animator.ResetTrigger("GroundBite");
                animator.SetTrigger("Bite");
            }
            else {
                animator.ResetTrigger("Bite");
                animator.SetTrigger("GroundBite");
            }
            if (Mathf.Abs(rig.velocity.x) < 1) rig.AddForce(transform.forward * biteForwardForce, ForceMode.Impulse);
        }
    }

    public bool IsGroundedBite() {
        if (rig.velocity.y==0) return true;
        //else return Physics.Raycast(new Vector3(transform.position.x, transform.position.y-distToGround, transform.position.z), Vector3.down, 0.2f);
        else {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(normalBitePivot.position, -Vector3.up, 1.5f);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.transform.root!=transform){
                    return true;
                }
            }
            return false;
        }
    }
}
