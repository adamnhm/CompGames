using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCorrect : MonoBehaviour
{
    private Transform SpawnTransform;
    private GameObject playerObject;

   

    // Start is called before the first frame update
    void Start()
    {
        SpawnTransform = this.gameObject.transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerObject.transform.position = SpawnTransform.position;
        playerObject.transform.rotation = SpawnTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
