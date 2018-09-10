using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {

	public event Action OnSelectionComplete = () => {};

	public Color normalColor;
	public Color selectionColor;

	public float selectionScale = 0.02f;
	public float scaleSpeed = 0.5f;

	private Material material;
	private float initScale;

	private Coroutine scaleCoroutine;

	// Use this for initialization
	void Start ()
	{
		material = GetComponent<MeshRenderer>().sharedMaterial;
		material.SetColor("_Color", normalColor);
		
		initScale = gameObject.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnGazeEnter()
	{
		material.SetColor("_Color", selectionColor);

		if (scaleCoroutine != null)
			StopCoroutine(scaleCoroutine);
			
		scaleCoroutine = StartCoroutine(GrowReticle());
	}

	IEnumerator GrowReticle()
	{
		while (transform.localScale.x < selectionScale)
		{
			float t = Time.deltaTime * scaleSpeed;
			transform.localScale += new Vector3(t,t,t);

			yield return null;
		}
	}

	public void OnGazeExit()
	{
		material.SetColor("_Color", normalColor);

		if (scaleCoroutine != null)
			StopCoroutine(scaleCoroutine);

		scaleCoroutine = StartCoroutine(ShrinkReticle());
	}
	
	IEnumerator ShrinkReticle()
	{
		while (transform.localScale.x >= initScale)
		{
			float t = Time.deltaTime * scaleSpeed;
			transform.localScale -= new Vector3(t,t,t);

			yield return null;
		}
	}
}
