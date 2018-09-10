using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

[RequireComponent(typeof(InteractableObject))]
public class InteractableButton : MonoBehaviour {

	private InteractableObject io;

	private Button button;

    void Awake()
	{
		io = GetComponent<InteractableObject>();

		io.GazeEnter += HandleButtonGazeEnter;
		io.GazeExit += HandleButtonGazeExit;
		io.Press += HandleButtonPress;
		io.Release += HandleButtonRelease;

		button = GetComponent<Button>();
	}

	void HandleButtonGazeEnter()
	{
		button.SendMessage("OnPointerEnter", new PointerEventData(EventSystem.current));
	}

	void HandleButtonGazeExit()
	{
		button.SendMessage("OnPointerExit", new PointerEventData(EventSystem.current));
	}

	void HandleButtonPress(RaycastHit hit)
	{
		button.SendMessage("OnPointerDown", new PointerEventData(EventSystem.current));
	}

	void HandleButtonRelease()
	{
		button.SendMessage("OnPointerClick", new PointerEventData(EventSystem.current));
		button.interactable = false;
	}
}
