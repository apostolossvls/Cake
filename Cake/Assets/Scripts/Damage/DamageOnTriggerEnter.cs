﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageOnTriggerEnter : MonoBehaviour
{
    public float damage=1f;
    public string[] tags;
    public string[] layers;

    void DealDamage(Transform t){
        PlayerHealth h = t.gameObject.GetComponent<PlayerHealth>();
        if (h){
            h.TakeDamage(damage, transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((tags.Contains(other.transform.tag) || layers.Contains(LayerMask.LayerToName(other.gameObject.layer)))){
            DealDamage(other.transform);
        }
    }
}
