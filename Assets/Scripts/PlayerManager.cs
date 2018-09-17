using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionMode { TIMER, CLICK };
public enum LocomotionMode { NONE, TELEPORT, WALK };

public enum CreationMode { NONE, CREATE, MOVE, ROTATE, RESIZE };

public class PlayerManager : MonoBehaviour {

	#region player modes

	public SelectionMode currentSelectionMode;

	public LocomotionMode currentLocomotionMode;

	public CreationMode currentCreationMode;

	#endregion

	#region player menus

	public GameObject manipulationMenuPrefab;

	[HideInInspector]
	public GameObject manipulationMenu;

	public GameObject creationMenuPrefab;

	[HideInInspector]
	public GameObject creationMenu;

	#endregion

	#region manipulation parameters

	private Vector3 prevRot;
	private Vector3 iniScale;

	public float rotSpeed = 1.0f;
	public float scaleFactor = 0.1f;

	public float minScale = 0.5f, maxScale = 5.0f;

	#endregion

	[HideInInspector]
	public GameObject currentObject;

	// FIXME: this variables and the whole button behavior should be moved to a specific component that handle button clicks
	public Button timerButton, clickButton, teleportButton, walkButton, onButton, offButton;

	public static PlayerManager instance = null;

	private Vector3 minScaleVec, maxScaleVec;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(instance);
		}

		currentCreationMode = CreationMode.NONE;
		instance = this;

		manipulationMenu = GameObject.Instantiate(manipulationMenuPrefab, transform.position, transform.rotation);
		manipulationMenu.SetActive(false);

		creationMenu = GameObject.Instantiate(creationMenuPrefab, transform.position, transform.rotation);
		creationMenu.SetActive(false);

		minScaleVec = new Vector3(minScale,minScale,minScale);
		maxScaleVec = new Vector3(maxScale,maxScale,maxScale);
	}

	// Use this for initialization
	void Start ()
	{
		currentSelectionMode = SelectionMode.CLICK;
		currentLocomotionMode = LocomotionMode.TELEPORT;
	}
	
	void Update()
	{
		switch (currentCreationMode)
		{
			case CreationMode.ROTATE:
			{
				RotateObject();
				StartCoroutine(CheckDone());
				break;
			}
			case CreationMode.RESIZE:
			{
				ResizeObject();
				StartCoroutine(CheckDone());
				break;
			}
		}
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

			// Disable Creation Mode
			onButton.interactable = true;
			offButton.interactable = false;
			currentCreationMode = CreationMode.NONE;
		}
	}

	public void SetCreationMode(bool enabled)
	{
		currentCreationMode = enabled ? CreationMode.CREATE : CreationMode.NONE;

		onButton.interactable = !(currentCreationMode == CreationMode.CREATE);
		offButton.interactable = !onButton.interactable;
	}

	public void SetManipulationMode(CreationMode mode)
	{
		GameObject colObj = currentObject.GetComponentInChildren<Collider>().gameObject;
		colObj.layer = (mode == CreationMode.CREATE) ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("Ignore Raycast");
		
		currentCreationMode = mode;

		switch (currentCreationMode)
		{
			case CreationMode.ROTATE:
			{
				prevRot = Camera.main.transform.localEulerAngles;
				break;
			}
			case CreationMode.RESIZE:
			{
				iniScale = currentObject.transform.localScale;
				prevRot = Camera.main.transform.forward;
				break;
			}
		}

		manipulationMenu.SetActive(false);
	}

	void RotateObject()
	{
		Vector3 difRotation = prevRot - Camera.main.transform.localEulerAngles;

		currentObject.transform.Rotate(new Vector3(0,difRotation.y,0) * rotSpeed);

		prevRot = Camera.main.transform.localEulerAngles;
	}

	void ResizeObject()
	{
		// What follows gives almost the same result but it's done purely with vectorial algebra (dots and cross products)

		// Project both vectors to the Camera's (Y,Z) plane
		Vector3 prevRotProj = Vector3.ProjectOnPlane(prevRot,Camera.main.transform.right);
		Vector3 angleProj = Vector3.ProjectOnPlane(Camera.main.transform.forward,Camera.main.transform.right);  

		float angle = Vector3.Angle(prevRotProj, angleProj);

		// Check whether we are "above" (we are enlarging the object) or "below" (we are shrinking
		// the object) initial forward vector and update the scale accordingly
		
		Vector3 cross = Vector3.Cross(prevRotProj,angleProj);
		currentObject.transform.localScale = Vector3.Dot(cross,Camera.main.transform.right) < 0 ? Vector3.Min(iniScale * (1.0f + angle * scaleFactor), maxScaleVec) :
																								  Vector3.Max(minScaleVec, iniScale - new Vector3(angle,angle,angle) * scaleFactor); 

		// Vector3 difRotation = prevRot - Camera.main.transform.localEulerAngles;

		// if (difRotation.x >= 0)
		// 	currentObject.transform.localScale = iniScale * (1.0f + difRotation.x * scaleFactor);
		// else
		// {
		// 	Vector3 scale = new Vector3(difRotation.x,difRotation.x,difRotation.x) * scaleFactor;

		// 	currentObject.transform.localScale = Vector3.Max(new Vector3(0.25f,0.25f,0.25f), iniScale + scale); 
		// }
	}

	// We have to implement this as a coroutine in order to discard the frame in which the mouse down event is
	// recognized as if we set the CreationMode to CREATE right in that frame, then the HandleManipulate funciton
	// from ManipulableObject is called this frame, making the ManipulationMenu to show up when it shouldn't
	IEnumerator CheckDone()
	{
		if (Input.GetMouseButtonDown(0))
		{
			yield return null;

			SetManipulationMode(CreationMode.CREATE);
		}
	}
}
