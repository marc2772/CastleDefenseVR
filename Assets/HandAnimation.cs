using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour {

    public SteamVR_TrackedController controller;

    private Animator animator;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void OnEnable()
    {
        controller.Gripped += CloseThreeFingers;
        controller.Ungripped += OpenThreeFingers;

        controller.TriggerClicked += CloseIndex;
        controller.TriggerUnclicked += OpenIndex;

        controller.PadTouched += CloseThumb;
        controller.PadUntouched += OpenThumb;
    }

    void OnDisable()
    {
        controller.Gripped -= CloseThreeFingers;
        controller.Ungripped -= OpenThreeFingers;

        controller.TriggerClicked -= CloseIndex;
        controller.TriggerUnclicked -= OpenIndex;

        controller.PadTouched += CloseThumb;
        controller.PadUntouched += OpenThumb;
    }

    public void CloseHand()
    {
        animator.SetBool("close", true);
    }

    public void OpenHand()
    {
        animator.SetBool("close", false);
    }

    void CloseHand(object sender, ClickedEventArgs e)
    {
        animator.SetBool("close", true);
    }

    void OpenHand(object sender, ClickedEventArgs e)
    {
        animator.SetBool("close", false);
    }

    void CloseThreeFingers(object sender, ClickedEventArgs e)
    {
        animator.SetBool("three_fingers_close", true);
    }

    void OpenThreeFingers(object sender, ClickedEventArgs e)
    {
        animator.SetBool("three_fingers_close", false);
    }

    void CloseIndex(object sender, ClickedEventArgs e)
    {
        animator.SetBool("close_index", true);
    }

    void OpenIndex(object sender, ClickedEventArgs e)
    {
        animator.SetBool("close_index", false);
    }

    void CloseThumb(object sender, ClickedEventArgs e)
    {
        animator.SetBool("close_thumb", true);
    }

    void OpenThumb(object sender, ClickedEventArgs e)
    {
        animator.SetBool("close_thumb", false);
    }

    public void HideHand()
    {
        skinnedMeshRenderer.enabled = false;
    }

    public void ShowHand()
    {
        skinnedMeshRenderer.enabled = true;
    }
}
