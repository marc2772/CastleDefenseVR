using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPopup : MonoBehaviour
{
	private float speed = 0.001f;

	private float timeShown = 2;
	private float timeSinceSpawn = 0;

	void Start()
	{
		transform.LookAt(Camera.main.transform);
		transform.Rotate(0, 180, 0);  //Because text is facing the other way for whatever reason
	}

	void Update()
	{
		transform.Translate(new Vector3(0, speed, 0));

		timeSinceSpawn += Time.deltaTime;
		if(timeSinceSpawn > timeShown)
			Destroy(gameObject);
	}
}
