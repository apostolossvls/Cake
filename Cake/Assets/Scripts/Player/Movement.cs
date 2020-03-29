﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rig;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForce = 5f;
    public float moveJumpForce = 10f;
    Vector3 movement;
    public Transform groundCheckPivot;
    public Transform runningGroundCheckPivot;
    public Transform cam;
    public Animator animator;
    //jump
    public bool runningJump;
    public bool jumpPressed;
    bool onJump;
    bool onJumpUpwards;
    //roll
    bool rollPressed;
    public float rollForce = 10f;

    void Start(){
        Cursor.visible = false;
        if (rig==null) rig = GetComponentInChildren<Rigidbody>(); 

        onJump = false;
        jumpPressed = false;
        runningJump = false;
        onJumpUpwards = false;
        rollPressed = false;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        if (movement.x != 0 || movement.z !=0){

        }

        jumpPressed = Input.GetButton("Jump");

        rollPressed = Input.GetButtonDown("Roll");
    }

    void FixedUpdate(){
        //JUMP
        if (jumpPressed){
            if (IsGrounded() || (runningJump && IsGroundedRunning() && !onJumpUpwards)){
                jumpPressed = false;
                onJump = true;
                onJumpUpwards = true;
                rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }

        //ROLL
        if (rollPressed){
            rollPressed = false;
            Roll();
        }

        //position
        if (movement != Vector3.zero)
        {
            Vector3 targetDirection = new Vector3(movement.x, 0f, movement.z);
            targetDirection = cam.transform.TransformDirection(targetDirection);
            targetDirection.y = 0.0f;
            //Debug.DrawRay(transform.position, forward.normalized*10f, Color.black);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
            if (IsGrounded()){
                rig.AddForce(transform.up * moveJumpForce, ForceMode.Impulse);
                runningJump = true;
            }
            //else Debug.Log("On air");
            //rig.MovePosition(rig.position + new Vector3(forward.x * movement.x, 0, forward.z * movement.z) * moveSpeed * Time.fixedDeltaTime);
            rig.MovePosition(rig.position + targetDirection * moveSpeed * Time.fixedDeltaTime);
            Debug.DrawRay(transform.position, new Vector3(cam.forward.x, 0, cam.forward.z).normalized * 10f, Color.blue);
        }

        //checks
        if (!IsGroundedRunning()){
            if (onJump && onJumpUpwards) {
                onJumpUpwards = false;
            }
        }
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y-distToGround, transform.position.z), -transform.up, Color.red);
    }

    void Roll(){
        if (movement.x != 0){
            if (movement.x < 0){
                animator.SetTrigger("RollLeft");
                animator.ResetTrigger("RollRight");
            }
            else {
                animator.ResetTrigger("RollRight");
                animator.SetTrigger("RollLeft");
            }
            rig.AddForce((new Vector3(rig.transform.right.x * Mathf.Sign(movement.x), rig.transform.right.y, rig.transform.right.z) + rig.transform.up / 4) * rollForce, ForceMode.Impulse);
        }
    }

    public bool IsGrounded() {
        if (runningJump) runningJump = false;
        if (onJump) onJump = false;
        if (onJumpUpwards) onJumpUpwards = false;
        if (rig.velocity.y==0) return true;
        //else return Physics.Raycast(new Vector3(transform.position.x, transform.position.y-distToGround, transform.position.z), Vector3.down, 0.2f);
        else {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(groundCheckPivot.position, -Vector3.up, 0.1f);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.transform.root!=transform){
                    return true;
                }
            }
            return false;
        }
    }

    public bool IsGroundedRunning() {
        if (runningJump) runningJump = false;
        if (rig.velocity.y==0) return true;
        //else return Physics.Raycast(new Vector3(transform.position.x, transform.position.y-distToGround, transform.position.z), Vector3.down, 0.2f);
        else {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(runningGroundCheckPivot.position, -Vector3.up, 0.5f);
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
