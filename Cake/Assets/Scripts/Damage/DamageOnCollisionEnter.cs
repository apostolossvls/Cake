﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageOnCollisionEnter : MonoBehaviour
{
    public float damage=1f;
    public string[] tags;
    public string[] layers;
    public bool searchHierarchy = true;

    void DealDamage(Transform t){
        PlayerHealth h = null;
        if (searchHierarchy) h = t.gameObject.GetComponentInParent<PlayerHealth>();
        else h = t.gameObject.GetComponent<PlayerHealth>();
        if (h){
            h.TakeDamage(damage, transform);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (tags.Contains(other.transform.tag) || layers.Contains(LayerMask.LayerToName(other.gameObject.layer))) {
            DealDamage(other.transform);
        }
    }
}
