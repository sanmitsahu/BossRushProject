using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Slider mySlider;
    private float maxHealth;
    private float currentHealth;
    private float healthBarValue;

    // Start is called before the first frame update
    void Start()
    {
        mySlider = GetComponent<Slider>();

        maxHealth = PlayerController.maximumHealth;
        currentHealth = PlayerController.health;
        healthBarValue = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerController.health;

        if (currentHealth <= 0)
            healthBarValue = 0;
        else
            healthBarValue = currentHealth / maxHealth;

        mySlider.value = healthBarValue;

        //UnityEngine.Debug.Log($"Health Bar Value: {currentHealth} / {maxHealth} : {mySlider.value}");
    }
}
