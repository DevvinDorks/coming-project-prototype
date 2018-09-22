using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public enum SwitchType { ActivatedByLaser, ActivatedByKeyPress};

    [SerializeField] public SwitchType switchType;
    [SerializeField] bool canBeSwitchedOff;
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;

    [SerializeField] GameObject connectedObject;

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
            SwitchConnectedObject(false);
        }
        else if (isOn == false)
        {
            //Debug.Log("Trying to turn on");
            isOn = true;
            GetComponent<Renderer>().material = onMaterial;
            SwitchConnectedObject(true);
        }

    }

    void SwitchConnectedObject(bool turnOn)
    {
        if (connectedObject != null)
        {
            if (turnOn)
            {
                connectedObject.GetComponent<MovableObject>().AddActivation();
            }
            else
            {
                connectedObject.GetComponent<MovableObject>().RemoveActivation();
            }

        }
    }

}
