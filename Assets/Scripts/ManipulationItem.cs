using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(InteractableObject))]
public class ManipulationItem : MonoBehaviour {

	public CreationMode mode;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Press += HandleButtonPress;
	}

	void HandleButtonPress(RaycastHit hit)
	{
		PlayerManager.instance.SetManipulationMode(mode);
	}
}
