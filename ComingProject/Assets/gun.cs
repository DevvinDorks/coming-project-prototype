using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

    

    public GameObject rayOriginObject;
    public GameObject lineOriginObject;

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

        //Maybe raycast from camera to center instead, but then draw the line as gun to hitpoint

        RaycastHit hit;
        Vector3 endPosition;

        Vector3 rayOrigin = rayOriginObject.transform.position;
        Vector3 lineOrigin = lineOriginObject.transform.position;
        Vector3 rayDirection = rayOriginObject.transform.forward;

        //Check if laser is hitting anything
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {

            //Check if the hit object is on the Reflective layer
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reflective"))
            {
                //Reflect
                lineRenderer.positionCount = 3;
                lineRenderer.SetPosition(0, lineOrigin);
                lineRenderer.SetPosition(1, hit.point);

                Vector3 reflectDirection = Vector3.Reflect(rayDirection, hit.normal);

                lineRenderer.SetPosition(2, reflectDirection * laserMaxLength);
            }
            else
            {
                //End beam at hit object
                endPosition = hit.point;

                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, lineOrigin);
                lineRenderer.SetPosition(1, endPosition);
            }

        }
        else
        {
            //End beam at max laser distance
            endPosition = rayDirection * laserMaxLength;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, lineOrigin);
            lineRenderer.SetPosition(1, endPosition);

            
        }



    }



}
