using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBAR : MonoBehaviour
{
    private HealthScript health_script;
    [SerializeField]
    private Image foregroundImage;

    // Start is called before the first frame update
    void Start()
    {
        health_script = GetComponentInParent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        foregroundImage.fillAmount = health_script.health / health_script.maxHealth;
        
    }
}