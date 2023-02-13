using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacingCamera : MonoBehaviour
{
    // Reference to the Slider component
    private Slider mySlider;
    // Reference to the main camera
    private Camera mainCamera;

    private void Start()
    {
        // Get the Text Mesh Pro component attached to this GameObject
        mySlider = GetComponent<Slider>();
        // Get reference to the main camera in the scene
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        // Rotate the transform of the GameObject to always face the main camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
