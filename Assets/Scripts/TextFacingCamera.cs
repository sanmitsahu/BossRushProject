using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFacingCamera : MonoBehaviour
{
    // Reference to the Text Mesh Pro component
    private TextMeshPro textMesh;
    // Reference to the main camera
    private Camera mainCamera;

    private void Start()
    {
        // Get the Text Mesh Pro component attached to this GameObject
        textMesh = GetComponent<TextMeshPro>();
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
