using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle_Avoid : MonoBehaviour
{
    // public GameObject HitObject;
    private NavMeshAgent agent;

    public float SphereRadius;
    public float Max_Dist;
    public LayerMask layerMask;

    private Vector3 dir;
    private Vector3 origin;

    public float HITDIST;
    public float rotSpeed = 100f;
   

    public void Ostacle_Avoid1()
    {
        origin = this.transform.position;
        dir = this.transform.forward;
        RaycastHit hit;
        if(Physics.SphereCast(origin, SphereRadius, dir, out hit, Max_Dist, layerMask, QueryTriggerInteraction.UseGlobal))
        {
           // HitObject = hit.transform.gameObject;
            HITDIST = hit.distance;
            agent.isStopped = true;
            transform.Rotate(transform.up* Time.deltaTime* rotSpeed);
            agent.isStopped = false;
        }
        else
        {
             HITDIST = Max_Dist;
            //  HitObject = null;
        }
    }
}
    
//based on code https://www.youtube.com/watch?v=Nplcqwq_oJU&ab_channel=RyanZehm
