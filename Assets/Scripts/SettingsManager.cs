using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionMode { TIMER, CLICK };
public enum LocomotionMode { TELEPORT, WALK };

public class SettingsManager : MonoBehaviour {

	public SelectionMode currentSelectionMode;

	public LocomotionMode currentLocomotionMode;

	public Button timerButton, clickButton, teleportButton, walkButton;

	public static SettingsManager instance = null;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(instance);
		}

		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		currentSelectionMode = SelectionMode.CLICK;
		currentLocomotionMode = LocomotionMode.TELEPORT;
	}
	
	public void SetSelectionMode(int sm)
	{
		if (currentSelectionMode != (SelectionMode) sm)
		{
			if (currentSelectionMode == SelectionMode.TIMER)
				timerButton.interactable = true;
			else
				clickButton.interactable = true;
				
			currentSelectionMode = (SelectionMode) sm;
		}
	}

	public void SetLocomotionMode(int lm)
	{
		if (currentLocomotionMode != (LocomotionMode) lm)
		{
			if (currentLocomotionMode == LocomotionMode.TELEPORT)
				teleportButton.interactable = true;
			else
				walkButton.interactable = true;

			currentLocomotionMode = (LocomotionMode) lm;
		}
	}
}
