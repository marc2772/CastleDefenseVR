using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
	public int cashDrop = 0;
	public int HP = 1;
	public GameObject dropPopupPrefab;

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		Weapon weapon = col.gameObject.GetComponent<Weapon>();

		if(weapon != null)
		{
			HP -= weapon.GetDamageValue();
			if(HP <= 0)
				Die();
		}
	}

	void Die()
	{
		Vector3 position = transform.position;
		position.y += 2;

		GameObject popup = (GameObject)Instantiate(dropPopupPrefab, position, Quaternion.identity);
		popup.GetComponent<TextMesh>().text = "+" + cashDrop + "$";

		Destroy(gameObject);
	}
}
