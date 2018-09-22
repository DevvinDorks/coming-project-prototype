using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

    enum ObjectType { Binary, Oscillating, UnfoldingStair };
    [SerializeField] ObjectType objectType;
    [Tooltip("Number of obstactles to activate this object")]
    public int activationAmount;

    [Header("Binary Object")]
    [SerializeField] float slideSpeed;
    [SerializeField] Vector3 binaryStartPosition;
    [SerializeField] Vector3 binaryEndPosition;

    [Header("Oscillating Object")]
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;


    int currentAmount;
    bool activated = false;

    bool isSliding = false;

    bool isMoving = false;
    Vector3 worldStart;
    Vector3 worldTarget;


    void Start () {
        currentAmount = 0;

	}
	
	void Update () {

        if(activated == false && currentAmount == activationAmount)
        {
            activated = true;
            ActivateObject();
        }

        if (isSliding)
        {
            transform.position = Vector3.Lerp(binaryStartPosition, binaryEndPosition, Time.time * slideSpeed);
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(worldStart, worldTarget, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
        }

    }

    public void AddActivation()
    {
        currentAmount++;
    }

    void ActivateObject()
    {
        switch (objectType)
        {
            case ObjectType.Binary:
                //Move object from start state to end state
                isSliding = true;
                break;
            case ObjectType.UnfoldingStair:
                //unfold stair
                break;
            case ObjectType.Oscillating:
                //make object start moving
                worldStart = startPosition + transform.parent.transform.position;
                worldTarget = endPosition + transform.parent.transform.position;
                isMoving = true;
                break;
            default:
                break;
        }
    }




}
