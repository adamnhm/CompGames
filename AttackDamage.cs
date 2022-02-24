using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);
        if(hits.Length > 0) // A game object was hit 
        {
            hits[0].GetComponent<HealthScript>().ApplyDamage(damage); // calls damage func from health script
         
            gameObject.SetActive(false); // prevents multihit every Update

        }
    }
}
//Based on code from https://www.youtube.com/watch?v=1wn5Ur1_vKg&ab_channel=freeCodeCamp.org