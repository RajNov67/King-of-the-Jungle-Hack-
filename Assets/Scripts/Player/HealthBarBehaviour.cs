using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    private Transform bar;
    private float maxHealth;


    private void Start()
    {
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(1f, 1f);
    }

    public void SetSize(float maxHealth, float health)
    {
        float scale = (health / maxHealth);
        bar.localScale = new Vector3(scale, 1f);
    }

 
}
