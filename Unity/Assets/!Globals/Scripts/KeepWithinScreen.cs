using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))] // Force renderer component to be attached
public class KeepWithinScreen : MonoBehaviour
{
    // Rendere attached to the object
    private Renderer rend;
    // Camera container (variable)
    private Camera cam;
    // Camera Bounds structure
    private Bounds camBounds;
    private float camWidth, camHeight;

    // Use this for initialization
    void Start()
    {
        // Set camera variable to main camera
        cam = Camera.main;
        // Get the Renderer component attached to this object
        rend = GetComponent<Renderer>();
    }

    // Updates the camBounds variable with the camera values
    void UpdateCamBounds()
    {
        // Calculate camera bounds
        // height = 2 * orthographic size
        camHeight = 2f * cam.orthographicSize;
        // width = height * aspect
        camWidth = camHeight * cam.aspect;
        camBounds = new Bounds(cam.transform.position, new Vector3(camWidth, camHeight));
    }

    Vector3 CheckBounds()
    {
        Vector3 pos = transform.position;
        Vector3 size = rend.bounds.size;
        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;
        float halfCamWidth = camWidth * 0.5f;
        float halfCamHeight = camHeight * 0.5f;
        // Check left
        if (pos.x - halfWidth < camBounds.min.x)
        {
            pos.x = camBounds.max.x - halfWidth;
        }
        // Check right
        if (pos.x + halfWidth > camBounds.max.x)
        {
            pos.x = camBounds.min.x + halfWidth;
        }
        // Check down
        if (pos.y - halfHeight < camBounds.min.y)
        {
            pos.y = camBounds.max.y - halfHeight;
        }
        // Check up
        if (pos.y + halfHeight > camBounds.max.y)
        {
            pos.y = camBounds.min.y + halfHeight;
        }
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the camera bounds
        UpdateCamBounds();
        // Set the position after checking the bounds
        transform.position = CheckBounds();
    }
}

