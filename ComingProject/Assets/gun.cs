using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

    

    public GameObject rayOrigin;

    public LineRenderer lineRenderer;
    public float laserWidth;
    public float laserMaxLength;

    bool gunFiring;

    void Start () {

        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;

        gunFiring = false;

    }
	
	
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            gunFiring = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            gunFiring = false;
        }


        if (gunFiring == true)
        {
            FireLaser();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }

    void FireLaser()
    {
        RaycastHit hit;
        Vector3 endPosition;

        //Check if raycast hits any object
        if (Physics.Raycast(rayOrigin.transform.position, rayOrigin.transform.forward, out hit, Mathf.Infinity))
        {

            //Check if the hit object is on the Reflective layer
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reflective"))
            {
                //Debug.DrawRay(rayOrigin.transform.position, rayOrigin.transform.forward * hit.distance, Color.yellow, 100.0f);
                //Debug.Log("Hit a Reflective object");
            }
            else
            {
                //Debug.Log("Hit an object");
            }

            endPosition = hit.point;
        }
        else
        {
            //Debug.DrawRay(rayOrigin.transform.position, rayOrigin.transform.forward * 1000, Color.red, 100.0f);

            //Render the line and have it end after the max distance
            endPosition = rayOrigin.transform.forward * laserMaxLength;
        }

        lineRenderer.SetPosition(0, rayOrigin.transform.position);
        lineRenderer.SetPosition(1, endPosition);

        

    }



}
