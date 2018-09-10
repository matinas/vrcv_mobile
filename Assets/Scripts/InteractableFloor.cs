using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class InteractableFloor : MonoBehaviour {

	public GameObject player;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Press += HandleButtonPress;
	}

	void HandleButtonPress (RaycastHit hit)
	{
		if (SettingsManager.instance.currentLocomotionMode == LocomotionMode.TELEPORT)
		{
			player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
		}
	}
}
