using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserActivatedItem : MonoBehaviour {

	// Use this for initialization
	void Start () {

        gameObject.layer = LayerMask.NameToLayer("LaserActivated");


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitByLaser()
    {
        Debug.Log("You hit a laser activated object!");
    }
}
