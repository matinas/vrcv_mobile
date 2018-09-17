using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class InteractableSurface : MonoBehaviour {

	public GameObject player;

	private Vector3 creationHitPos;

	private bool firstRotation;
	private Vector3 iniRotation;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Press += HandleButtonPress;
		io.Gaze += HandleGaze;

		firstRotation = true;
	}

	void HandleButtonPress (RaycastHit hit)
	{
		if (PlayerManager.instance.currentCreationMode != CreationMode.NONE)
		{
			switch (PlayerManager.instance.currentCreationMode)
			{
				case CreationMode.CREATE:
				{
					ShowCreationMenu(hit);
					break;
				}
				case CreationMode.MOVE:
				case CreationMode.ROTATE:
				case CreationMode.RESIZE:
				{
					PlayerManager.instance.SetManipulationMode(CreationMode.CREATE);
					firstRotation = true;
					break;
				}
			}			
		}
		else if (PlayerManager.instance.currentLocomotionMode == LocomotionMode.TELEPORT)
		{
			player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
		}
	}

	void HandleGaze (RaycastHit hit)
	{
		if (PlayerManager.instance.currentCreationMode != CreationMode.NONE)
		{
			switch (PlayerManager.instance.currentCreationMode)
			{
				case CreationMode.MOVE:
				{
					MoveObject(hit);
					break;
				}
			}
		}
	}

	void HandleItemSelected(GameObject item)
	{
		GameObject go = GameObject.Instantiate(item,creationHitPos,Quaternion.identity);
		PlayerManager.instance.creationMenu.SetActive(false);
	}

	void ShowCreationMenu(RaycastHit hit)
	{
		GameObject creationMenu = PlayerManager.instance.creationMenu;

		creationMenu.SetActive(true);
		creationMenu.transform.position = hit.point + new Vector3(0.0f,0.5f,0.0f);

		foreach (InteractableItem i in creationMenu.GetComponentsInChildren<InteractableItem>())
		{
			i.ClearInvocationList();
			i.OnItemSelected += HandleItemSelected;
		}

		// Rotate the menu about the Y axis so it faces viewer's direction
		Vector3 menuRotation = creationMenu.transform.localEulerAngles;
		creationMenu.transform.localEulerAngles = new Vector3(menuRotation.x, Camera.main.transform.localEulerAngles.y, menuRotation.z);
		
		creationHitPos = hit.point;
	}

	void MoveObject(RaycastHit hit)
	{
		PlayerManager.instance.currentObject.transform.position = hit.point;
	}
}
