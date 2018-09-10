using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	public event Action GazeEnter = () => {};
	public event Action Gaze = () => {};
	public event Action GazeExit = () => {};

	public event Action<RaycastHit> Press = (hit) => {};
	public event Action Hold = () => {};
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

	public void OnGaze()
	{
		Gaze();
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

	public void OnHold()
	{
		Hold();
		Debug.Log("Holding button!");
	}

	public void OnRelease()
	{
		Release();
		Debug.Log("Released button!");
	}
}
