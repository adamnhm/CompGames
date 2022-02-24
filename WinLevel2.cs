using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel2 : MonoBehaviour
{
    public GameObject[] trolls;
    public int trollDeathCountWin;

    private GameObject WinGame; //Win text

    // Start is called before the first frame update
    void Start()
    {
        WinGame = GameObject.FindGameObjectWithTag("NPCGUI");
        WinGame.SetActive(false);
        trollDeathCountWin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        trolls = GameObject.FindGameObjectsWithTag("Troll");
        foreach (GameObject troll in trolls)
        {
            if (troll.GetComponentInParent<HealthScript>().PriestDeathCount)
            {
                trollDeathCountWin++;
                troll.GetComponentInParent<HealthScript>().PriestDeathCount = false;

            }
        }

        if (trollDeathCountWin >= 5)
        {
            WinGame.SetActive(true);
        }
    }
}