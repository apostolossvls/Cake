using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageOnCollisionStay : MonoBehaviour
{
    public float damage=1f;
    public float TickSeconds = 1f;
    public List<Transform> targets;
    List<float> tickTimer;
    public string[] tags;
    public string[] layers;
    public bool searchHierarchy = true;

    void Start()
    {
        targets = new List<Transform>{};
        tickTimer = new List<float>{};
    }

    void Update(){
        for (int i = 0; i < tickTimer.Count; i++)
        {
            tickTimer[i] += Time.deltaTime;
        }
        List<int> indexes = new List<int>{};
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i]){
                indexes.Add(OnArray(targets[i]));
            }
        }
        foreach (int i in indexes)
        {
            targets.RemoveAt(i);
            tickTimer.RemoveAt(i);
        }
    }

    void DealDamage(Transform t){
        PlayerHealth h = null;
        if (searchHierarchy) h = t.gameObject.GetComponentInParent<PlayerHealth>();
        else h = t.gameObject.GetComponent<PlayerHealth>();
        if (h){
            h.TakeDamage(damage, transform);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (this.enabled){
            if (TickSeconds != 0){
                if ((tag.Contains(other.transform.tag) || layers.Contains(LayerMask.LayerToName(other.gameObject.layer))) && OnArray(other.transform)==-1){
                    targets.Add(other.transform);
                    tickTimer.Add(TickSeconds);
                }
                for (int i = 0; i < tickTimer.Count; i++)
                {
                    if (tickTimer[i]>=TickSeconds){
                        tickTimer[i]=0;
                        DealDamage(targets[i]);
                    }
                }
            }
            else
            {
                DealDamage(other.transform);
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (this.enabled){
            if (OnArray(other.transform)!=-1){
                int index = OnArray(other.transform);
                targets.RemoveAt(index);
                tickTimer.RemoveAt(index);
            }
        }
    }
    

    int OnArray(Transform t){
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i]==t) return i;
        }
        return -1;
    }
}
