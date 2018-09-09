using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {

	public Color normalColor;
	public Color selectionColor;

	private Material material;

	// Use this for initialization
	void Start ()
	{
		material = GetComponent<MeshRenderer>().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnGazeEnter()
	{
		Debug.Log("Se setea color Enter");
		material.SetColor("_Color", selectionColor);
	}

	public void OnGazeExit()
	{
		Debug.Log("Se setea color Exit");
		material.SetColor("_Color", normalColor);
	}
}
