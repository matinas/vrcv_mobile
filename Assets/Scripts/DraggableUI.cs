using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InteractableObject))]
public class DraggableUI : MonoBehaviour {

	public Image mainPanel;

	public Color unselectedColor;
	public Color highlightedColor;

	public Vector3 offset;

	// Use this for initialization
	void Start ()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.GazeEnter += HandleGazeEnter;
		io.GazeExit += HandleGazeExit;
		io.Press += HandlePress;
		io.Release += HandleRelease;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleGazeEnter()
	{
		mainPanel.color = highlightedColor;
	}

	void HandleGazeExit()
	{
		mainPanel.color = unselectedColor;
	}

	void HandlePress(RaycastHit hit)
	{
		PlayerManager.instance.currentCreationMode = CreationMode.UIDRAG;
		PlayerManager.instance.currentObject = this.gameObject;

		offset = hit.point - transform.position;

		gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
	}
	
	void HandleRelease()
	{
		Debug.Log("panel released");
	}
}
