using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(InteractableObject))]
public class LabeledObject : MonoBehaviour {

	public Transform start;
	public Transform end;

	public Transform label;

	public float duration;

	private LineRenderer lineRender;

	void Awake()
	{
		InteractableObject io = GetComponent<InteractableObject>();

		io.GazeEnter += HandleGazeEnter;
		io.GazeExit += HandleGazeExit;
	}

	// Use this for initialization
	void Start ()
	{
		lineRender = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void HandleGazeEnter()
	{
		lineRender.SetPosition(0, start.position);
		lineRender.enabled = true;

		StartCoroutine(DrawLabel());
	}

	void HandleGazeExit()
	{
		lineRender.enabled = false;
		label.gameObject.SetActive(false);
	}

	IEnumerator DrawLabel()
	{
		float t = 0;

		while (t < duration)
		{
			Vector3 p = Vector3.Lerp(start.position, end.position, t/duration);	
			lineRender.SetPosition(1, p);

			t += Time.deltaTime;

			yield return null;
		}

		label.gameObject.SetActive(true);
	}

}
