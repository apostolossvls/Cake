using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageOnTriggerStay : MonoBehaviour
{
    public float damage=1f;
    public float TicksPerSecond=1f;
    public List<Transform> targets;
    List<float> tickTimer;
    public string[] tags;
    public string[] layers;

    void Start()
    {
        targets = new List<Transform>{};
        tickTimer = new List<float>{};
    }

    void Update(){
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
        PlayerHealth h = t.gameObject.GetComponent<PlayerHealth>();
        if (h){
            h.OnDamage(damage);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (this.enabled){
            if (TicksPerSecond!=0){
                if ((tags.Contains(other.transform.tag) || layers.Contains(LayerMask.LayerToName(other.gameObject.layer))) && OnArray(other.transform)==-1){
                    targets.Add(other.transform);
                    tickTimer.Add(TicksPerSecond);
                }
                for (int i = 0; i < tickTimer.Count; i++)
                {
                    tickTimer[i] += Time.deltaTime;
                    if (tickTimer[i]>=TicksPerSecond){
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

    void OnTriggerExit(Collider other)
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
