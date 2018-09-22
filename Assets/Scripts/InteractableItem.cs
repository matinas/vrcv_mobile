using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(InteractableObject))]
public class InteractableItem : MonoBehaviour {

	public GameObject itemPrefab;

	public event Action<GameObject> OnItemSelected = (go) => {};

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.Press += HandleButtonPress;
	}

	void HandleButtonPress (RaycastHit hit)
	{
		OnItemSelected(itemPrefab);
	}

	public void ClearInvocationList()
	{
		System.Delegate[] deletegates = OnItemSelected.GetInvocationList();
        for (int i = 0; i < deletegates.Length; i++)
        {
            // Remove all events
            OnItemSelected -= deletegates[i] as Action<GameObject>;
        }
	}
}
