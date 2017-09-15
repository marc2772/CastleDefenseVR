using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {
    public HandInteraction rightHand;
    public HandInteraction leftHand;

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "RightFingers")
        {
            rightHand.ObjectColliding = gameObject;
        }
        else if (other.tag == "LeftFingers") {
            leftHand.ObjectColliding = gameObject;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "RightFingers")
        {
            rightHand.ObjectColliding = gameObject;
        }
        else if (other.tag == "LeftFingers")
        {
            leftHand.ObjectColliding = gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "RightFingers")
        {
            if (rightHand.ObjectColliding == gameObject)
            {
                rightHand.ObjectColliding = null;
            }
        }
        else if (other.tag == "LeftFingers")
        {
            if (leftHand.ObjectColliding == gameObject) {
                leftHand.ObjectColliding = null;
            }
        }
    }
}
