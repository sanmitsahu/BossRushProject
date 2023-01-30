using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    // Reference to the Text Mesh Pro component
    private TextMeshPro textMesh;

    // The overall counter
    public static int overallCounter;

    // The current number being displayed
    public static int currentNumber;

    // The time at which the number was last incremented
    private float lastIncrementTime;

    // The time interval between increments (in seconds)
    private readonly float incrementInterval = 1f;

    // Reset the value when it reaches 5
    private int resetValue = 5;

    private void Awake()
    {
        // Get the Text Mesh Pro component attached to this GameObject
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        // Set the initial number to 0 and update text
        overallCounter = 0;
        currentNumber = 0;
        textMesh.SetText(currentNumber.ToString());

        // Record the current time as the last increment time
        lastIncrementTime = Time.time;
    }

    private void Update()
    {
        // Check if the increment interval has passed
        if (Time.time - lastIncrementTime >= incrementInterval)
        {
            if (currentNumber == 0)
            {
                overallCounter++;
            }

            //UnityEngine.Debug.Log($"Overall = {overallCounter}");

            // Increment the number by 1
            currentNumber = (int)(Time.time % resetValue);

            // Update the last increment time
            lastIncrementTime = Time.time;

            // Update the text display with the new number
            textMesh.SetText(currentNumber.ToString());
        }
    }
}
