using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rig;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float moveJumpForce = 10f;
    Vector3 movement;
    public Transform GroundCheckPivot;
    public Transform cam;

    void Start(){
        Cursor.visible = false;
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
            Vector3 targetDirection = new Vector3(movement.x, 0f, movement.z);
            targetDirection = cam.transform.TransformDirection(targetDirection);
            targetDirection.y = 0.0f;
            //Debug.DrawRay(transform.position, forward.normalized*10f, Color.black);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
            if (IsGrounded()){
                rig.AddForce(transform.up * moveJumpForce, ForceMode.Impulse);
            }
            //else Debug.Log("On air");
            //rig.MovePosition(rig.position + new Vector3(forward.x * movement.x, 0, forward.z * movement.z) * moveSpeed * Time.fixedDeltaTime);
            rig.MovePosition(rig.position + targetDirection * moveSpeed * Time.fixedDeltaTime);
            Debug.DrawRay(transform.position, new Vector3(cam.forward.x, 0, cam.forward.z).normalized * 10f, Color.blue);
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
