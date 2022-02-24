using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int sceneIndex;
    private GameObject playerObject;
  

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(playerObject);
   
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
