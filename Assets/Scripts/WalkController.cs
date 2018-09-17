using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour {

	[SerializeField]
	private float speed = 1.0f;

	private float initY;

	public bool locked;

	void Start()
	{
		locked = false;
		initY = transform.position.y;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton(0) && (PlayerManager.instance.currentCreationMode == CreationMode.NONE)
									&& (PlayerManager.instance.currentLocomotionMode == LocomotionMode.WALK)
									&& !locked)
		{
			Vector3 newPos = transform.position + speed * Time.deltaTime * Camera.main.transform.forward;
			transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
		}
	}

	public void UpdateMovementLock(Vector3 v)
	{
		locked = Vector3.Dot(Camera.main.transform.forward,v) >= 0 ? false : true;
	}
}
