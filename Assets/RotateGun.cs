using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    // This script rotates our gun to look at our grappling point

    public GrapplingGun grappling;
    private Quaternion desiredRotation;
    private float rotationSpeed = 3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!grappling.isGrappling())
        {
            desiredRotation = transform.parent.rotation;
        }

        else
        {
            desiredRotation = Quaternion.LookRotation(grappling.GetGrappleToPoint() - transform.position);

        }

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
