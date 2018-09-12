using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class InteractableFloor : MonoBehaviour {

	public GameObject player;

	private Vector3 iniPos;

	public float speed = 1.0f;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Press += HandleButtonPress;
		//io.Hold += HandleButtonHold;
	}

	void HandleButtonPress (RaycastHit hit)
	{
		if (SettingsManager.instance.currentLocomotionMode == LocomotionMode.TELEPORT)
		{
			player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
		}
	}

	// void HandleButtonHold (RaycastHit hit)
	// {
	// 	if (SettingsManager.instance.currentLocomotionMode == LocomotionMode.WALK)
	// 	{
	// 		Vector3 hitVector = hit.point - player.transform.position;
	// 		hitVector.Normalize();
	// 		Vector3 hitVectorProj = new Vector3(hitVector.x, 0, hitVector.z);

	// 		player.transform.position += speed * Time.deltaTime * hitVectorProj;
	// 	}
	// }
}
