﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{  
    [SerializeField] private Transform PlayerBodyTransform; // For Moving Parent Body object (Rigidbody Controller)
    [SerializeField] private float mouseSensitivity = 100f;

    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;


    void Start()
    {      
        Cursor.lockState = CursorLockMode.Locked;     
    }

    // Update is called once per frame
    void Update()
    {      
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation= Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBodyTransform.Rotate(Vector3.up * mouseX); // Moves the player body left right (Parent Object)
     
    }

   

    
}
