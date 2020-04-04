using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rig;
    public Transform normalBitePivot;
    public float biteForwardForce = 2f;
    public float biteDamage = 1f;
    public GameObject testOnject;
    public LayerMask layers;
    float mZCoord;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!IsGroundedBite()) {
                animator.ResetTrigger("GroundBite");
                animator.SetTrigger("Bite");
            }
            else {
                animator.ResetTrigger("Bite");
                animator.SetTrigger("GroundBite");
            }
            if (Mathf.Abs(rig.velocity.x) < 1 && Mathf.Abs(rig.velocity.z) < 1) rig.AddForce(transform.forward * biteForwardForce, ForceMode.Impulse);
        }
        else if (Input.GetMouseButtonDown(1)){
            Vector3 m = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("MouseWorldPos: " + w);
            //Destroy(GameObject.Instantiate(testOnject, w + Camera.main.transform.forward*10, Quaternion.identity), 1f);
            //animator.SetFloat("TongueX", Mathf.Lerp(-1f, 1f, m.x));
            //animator.SetFloat("TongueY", Mathf.Lerp(-1f, 1f, m.y));
            animator.SetTrigger("TongueSwing");
            Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition) + " - ("+ Mathf.Lerp(-1f, 1f, m.x)+" , "+Mathf.Lerp(-1f, 1f, m.y)+")");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 finalPos;
            if (Physics.Raycast(ray, out hit, 30, layers)) {
                finalPos = hit.point;
            }
            else {
                ray.origin = transform.position;
                finalPos = ray.GetPoint(10f);
            }
            //Destroy(GameObject.Instantiate(testOnject, finalPos, Quaternion.identity), 1f);

            mZCoord = Camera.main.WorldToScreenPoint(transform.position).z+5;
            Vector3 mOffset = transform.position - GetMouseWorldPos();
            finalPos = GetMouseWorldPos();

            Destroy(GameObject.Instantiate(testOnject, finalPos, Quaternion.identity), 1f);
            Vector3 dif = transform.position - finalPos;

            Quaternion changeInRotation = Quaternion.FromToRotation(transform.forward, dif);
            Vector3 euler = changeInRotation.eulerAngles;

            Vector3 finalXY = new Vector3(
                Mathf.Clamp(((euler.x - 180) / 40f), -1f, 1f),
                0,
                0
            );

            animator.SetFloat("TongueX", finalXY.x);
            animator.SetFloat("TongueY", finalXY.y);

            Debug.Log("Angle x: " + finalXY.x +"-"+ euler.x+ " , angle y: "+ finalXY.y);
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

    public void DealDamageTo(Health health){
        health.health -= biteDamage;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
