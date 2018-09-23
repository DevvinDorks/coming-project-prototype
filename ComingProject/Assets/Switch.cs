using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public enum SwitchType { ActivatedByLaser, ActivatedByKeyPress};

    [SerializeField] public SwitchType switchType;
    [SerializeField] bool canBeSwitchedOff;
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;

    [SerializeField] List<GameObject> connectedObjects;

    //bool hitByLaser = false;
    bool isOn = false;


    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Switch");
    }

    void Update()
    {

    }

    public void Activate()
    {

        if (canBeSwitchedOff && isOn)
        {
            //Debug.Log("Trying to turn off");
            isOn = false;
            GetComponent<Renderer>().material = offMaterial;
            SwitchConnectedObjects(false);
        }
        else if (isOn == false)
        {
            //Debug.Log("Trying to turn on");
            isOn = true;
            GetComponent<Renderer>().material = onMaterial;
            SwitchConnectedObjects(true);
        }

    }

    void SwitchConnectedObjects(bool turnOn)
    {
        foreach (GameObject o in connectedObjects)
        {
            if (o != null)
            {
                if (turnOn)
                {
                    o.GetComponent<MovableObject>().AddActivation();
                }
                else
                {
                    o.GetComponent<MovableObject>().RemoveActivation();
                }

            }
        }

        
    }

}
