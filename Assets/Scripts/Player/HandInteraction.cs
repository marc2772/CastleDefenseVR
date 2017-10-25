using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandInteraction : MonoBehaviour
{
	public SteamVR_TrackedController controller;
    public Transform head;
    public float velocityMultiplier = 1f;

    private GameObject objectInHand;
    private GameObject objectColliding;

    private LayerMask grabbable;

    private HandAnimation handAnimation;

    private SteamVR_Controller.Device controllerDevice
    {
        get { return SteamVR_Controller.Input((int)controller.controllerIndex); }
    }

    void OnEnable()
	{
        controller.Gripped += Grab;
        controller.Ungripped += ReleaseObject;
	}

	void OnDisable()
	{
        controller.Gripped -= Grab;
        controller.Ungripped -= ReleaseObject;
    }

    void Awake()
    {
        handAnimation = GetComponent<HandAnimation>();
        grabbable = 1 << 16;
    }

    void Grab(object sender, ClickedEventArgs e)
    {
        if (!objectInHand && objectColliding)
        {
            handAnimation.CloseHand();

            objectInHand = objectColliding;
            objectColliding = null;

            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

            handAnimation.HideHand();
        }
    }

    void ReleaseObject(object sender, ClickedEventArgs e)
    {
        if (objectInHand)
        {
            if (GetComponent<FixedJoint>())
            {
                handAnimation.OpenHand();

                RemoveLinkBetweenHandAndObjectInHand();
                AddVelocityToObjectInHand();

                handAnimation.ShowHand();
            }
            objectInHand = null;
        }
    }

    void RemoveLinkBetweenHandAndObjectInHand()
    {
        GetComponent<FixedJoint>().connectedBody = null;
        Destroy(GetComponent<FixedJoint>());
    }

    void AddVelocityToObjectInHand() 
    {
        objectInHand.GetComponent<Rigidbody>().velocity = velocityMultiplier * (head.rotation * controllerDevice.velocity);
        objectInHand.GetComponent<Rigidbody>().angularVelocity = velocityMultiplier * (head.rotation * controllerDevice.angularVelocity);
    }

    FixedJoint AddFixedJoint()
    {
        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.breakForce = 20000;
        fixedJoint.breakTorque = 20000;
        return fixedJoint;
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & grabbable) != 0)
        {
            ObjectColliding = other.gameObject;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & grabbable) != 0)
        {
            ObjectColliding = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & grabbable) != 0)
        {
            if (other.gameObject.Equals(objectColliding))
            {
                ObjectColliding = null;
            }
        }
    }

    private void OnJointBreak(float breakForce)
    {
        handAnimation.OpenHand();
        handAnimation.ShowHand();
    }

    public GameObject ObjectColliding
    {

        get
        {
            return objectColliding;
        }

        set
        {
            if (value == null) {
                objectColliding = null;
                return;
            }

            if (value.GetComponent<Rigidbody>())
            {
                objectColliding = value;
            }
        }
    }
}
