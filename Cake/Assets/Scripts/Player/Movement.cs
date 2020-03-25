using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rig;
    public float moveSpeed = 5f;
    public float moveJumpForce = 10f;
    Vector3 movement;
    public Transform GroundCheckPivot;

    void Start(){
        if (rig==null) rig = GetComponentInChildren<Rigidbody>(); 
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        if (movement != Vector3.zero)
        {
            if (IsGrounded()){
                rig.AddForce(transform.up * moveJumpForce, ForceMode.Impulse);
            }
            else Debug.Log("On air");
            rig.MovePosition(rig.position +  movement * moveSpeed * Time.fixedDeltaTime);

        }
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y-distToGround, transform.position.z), -transform.up, Color.red);
    }

    public bool IsGrounded() {
        if (rig.velocity.y==0) return true;
        //else return Physics.Raycast(new Vector3(transform.position.x, transform.position.y-distToGround, transform.position.z), Vector3.down, 0.2f);
        else {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(GroundCheckPivot.position, -Vector3.up, 0.1f);
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
