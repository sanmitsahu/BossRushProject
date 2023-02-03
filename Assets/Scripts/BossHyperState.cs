using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHyperState : MonoBehaviour
{
    // Reference to the Renderer component of the object
    private Renderer newRenderer;

    // The color to change the material to
    public Color bossColour = Color.red;

    // Use this for initialization
    void Start()
    {
        // Get the renderer component attached to the object
        newRenderer = GetComponent<Renderer>();

        // Set the initial color of the material
        newRenderer.material.color = bossColour;
    }

    // Update is called once per frame
    void Update()
    {
        // After three consecutive attacks, the boss will change to a yellow colour
        if (HitCounter.currentHits >= 3)
        {
            bossColour = Color.yellow;
        }
        else
        {
            bossColour = Color.red;
        }

        // Update the color of the material to the new color
        newRenderer.material.color = bossColour;
    }
}
