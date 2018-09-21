using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

    enum ObjectType { Door, Stair, MovingPlatform };
    [SerializeField] ObjectType objectType;
    [Tooltip("Number of obstactles to activate this object")]
    public int activationAmount;

    [Header("Door")]
    [SerializeField] float openingSpeed;

    [Header("Moving Platform")]
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;


    int currentAmount;
    bool activated = false;

    bool isOpening = false;
    bool isMoving = false;
    Vector3 originalPosition;
    float endPositionY;


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

        if (isOpening)
        {
            var move = Time.deltaTime * openingSpeed;
            transform.Translate(0, -move, 0);

            if (transform.position.y < endPositionY)
            {
                isOpening = false;
            }
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
            case ObjectType.Door:
                //open door
                originalPosition = transform.position;
                endPositionY = originalPosition.y - transform.localScale.y;
                isOpening = true;
                break;
            case ObjectType.Stair:
                //unfold stair
                break;
            case ObjectType.MovingPlatform:
                //make platform start moving
                worldStart = startPosition + transform.parent.transform.position;
                worldTarget = endPosition + transform.parent.transform.position;
                isMoving = true;
                break;
            default:
                break;
        }
    }




}
