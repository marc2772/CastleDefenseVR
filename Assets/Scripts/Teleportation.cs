using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour {

    public SteamVR_TrackedController controller;
    public SteamVR_Controller.Device device;

    public bool teleportationEngaged = false;

    void OnEnable()
    {
        controller.PadTouched += InitTeleportation;
        controller.PadUntouched += CompleteTeleportation;
    }

    void OnDisable()
    {
        controller.PadTouched -= InitTeleportation;
        controller.PadUntouched -= CompleteTeleportation;
    }

    void InitTeleportation(object sender, ClickedEventArgs e)
    {
        teleportationEngaged = true;
    }

    void CompleteTeleportation(object sender, ClickedEventArgs e)
    {
        teleportationEngaged = false;
    }

    void Start()
    {
        device = SteamVR_Controller.Input((int)controller.controllerIndex);
    }

    void Update()
    {
        if (teleportationEngaged)
        {
            Debug.Log("(" + device.GetAxis().x + ", " + device.GetAxis().y + ")");
        }
    }
}
