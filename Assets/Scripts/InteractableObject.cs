using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	public event Action GazeEnter = () => {};
	public event Action<RaycastHit> Gaze = (hit) => {};
	public event Action GazeExit = () => {};

	public event Action<RaycastHit> Press = (hit) => {};
	public event Action<RaycastHit> Hold = (hit) => {};
	public event Action Release = () => {};


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnGazeEnter()
	{
		GazeEnter();
		Debug.Log("Gaze Enter!");
	}

	public void OnGaze(RaycastHit hit)
	{
		Gaze(hit);
		Debug.Log("Gaze!");
	}

	public void OnGazeExit()
	{
		GazeExit();
		Debug.Log("Gaze Exit!");
	}

	public void OnPress(RaycastHit hit)
	{
		Press(hit);
		Debug.Log("Pressed button!");
	}

	public void OnHold(RaycastHit hit)
	{
		Hold(hit);
		Debug.Log("Holding button!");
	}

	public void OnRelease()
	{
		Release();
		Debug.Log("Released button!");
	}
}
