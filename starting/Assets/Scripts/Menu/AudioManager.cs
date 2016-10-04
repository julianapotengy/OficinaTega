using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{
	private AudioSource audio;
	private static AudioManager instance = null;
	
	public static AudioManager Instance
	{
		get { return instance; }
	}
	
	void Awake ()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		} else instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Start ()
	{
		audio = GetComponent<AudioSource> ();
		if (!audio.playOnAwake)
		{
			audio.Play();
		}
	}
	
	void Update ()
	{
		if (!audio.isPlaying)
		{
			audio.Play();
		}
	}
}
