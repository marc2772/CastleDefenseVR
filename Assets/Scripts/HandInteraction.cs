using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandInteraction : MonoBehaviour
{
	public SteamVR_TrackedController controller;
    public float velocityMultiplier = 1f;

    private GameObject objectInHand;
    private GameObject objectColliding;

    private LayerMask grabbable;
	private Animator animator;

    private SteamVR_Controller.Device controllerDevice
    {
        get { return SteamVR_Controller.Input((int)controller.controllerIndex); }
    }

    void OnEnable()
	{
		controller.Gripped += CloseHand;
		controller.Ungripped += OpenHand;
        controller.Gripped += Grab;
        controller.Ungripped += ReleaseObject;
	}

	void OnDisable()
	{
		controller.Gripped -= CloseHand;
		controller.Ungripped -= OpenHand;
        controller.Gripped -= Grab;
        controller.Ungripped -= ReleaseObject;
    }

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

    private void Start()
    {
        grabbable = 1 << 16;
    }

    void CloseHand(object sender, ClickedEventArgs e)
	{
		animator.SetBool("close", true);
	}

	void OpenHand(object sender, ClickedEventArgs e)
	{
		animator.SetBool("close", false);
	}

    public void Grab(object sender, ClickedEventArgs e)
    {
        if (!objectInHand && objectColliding)
        {
            objectInHand = objectColliding;
            objectColliding = null;

            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        }
    }

    private void ReleaseObject(object sender, ClickedEventArgs e)
    {
        if (objectInHand)
        {
            if (GetComponent<FixedJoint>())
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());

                objectInHand.GetComponent<Rigidbody>().velocity = velocityMultiplier * controllerDevice.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = velocityMultiplier * controllerDevice.angularVelocity;
            }
            objectInHand = null;
        }
    }

    private FixedJoint AddFixedJoint()
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
