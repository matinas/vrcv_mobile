using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour {

	InteractableObject currentInteractable;
	
	private Reticle reticle;

	private bool beingPressed;

	// Use this for initialization
	void Start ()
	{
		currentInteractable = null;
		beingPressed = false;
		reticle = GetComponentInChildren<Reticle>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessGaze();
	}

	void ProcessGaze()
	{
		Ray raycastRay = new Ray(transform.position, transform.forward);

		RaycastHit hitInfo;
		InteractableObject lastInteractable;

		Debug.DrawRay(raycastRay.origin, raycastRay.direction * 100, Color.blue);

		if (Physics.Raycast(raycastRay, out hitInfo))
		{
			if ((lastInteractable = hitInfo.transform.gameObject.GetComponentInChildren<InteractableObject>()) != null)
			{
				if ((currentInteractable == null) || (currentInteractable != lastInteractable)) // A new Interactable object was gazed
				{
					if (currentInteractable != null)
						currentInteractable.OnGazeExit();
						
					currentInteractable = lastInteractable;
					currentInteractable.OnGazeEnter();
					reticle.OnGazeEnter();
				}
				else // Same Interactable object is being gazed
				{
					currentInteractable.OnGaze();
				}
			}
			else
			{
				if (currentInteractable != null) // An Interactable object is not being gazed anymore
				{
					currentInteractable.OnGazeExit();
					currentInteractable = null;
					reticle.OnGazeExit();
				}
			}
		}

		if (Input.GetMouseButtonDown(0) && currentInteractable != null)
		{
			currentInteractable.OnPress();
			beingPressed = true;
		}
		else
		{
			if (Input.GetMouseButtonUp(0) && currentInteractable != null)
			{
				currentInteractable.OnRelease();
				beingPressed = false;
			}
			else
			{
				if (beingPressed && currentInteractable != null)
				{
					currentInteractable.OnHold();
				}
			}
		}
	}
}
