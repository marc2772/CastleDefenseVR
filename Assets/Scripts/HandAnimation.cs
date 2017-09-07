using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimation : MonoBehaviour {
    public bool isRightHand = false;

    SteamVR_Controller.Device controller;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (isRightHand)
        {
            controller = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost));
        }
        else
        {
            controller = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.GetPress(trigger))
        {
            animator.SetBool("close", true);
        }

        if (controller.GetPressUp(trigger)) {
            animator.SetBool("close", false);
        }
    }
}
