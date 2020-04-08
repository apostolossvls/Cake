using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeCake : MonoBehaviour
{
    public float powerNeeded = 10;
    public float unitsPerPress = 1;
    public float power;
    public bool attached = true;
    public GameObject Player;
    public GameObject Cake100;
    public GameObject cake90;
    public GameObject particles;
    public Animator animator;

    void Start()
    {
        power = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attached) return;
        bool pressed = false;
        if (power > 0) power -= Time.deltaTime;
        if (power < 0) power = 0;
        pressed = Input.GetButtonDown("Horizontal")? true : pressed;
        pressed = Input.GetButtonDown("Vertical") ? true : pressed;
        pressed = Input.GetButtonDown("Jump") ? true : pressed;
        if (pressed)
        {
            Stragle();
        }
        if (power >= powerNeeded)
        {
            Detach();
        }
    }

    void Stragle()
    {
        power += unitsPerPress;
        animator.SetFloat("StragleRandom", Random.Range(0f, 1f));
        animator.SetTrigger("Stragle");
    }

    void Detach()
    {
        attached = false;
        Cake100.SetActive(false);
        cake90.SetActive(true);
        particles.SetActive(true);
        Player.transform.SetParent(null);
        Player.SetActive(true);
    } 
}
