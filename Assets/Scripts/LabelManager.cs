using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class LabelManager : MonoBehaviour {

	public float duration;

	public float length;

	public GameObject label;

	public static LabelManager instance = null;

	private LineRenderer lineRender;

	private Coroutine drawCrt;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(instance);
		}

		instance = this;
	}

	void Start()
	{
		lineRender = GetComponent<LineRenderer>();
	}
	
	public void ShowLabel(Vector3 start, string text)
	{
		lineRender.SetPosition(0, start);
		lineRender.enabled = true;

		Vector3 startModif = start + new Vector3(0.0f,0.5f,0.0f);

		Vector3 dir = 0.75f*transform.up + 0.5f*Camera.main.transform.right;
		Vector3 end = startModif + dir*length;

		drawCrt = StartCoroutine(DrawLabel(startModif,end,text));
	}

	IEnumerator DrawLabel(Vector3 startPos, Vector3 endPos, string text)
	{
		float t = 0;

		while (t < duration)
		{
			Vector3 p = Vector3.Lerp(startPos, endPos, t/duration);	
			lineRender.SetPosition(1, p);

			t += Time.deltaTime;

			yield return null;
		}

		label.gameObject.SetActive(true);
		label.GetComponentInChildren<Text>().text = text;
		label.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, 0.0f);
		label.transform.position = endPos;
}

	public void HideLabel()
	{
		lineRender.enabled = false;
		label.gameObject.SetActive(false);

		if (drawCrt != null)
			StopCoroutine(drawCrt);
	}
}
