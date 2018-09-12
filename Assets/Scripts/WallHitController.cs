using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<WalkController>().UpdateMovementLock(transform.right);
		}
	}
}
