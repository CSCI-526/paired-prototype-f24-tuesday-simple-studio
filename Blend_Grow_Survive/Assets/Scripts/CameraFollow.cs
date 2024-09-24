using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;  // The target (player) to follow
    public Vector3 offset;    // Optional offset for the camera

    public float zoomFactor = 1.5f;  // Adjusts how zoom responds to player's size
    public float minZoom = 5.0f;     // Minimum zoom level
    public float maxZoom = 20.0f;    // Maximum zoom level

    private Camera cam;  // Reference to the camera component

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;  // Get the main camera component
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            // Follow the target's position with an optional offset
            transform.position = Target.position + offset;

            // Adjust the camera's orthographic size based on the player's local scale
            float targetZoom = Target.localScale.x * zoomFactor;
            cam.orthographicSize = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
    }
}