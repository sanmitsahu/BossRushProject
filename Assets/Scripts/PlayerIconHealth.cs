



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconHealth : MonoBehaviour
{
    public GameObject IconPrefab;

    private float maxHealth;
    private float currentHealth;
    public List<Image> Icons;

    // Start is called before the first frame update
    void Start()
    {


        maxHealth = PlayerController.maximumHealth;
        currentHealth = PlayerController.health;
        UnityEngine.Debug.Log("pHealth");



        for (int i = 0; i < maxHealth; i++)
        {
            
            GameObject h = Instantiate(IconPrefab, transform);
            UnityEngine.Debug.Log(h);
            Icons.Add(h.GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerController.health;

        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                Icons[i].enabled = true;
            }
            else
            {
                Icons[i].enabled = false;
            }
        }
        //UnityEngine.Debug.Log($"Health Bar Value: {currentHealth} / {maxHealth} : {mySlider.value}");
    }
}

