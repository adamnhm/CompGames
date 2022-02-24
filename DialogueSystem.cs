using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText; // this text is in chat background object

    private GameObject dialogueGUI; //NPCGUI
    public Transform dialogueBoxGUI; // DialogueGUI

    public float DisplayTime;

    public string Names;
    public string[] dialogueLines; // sentences array

    public bool dialogueActive; // is dialogue active?
    public bool outOfRange;

    public int dialogueLength; // No. sentences
    public int currentDialogueIndex;

    GameObject gameOver;
    public float Loading;
    public float LoadMax;
    public int sceneIndex;


    void Start()
    {
        dialogueActive = false; // is dialogue active?
        outOfRange = true;
        dialogueGUI = GameObject.FindGameObjectWithTag("NPCGUI");
       
        dialogueText.text = "";
        dialogueGUI.SetActive(false); // NPCGUI == false

        gameOver = GameObject.Find("GAMEOVERObject");
        gameOver.SetActive(false);
        LoadMax = 5f;
        Loading = 0f;
        DisplayTime = 5f;

    }

    public void Update()
    {
        if(outOfRange == true)
        {
            dialogueGUI.SetActive(false);
        }
    }
    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true); // NPCGUI
        if (dialogueActive == true)
        {
            dialogueGUI.SetActive(false); // if dialogue active NPCGUI off
        }
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true); // activates dialogue GUI
        dialogueActive = true;
        nameText.text = Names;
        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        
        if (outOfRange == false)
        {
            dialogueLength = dialogueLines.Length; // No. sentences
            currentDialogueIndex = 0;
            
            while (currentDialogueIndex < dialogueLength)
            {
                dialogueText.text = dialogueLines[currentDialogueIndex];
                currentDialogueIndex++;

                yield return new WaitForSeconds(DisplayTime);  
            }

            dialogueActive = false;
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
        else
        {
            OutOfRange();
        }
    }

    public void OutOfRange() // player walks away
    {
        outOfRange = true;
        dialogueActive = false;
        StopAllCoroutines();
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
        
    }

    public void DeathLoad()
    {
        sceneIndex = 0;
        Loading += Time.deltaTime;

        gameOver.SetActive(true);
        if (LoadMax > Loading)
        {
            SceneManager.LoadScene(sceneIndex);
        }

    }
}
// Based on code from: https://www.youtube.com/watch?v=p4a_OYmk1uU&ab_channel=TheSenpaiCode