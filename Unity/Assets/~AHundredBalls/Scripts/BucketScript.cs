﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketScript : MonoBehaviour
{


    public float movementSpeed = 10.0f;
    private Rigidbody2D rigid2D;
    private Renderer[] renderers;


    // Use this for initialization
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePosition();
        HandleBoundary();
    }


    // handles bucket position
    void HandlePosition()
    {
        rigid2D.velocity = Vector3.left * movementSpeed;
    }

    // handles teh screen boundraies for game object
    void HandleBoundary()
    {
        Vector3 transformPos = transform.position;
        // get the viewport position of where the bucket is
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transformPos);
        // is the GameObject visible from the camera and on the left hand side 
    }


}
