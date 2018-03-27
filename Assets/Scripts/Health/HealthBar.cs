using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    

    public int MaxHealth = 100;

    public int currentHealth = 100;

    Slider healthBar;
    float maxWidth;
    float currentWidth;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentHealth > MaxHealth)
            currentHealth = MaxHealth;

        if (currentHealth < 0)
            currentHealth = 0;

        healthBar.value = (float)currentHealth / MaxHealth;
    }
}
