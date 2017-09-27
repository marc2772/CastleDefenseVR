using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimation : MonoBehaviour
{
	public SteamVR_TrackedController controller;

	private Animator animator;

	void OnEnable()
	{
		controller.TriggerClicked += CloseHand;
		controller.TriggerUnclicked += OpenHand;
	}

	void OnDisable()
	{
		controller.TriggerClicked -= CloseHand;
		controller.TriggerUnclicked -= OpenHand;
	}

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void CloseHand(object sender, ClickedEventArgs e)
	{
		animator.SetBool("close", true);
	}

	void OpenHand(object sender, ClickedEventArgs e)
	{
		animator.SetBool("close", false);
	}
}
