using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionMode { TIMER, CLICK };
public enum LocomotionMode { NONE, TELEPORT, WALK };

public class SettingsManager : MonoBehaviour {

	public SelectionMode currentSelectionMode;

	public LocomotionMode currentLocomotionMode;

	public bool creationModeEnabled;

	public Button timerButton, clickButton, teleportButton, walkButton, onButton, offButton;

	public static SettingsManager instance = null;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(instance);
		}

		creationModeEnabled = false;
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
			timerButton.interactable = currentSelectionMode == SelectionMode.TIMER;
			clickButton.interactable = currentSelectionMode == SelectionMode.CLICK;
				
			currentSelectionMode = (SelectionMode) sm;
		}
	}

	public void SetLocomotionMode(int lm)
	{
		if (currentLocomotionMode != (LocomotionMode) lm)
		{
			if (currentLocomotionMode == LocomotionMode.NONE)
			{
				teleportButton.interactable = !((LocomotionMode) lm == LocomotionMode.TELEPORT);
				walkButton.interactable = !((LocomotionMode) lm == LocomotionMode.WALK);
			}
			else
			{
				teleportButton.interactable = currentLocomotionMode == LocomotionMode.TELEPORT;
				walkButton.interactable = currentLocomotionMode == LocomotionMode.WALK;
			}

			currentLocomotionMode = (LocomotionMode) lm;

			creationModeEnabled = false;
			onButton.interactable = !creationModeEnabled;
			offButton.interactable = creationModeEnabled;
		}
	}

	public void SetCreationMode(bool enabled)
	{
		creationModeEnabled = enabled;
		currentLocomotionMode = LocomotionMode.NONE;

		teleportButton.interactable	= true;
		walkButton.interactable = true;

		onButton.interactable = !creationModeEnabled;
		offButton.interactable = creationModeEnabled;
	}
}
