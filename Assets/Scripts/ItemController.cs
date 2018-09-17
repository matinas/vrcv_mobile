using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

	public float itemRotSpeed = 1.0f;

	// Update is called once per frame
	void Update ()
	{
		foreach (Transform child in transform)
		{
			//Transform item = child.GetChild(0);
			child.Rotate(child.up, itemRotSpeed * Time.deltaTime);
		}
	}
}
