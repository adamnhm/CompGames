using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK,
    IDLE
}

public class Goblin_Controller : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private Transform PlayerTarget;
    Animator EnemyAnim;
   // private Crowd_Control GetAgent 

    public GameObject attackPoint;
    public float move_Speed = 3.5f;
    public float attack_Dist = 1.5f;
    public float Run_Away_Dist = 12f;
    public float Chase_After_Attack_Dist = 1.5f;
    public float wait_b4_attack_time = 3f;
    private float attack_Timer;
 
    private EnemyState enemy_state;
 

    //Animation Variables
    bool EnemyWalk;


    // Start is called before the first frame update
    void Start()
    {

        navAgent = GetComponent<NavMeshAgent>();
        PlayerTarget = GameObject.FindGameObjectWithTag("Player").transform; // player position
        enemy_state = EnemyState.CHASE;
        attack_Timer = wait_b4_attack_time;
        if (navAgent.enabled == false)
        {
            navAgent.enabled = true;
        }
        EnemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     
        
        if (enemy_state == EnemyState.CHASE)
        {
            ChasePlayer();
        }

        if (enemy_state == EnemyState.ATTACK)
        {
            AttackPlayer();
        }

        if (enemy_state == EnemyState.IDLE)
        {
            EnemyIdle();
        }
    }

    void ChasePlayer()
    {
     
        navAgent.SetDestination(PlayerTarget.position);
        navAgent.speed = move_Speed;

        if(navAgent.velocity.sqrMagnitude == 0)
        {
            EnemyWalk = false;
            EnemyAnim.SetBool("walk", EnemyWalk);
        }
        else
        {
            EnemyWalk = true;
            EnemyAnim.SetBool("walk", EnemyWalk);
        }

        //set distance from player for enemy to attack
        if (Vector3.Distance(transform.position, PlayerTarget.position) <= attack_Dist) 
        {
            enemy_state = EnemyState.ATTACK;
        }

        if (Vector3.Distance(transform.position, PlayerTarget.position) >
            Run_Away_Dist)
        {
            enemy_state = EnemyState.IDLE;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero; // stop navAgent
        
        navAgent.isStopped = true;

        EnemyWalk = false; // stop walk animation
        EnemyAnim.SetBool("walk", EnemyWalk);

    
        attack_Timer = attack_Timer + Time.deltaTime;

        if(attack_Timer > wait_b4_attack_time)
        {
            EnemyAnim.SetTrigger("attack01"); // attack 
            attack_Timer = 0f;
        }
        if (Vector3.Distance(transform.position, PlayerTarget.position) >
            attack_Dist + Chase_After_Attack_Dist)
        {
            navAgent.isStopped = false;
            enemy_state = EnemyState.CHASE;
        }
    }

    void EnemyIdle()
    {
        if(Vector3.Distance(transform.position, PlayerTarget.position) >
            Run_Away_Dist)
        { 
         
                navAgent.velocity = Vector3.zero;
                navAgent.isStopped = true;

                EnemyWalk = false; // stop walk animation
                EnemyAnim.SetBool("walk", EnemyWalk);

        }
     
        else
        {
            navAgent.isStopped = false;
            enemy_state = EnemyState.CHASE;
        }
        
    }

    public void Activate_AttackPoint()
    {
        attackPoint.SetActive(true);
      
    }

    public void Deactivate_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
// Code adapted from https://www.youtube.com/watch?v=1wn5Ur1_vKg&ab_channel=freeCodeCamp.org