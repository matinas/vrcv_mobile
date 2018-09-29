using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ManipulableObjectAudio : MonoBehaviour {

	public float waitTime;

	public int playCount = 1;

	private AudioSource audioSrc;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
	}

	void Start()
	{
		StartCoroutine(PlayAudioCoroutine());
	}

	IEnumerator PlayAudioCoroutine()
	{
		while (playCount > 0)
		{
			yield return new WaitForSeconds(waitTime);
			
			audioSrc.Play();
			playCount--;
		}
	}
}
