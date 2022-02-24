using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class Crowd_Control : MonoBehaviour
{
    private NavMeshAgent agent;
    GameObject[] goalLocs;
    Animator Anim;
    private int SpecGoal;
    private bool isWalking = true;
    private float sm;
    private bool CoRoot;


    public float SphereRadius;
    public float Max_Dist;
    public LayerMask layerMask;

    private Vector3 dir;
    private Vector3 origin;

    public float HITDIST;
    public float rotSpeed = 100f;

    bool Nav;
    public bool IsNPC;
    public bool IsGuard;

    // Start is called before the first frame update
    void Start()
    {
        if (IsNPC)
        {
            goalLocs = GameObject.FindGameObjectsWithTag("goal");
        }

        if (IsGuard)
        {
            goalLocs = GameObject.FindGameObjectsWithTag("Guardgoal");
        }

        agent = GetComponent<NavMeshAgent>();

        SpecGoal = Random.Range(0, goalLocs.Length);
        agent.SetDestination(goalLocs[SpecGoal].transform.position);

        Anim = GetComponent<Animator>();
        Anim.SetBool("walk",isWalking);
        Anim.SetFloat("Woffset", Random.Range(0, 1));
        SetSpeed();
        CoRoot = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (CoRoot == false)
            {
                StartCoroutine(Pause());
            }

        if(Vector3.Distance(transform.position, goalLocs[SpecGoal].transform.position) < 1){
            SpecGoal = Random.Range(0, goalLocs.Length);
            agent.SetDestination(goalLocs[SpecGoal].transform.position);
            SetSpeed(); 
        }

        Ostacle_Avoid1();
    }

    void SetSpeed()
    {
        sm = Random.Range(0.3f, 1.2f);
        Anim.SetFloat("SpeedMult", sm);
        agent.speed *= sm;
    }

    IEnumerator Pause()
    {
        CoRoot = true;
        yield return new WaitForSeconds(Random.Range(2, 6));
        agent.velocity = Vector3.zero;
        isWalking = false;
        Anim.SetBool("walk", isWalking);
        agent.enabled = false;
        Nav = false;
      
        yield return new WaitForSeconds(Random.Range(2, 4));
        agent.enabled = true;
        Nav = true;
        SpecGoal = Random.Range(0, goalLocs.Length);
        agent.SetDestination(goalLocs[SpecGoal].transform.position);
        isWalking = true;
        Anim.SetBool("walk", isWalking);
        CoRoot = false;
    }

    void Ostacle_Avoid1()
    {
        origin = this.transform.position;
        dir = this.transform.forward;
        RaycastHit hit;
        if (Nav)
        {
                if (Physics.SphereCast(origin, SphereRadius, dir, out hit, Max_Dist, layerMask, QueryTriggerInteraction.UseGlobal))
                {

                    // HitObject = hit.transform.gameObject;
                    HITDIST = hit.distance;             
                    agent.ResetPath();

                    agent.velocity = Vector3.zero;
                    isWalking = false;
                    Anim.SetBool("walk", isWalking);
                    agent.enabled = false;

                    transform.Rotate(transform.up * Time.deltaTime * rotSpeed);

                    agent.enabled = true;  
                    isWalking = true;
                    Anim.SetBool("walk", isWalking);
                    agent.speed *= sm;

            }
                else
                {
                   // agent.isStopped = false;
                    HITDIST = Max_Dist;
                    agent.SetDestination(goalLocs[SpecGoal].transform.position);
                }
        }
        
    }



}
