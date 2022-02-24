using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool PriestDeathCount;

    public float health;
    public float maxHealth;
    public bool IsPlayer;
    public bool IsEnemy;
    public bool IsGuard;
    public bool IsPriest;
    bool Die;
    Animator Health_Animator;
    

    private ThirdPersonCharacter m_Character;

    public GameObject[] enemies;

    public GameObject HealthBar;

    private DialogueSystem dialogueSystem;
    

    [HideInInspector]
    public bool BlockActivated; 

    private void Start()
    {
        PriestDeathCount = false;
        dialogueSystem = FindObjectOfType<DialogueSystem>();

        health = maxHealth;


        agent = GetComponent<NavMeshAgent>();
        Health_Animator = gameObject.GetComponent<Animator>();
        m_Character = GetComponent<ThirdPersonCharacter>();
      
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in allChildren)
        {
            childObjects.Add(child.gameObject); // list all children of game object
        }
        foreach (GameObject child1 in childObjects)
            if (child1.CompareTag("HealthBar"))
            {
                HealthBar = child1; //find health bar
            }
        if (!IsPlayer)
        {
            HealthBar.SetActive(false); // deactivate non player health bar
        }

        if (IsGuard || IsPriest)
        {
            this.gameObject.GetComponent<Goblin_Controller>().enabled = false; // deactivate guard attacks on start
        }

        enemies = GameObject.FindGameObjectsWithTag("Troll");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Goblin_Controller>().enabled = true; // enable troll goblin controller
        }
    }

    void Update()
    {
        if (health <= 0 && !IsPlayer) 
        {
            agent.enabled = false; // prevents navMeshAgent from re enabling
        }
        if (health <= 0 && IsPlayer)
        {
            dialogueSystem.DeathLoad();
        }
        
    }

    // Activated by collision on Attack Point
    // Called by Attackdamage.cs on Attack Point
    public void ApplyDamage(float damage) 
    {
        if (IsPlayer)
        {
            BlockActivated = m_Character.m_Block; 
            //retrives block bool from Thirdpersoncharacter -> exits function
            if (BlockActivated)
            {
                return;
            }
        }

        health -= damage;

        if (!IsPlayer)
        {
            HealthBar.SetActive(true); //when enemy damaged health bar appears
            if (IsGuard)
            {
                enemies = GameObject.FindGameObjectsWithTag("Guard");
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Goblin_Controller>().enabled = true; // activate guard attacks
                    enemy.GetComponentInParent<Crowd_Control>().enabled = false;
                    if(agent.enabled == true)
                    {
                        agent.enabled = false;
                    }
                }
            }

            if (IsPriest)
            {
                enemies = GameObject.FindGameObjectsWithTag("Priest");
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Goblin_Controller>().enabled = true; // activate priest attacks
                    enemy.GetComponentInParent<Crowd_Control>().enabled = false;
                    if (agent.enabled == true)
                    {
                        agent.enabled = false;
                    }
                }
            }
        }

                

        if(health <= 0) // character dies
        {
            HealthBar.SetActive(false);
            Die = true;
            Health_Animator.SetBool("Die", Die);
            if (IsPlayer)
            {
                Camera.main.transform.SetParent(null);
                GetComponent<ThirdPersonUserControl>().enabled = false; //disable player controls

                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                
                foreach(GameObject enemy in enemies)
                {
                    enemy.GetComponent<Goblin_Controller>().enabled = false; // stop enemy attacking after death   
                }
                enemies = GameObject.FindGameObjectsWithTag("Guard");

                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Goblin_Controller>().enabled = false; // stop guards attacking after death   
                }
                enemies = GameObject.FindGameObjectsWithTag("Priest");

                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Goblin_Controller>().enabled = false; // stop priests attacking after death   
                }
                enemies = GameObject.FindGameObjectsWithTag("Troll");

                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Goblin_Controller>().enabled = false; // stop Trolls attacking after death   
                }

            }
            else if (IsEnemy)
            {
                GetComponentInParent<Goblin_Controller>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;     
            }
            else if (IsPriest)
            {
                GetComponentInParent<Goblin_Controller>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                if(health > -34) // Prevents player attacks after death
                {
                    PriestDeathCount = true;
                } 
                
            }
            else
            {
                GetComponentInParent<Crowd_Control>().enabled = false;
                
                agent.enabled = false;
                
            }
        }
        else { 
        
            Die = false;
            Health_Animator.SetBool("Die", Die);
        }
    }

    
        
    
}
