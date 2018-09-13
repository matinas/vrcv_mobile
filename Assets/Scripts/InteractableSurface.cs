using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class InteractableSurface : MonoBehaviour {

	public GameObject player;

	public GameObject creationMenuPrefab;

	public float speed = 1.0f;

	private Vector3 iniPos;

	private Vector3 creationHitPos;

	private GameObject creationMenuRef;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Press += HandleButtonPress;
		//io.Hold += HandleButtonHold;

		creationMenuRef = GameObject.Instantiate(creationMenuPrefab, transform.position, transform.rotation);
		creationMenuRef.SetActive(false);
		foreach (InteractableItem i in creationMenuRef.GetComponentsInChildren<InteractableItem>())
		{
			i.OnItemSelected += HandleItemSelected;
		}
	}

	void HandleButtonPress (RaycastHit hit)
	{
		if (SettingsManager.instance.currentLocomotionMode == LocomotionMode.TELEPORT)
		{
			player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
		}
		else if (SettingsManager.instance.creationModeEnabled)
		{
			creationMenuRef.SetActive(true);
			creationMenuRef.transform.position = hit.point + new Vector3(0.0f,0.5f,0.0f);

			// Rotate the menu about the Y axis so it faces viewer's direction
			Vector3 menuRotation = creationMenuRef.transform.localEulerAngles;
			creationMenuRef.transform.localEulerAngles = new Vector3(menuRotation.x, Camera.main.transform.localEulerAngles.y, menuRotation.z);
			
			creationHitPos = hit.point;
		}
	}

	void HandleItemSelected(GameObject item)
	{
		GameObject go = GameObject.Instantiate(item,creationHitPos,Quaternion.identity);
		creationMenuRef.SetActive(false);
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
