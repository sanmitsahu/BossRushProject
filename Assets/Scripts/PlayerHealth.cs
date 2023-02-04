using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;  
    // Start is called before the first frame update
    void Start()
    {
        curHealth= maxHealth;
    }

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
