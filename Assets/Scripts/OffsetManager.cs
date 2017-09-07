using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetManager : MonoBehaviour {
    public Vector3 offsetEleurRotation;
    public Vector3 offsetPosition;

    //public bool isRightHand;
    // Use this for initialization
    void Start () {
        transform.rotation = Quaternion.Euler(offsetEleurRotation);
        transform.localPosition = offsetPosition;

        /*if (isRightHand) {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
