using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component replaced shader's material texture and color (marked as PerRendererData in shader) in runtime

public class RenderData : MonoBehaviour {

	public Color materialColor;
	public Texture materialTexture;

	private MaterialPropertyBlock m_PropertyBlock ;
	private Renderer myRenderer;

	void Start()
	{
		myRenderer = GetComponentInChildren<Renderer> ();
		m_PropertyBlock = new MaterialPropertyBlock ();
	}
	
	void Update()
	{
		m_PropertyBlock.SetColor ("_Color", materialColor);
		if (materialTexture != null)
			m_PropertyBlock.SetTexture ("_MainTex", materialTexture);

		myRenderer.SetPropertyBlock (m_PropertyBlock);
	}
}
