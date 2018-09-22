using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {

    enum ObjectType { Binary, Oscillating };
    [SerializeField] ObjectType objectType;
    [Tooltip("Number of obstactles to activate this object")]
    public int activationAmount;

    [Header("Binary Object")]
    [SerializeField]
    float slideSpeed;
    [SerializeField] Vector3 binaryStartPosition;
    [SerializeField] Vector3 binaryEndPosition;

    [Header("Oscillating Object")]
    [SerializeField]
    float moveSpeed;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;


    int currentAmount;
    bool activated = false;

    //bool isSliding = false;
    bool moveToEndPosition = false;
    bool moveToStartPosition = false;

    bool isMoving = false;
    Vector3 worldStart;
    Vector3 worldTarget;


    void Start()
    {
        currentAmount = 0;

    }

    void Update()
    {

        if (activated == false && currentAmount == activationAmount)
        {
            //Debug.Log("Trying to activate object...");
            activated = true;
            ActivateObject();
        }
        else if (activated == true && currentAmount < activationAmount)
        {
            activated = false;
            DeactivateObject();
        }

        if (moveToEndPosition)
        {
            transform.position = Vector3.Lerp(binaryStartPosition, binaryEndPosition, Time.time * slideSpeed);
        }
        else if (moveToStartPosition)
        {
            transform.position = Vector3.Lerp(binaryEndPosition, binaryStartPosition, Time.time * slideSpeed);
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
        }

    }

    public void AddActivation()
    {
        currentAmount++;
    }

    public void RemoveActivation()
    {
        currentAmount--;
    }

    void ActivateObject()
    {
        //Debug.Log("Activating object");
        switch (objectType)
        {
            case ObjectType.Binary:
                //Move object from start state to end state
                moveToEndPosition = true;
                moveToStartPosition = false;
                break;
            case ObjectType.Oscillating:
                //make object start moving
                isMoving = true;
                break;
            default:
                break;
        }
    }

    void DeactivateObject()
    {
        //Debug.Log("Deactivating object");
        switch (objectType)
        {
            case ObjectType.Binary:
                //Move object back to start state
                moveToEndPosition = false;
                moveToStartPosition = true;
                break;
            case ObjectType.Oscillating:
                //make object stop moving
                isMoving = false;
                break;
            default:
                break;
        }
    }

}
