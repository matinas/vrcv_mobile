using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class ManipulableObject : MonoBehaviour {

	public string labelText;

	public float outlineWidth = 0.02f;

	private Material material;

	private AudioSource audioSrc;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.GazeEnter += HandleGazeEnter;
		io.GazeExit += HandleGazeExit;
		io.Press += HandleManipulate;

		material = GetComponentInChildren<Renderer>().material;

		material.SetFloat("_OutlineWidth", 0.0f);

		audioSrc = GetComponent<AudioSource>();
	}

	void HandleManipulate(RaycastHit hit)
	{
		if (PlayerManager.instance.currentCreationMode == CreationMode.CREATE)
		{
			PlayerManager.instance.manipulationMenu.SetActive(true);
			PlayerManager.instance.manipulationMenu.transform.position = hit.point;
			PlayerManager.instance.manipulationMenu.transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y, 0);

			PlayerManager.instance.currentObject = transform.gameObject;
			PlayerManager.instance.currentObject.GetComponentInChildren<Collider>().gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
	}

	void HandleGazeEnter()
	{
		LabelManager.instance.ShowLabel(transform.position, labelText);

		material.SetFloat("_OutlineWidth", outlineWidth);

		audioSrc.Play();
	}

	void HandleGazeExit()
	{
		LabelManager.instance.HideLabel();

		material.SetFloat("_OutlineWidth", 0.0f);
	}
}
