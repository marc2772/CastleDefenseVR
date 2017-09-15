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

    private Rigidbody rb;

	private Animator animator;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)controller.controllerIndex); }
    }

    void OnEnable()
	{
		controller.TriggerClicked += CloseHand;
		controller.TriggerUnclicked += OpenHand;
        controller.TriggerClicked += Grab;
        controller.TriggerUnclicked += ReleaseObject;
	}

	void OnDisable()
	{
		controller.TriggerClicked -= CloseHand;
		controller.TriggerUnclicked -= OpenHand;
        controller.TriggerClicked -= Grab;
        controller.TriggerUnclicked -= ReleaseObject;
    }

	void Awake()
	{
		animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

                objectInHand.GetComponent<Rigidbody>().velocity = velocityMultiplier * Controller.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = velocityMultiplier * Controller.angularVelocity;
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
