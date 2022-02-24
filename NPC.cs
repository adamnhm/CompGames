using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class NPC : MonoBehaviour
{
    private NavMeshAgent navAgent;
    Animator EnemyAnim;
    bool EnemyWalk;

    public Transform ChatBackGround;
    private Transform NPCCharacter;

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        NPCCharacter = this.gameObject.transform;
        navAgent = GetComponent<NavMeshAgent>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        EnemyAnim = GetComponent<Animator>();
    }

    void Update() // updates position box would appear in
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 175;
        ChatBackGround.position = Pos;
    }

    public void OnTriggerStay(Collider other) // called once per physics update for every Collider other that is touching the trigger.
    {
        if(other.gameObject.tag == "Player") // prevent other NPCs from triggering colliders
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.EnterRangeOfNPC(); 
            
            if(this.gameObject.tag == "Priest" || this.gameObject.tag == "Guard")
            {
                this.gameObject.GetComponent<Crowd_Control>().enabled = false;

                if (this.gameObject.GetComponent<Goblin_Controller>().enabled == false)
                {
                    navAgent.velocity = Vector3.zero; // stop navAgent 
                    navAgent.isStopped = true;

                    EnemyWalk = false; // stop walk animation
                    EnemyAnim.SetBool("walk", EnemyWalk);    
                }
               
            }
            

                                // handles NPC GUI
            if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
            {
                dialogueSystem.Names = Name;
                dialogueSystem.dialogueLines = sentences;
                dialogueSystem.NPCName();
            }
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        
        this.gameObject.GetComponent<NPC>().enabled = false;

        if (other.gameObject.tag == "Player") // prevent other NPCs from triggering colliders
        {
            dialogueSystem.OutOfRange();
            if (this.gameObject.tag == "Priest" || this.gameObject.tag == "Guard")
            {
                navAgent.isStopped = false;
                if (this.gameObject.GetComponent<Goblin_Controller>().enabled == false)
                {
                    
                    this.gameObject.GetComponent<Crowd_Control>().enabled = true;
                }
                
            }
        }

    }
}
//based on code from: https://www.youtube.com/watch?v=p4a_OYmk1uU&ab_channel=TheSenpaiCode