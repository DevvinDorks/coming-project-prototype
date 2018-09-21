using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserActivatedItem : MonoBehaviour {

    public Material offMaterial;
    public Material onMaterial;

    public GameObject connectedObject;

    bool isOn;


	void Start () {

        gameObject.layer = LayerMask.NameToLayer("LaserActivated");
        isOn = false;
        
    }
	
	void Update () {
		
	}

    public void HitByLaser()
    {
        
        if(connectedObject != null && isOn == false)
        {
            isOn = true;
            GetComponent<Renderer>().material = onMaterial;
            connectedObject.GetComponent<Activatable>().AddActivation();
        }
        
    }


}
