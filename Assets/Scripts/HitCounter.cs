using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCounter : MonoBehaviour
{
    // Reference to the Text Mesh Pro component
    private TextMeshPro textMesh;

    // The current number being displayed
    public static int currentHits;

    // Consecutive hits within the time frame
    public static int consecutiveHits;

    // Var to track the overall counter
    public int overall;

    // The time at which the number was last incremented
    private float lastIncrementTime;

    // The time interval between increments (in seconds)
    private readonly float incrementInterval = 0.25f;

    private void Awake()
    {
        // Get the Text Mesh Pro component attached to this GameObject
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        // Set the initial number to 0
        currentHits = 0;

        // Set the initial value of the text
        textMesh.SetText(currentHits.ToString());

        // Record the current time as the last increment time
        lastIncrementTime = Time.time;

        overall = Counter.overallCounter;
    }

    private void Update()
    {
        // Interval checking prevents currentHits from incrementing by more than one with each hit
        // Check if the increment interval has passed
        if (Time.time - lastIncrementTime >= incrementInterval)
        {
            if (Counter.currentNumber == 0)
            {
                currentHits = 0;
                textMesh.SetText(currentHits.ToString());
            }

            // Check if the hit variable is true
            if (EnemyBehavior.hit)
            {
                //UnityEngine.Debug.Log($"consecutive = {consecutiveHits}");

                // Increment the number by 1
                currentHits++;

                // Update the text display with the new number
                textMesh.SetText(currentHits.ToString());
            }

            //UnityEngine.Debug.Log($"counter = {Counter.currentNumber}");

            // Update the last increment time
            lastIncrementTime = Time.time;
        }
    }
}
