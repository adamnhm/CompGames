using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel1 : MonoBehaviour
{
    public GameObject[] priests;
    public int PriestDeathCountWin;
  //  private HealthScript healthScript;

    private DialogueSystem dialogueSystem;

    // Start is called before the first frame update
    void Start()
    {
        PriestDeathCountWin = 0;
        GetComponentInParent<LevelTransition>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        priests = GameObject.FindGameObjectsWithTag("Priest");
        foreach (GameObject priest in priests)
        {
            if (priest.GetComponentInParent<HealthScript>().PriestDeathCount)
            {
                PriestDeathCountWin++;
                priest.GetComponentInParent<HealthScript>().PriestDeathCount = false;

            }
        }

        if (PriestDeathCountWin >= 4)
        {
            GetComponentInParent<LevelTransition>().enabled = true;
        }
    }
}
