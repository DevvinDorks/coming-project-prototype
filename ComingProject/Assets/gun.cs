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
            //FireLaser();
            DrawLaser(FireLaserRecursive(rayOriginObject.transform.position, rayOriginObject.transform.forward, 0, new List<Vector3>()));
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }

    List<Vector3> FireLaserRecursive(Vector3 rayOrigin, Vector3 rayDirection, int recursions, List<Vector3> linePoints)
    {
        //Shoot beam, check if hit object or not
        //If hit reflective object, find reflection vector
        //Shoot new raycast along reflection vector
        //Count recursion
        //Recursive call

        RaycastHit hit;
        Vector3 endPosition;
        Vector3 lineOrigin = lineOriginObject.transform.position;


        //Use lineOrigin to draw the line but rayOrigin to calculate the raycast
        linePoints.Add(lineOrigin);
        linePoints.RemoveRange(recursions + 1, linePoints.Count - recursions - 1);


        //Shoot beam, check if hit object or not
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {

            linePoints.Add(hit.point);

            //Check if the hit object is on the Reflective layer
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reflective"))
            {
                //Find reflection vector
                Vector3 reflectDirection = Vector3.Reflect(rayDirection, hit.normal);
                recursions++;
                

                //For now just a simple break if an infinite loop of reflections occurs
                if(recursions > 20)
                {
                    return new List<Vector3>();
                }

                Vector3 reflectPoint = reflectDirection * laserMaxLength;
                linePoints.Add(reflectPoint);


                linePoints = FireLaserRecursive(hit.point, reflectDirection, recursions, linePoints);
                return linePoints;


            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("LaserActivated"))
            {
                LaserActivatedItem item = hit.transform.gameObject.GetComponent<LaserActivatedItem>();
                item.HitByLaser();

                return linePoints;
            }
            else
            {
                return linePoints;

            }

        }
        else
        {
            //End beam at max laser distance
            endPosition = rayDirection * laserMaxLength;

            linePoints.Add(endPosition);
            return linePoints;



        }

    }

    void DrawLaser(List<Vector3> linePoints)
    {
        lineRenderer.positionCount = linePoints.Count;

        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }

    }

    void FireLaser()
    {


        RaycastHit hit;
        Vector3 endPosition;
        
        Vector3 rayOrigin = rayOriginObject.transform.position;
        Vector3 rayDirection = rayOriginObject.transform.forward;

        Vector3 lineOrigin = lineOriginObject.transform.position;

        //Check if laser is hitting anything
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {

            //Check if the hit object is on the Reflective layer
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reflective"))
            {
                //Find reflection vector
                Vector3 reflectDirection = Vector3.Reflect(rayDirection, hit.normal);



                //Reflect
                lineRenderer.positionCount = 3;
                lineRenderer.SetPosition(0, lineOrigin);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.SetPosition(2, reflectDirection * laserMaxLength);

            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("LaserActivated"))
            {
                LaserActivatedItem item = hit.transform.gameObject.GetComponent<LaserActivatedItem>();
                item.HitByLaser();
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
