using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grappleToPoint;

    public LayerMask whatIsGrappleable;
    public Transform gunTip; // for drawing rope (draw rope from gun tip to where the grapple hit.
    public Transform playerCamera; // for Raycasting.  where are we raycasting from?
    public Transform player; // For Joint/ Rope - on Raycastm add a joint to the player objects components (through inspector)

    private float maxDistanceRC = 100f;

    private SpringJoint joint;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
       

        if (Input.GetMouseButtonDown(0)) // when holding left-click
        {
            StartGrapple();
        }

        else if (Input.GetMouseButtonUp(0)) // when release left-click
        {
            StopGrapple();
        }
    }

   void LateUpdate()
    {
        DrawRope();
    }
    private void StartGrapple()
    {
        RaycastHit hit;

       if (Physics.Raycast(playerCamera.position,playerCamera.forward, out hit, maxDistanceRC, whatIsGrappleable))
        {
            grappleToPoint = hit.point;

            joint = player.gameObject.AddComponent<SpringJoint>(); // add joint component to player in unity inspector
            joint.autoConfigureConnectedAnchor = false; // some joint settings visible at front-end
            joint.connectedAnchor = grappleToPoint; // same as above

            float playerDistanceFromGrappleToPoint = Vector3.Distance(player.position, grappleToPoint);

            // Distance between the grapple point and player
            // Spring Joint Settings below

            joint.maxDistance = playerDistanceFromGrappleToPoint * 0.8f;
            joint.minDistance = playerDistanceFromGrappleToPoint * 0.25f;

            joint.spring = 4.5f ;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2; // TODO - understand what exactly this is.

           
        }


    }


    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void DrawRope ()
    {

        if (!joint)         // If we havent grappled, dont draw line.
        {
            return;
        }

        else  // draw a rope from guntip to grapple point
        { 
            lr.SetPosition(0, gunTip.position); // TODO understand exactly what this does
            lr.SetPosition(1, grappleToPoint);
        }

    }

    public bool isGrappling()
    {
        return joint != null; // joint is present
    }

    public Vector3 GetGrappleToPoint()
    {
        return grappleToPoint;
    }
}
