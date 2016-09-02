using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public static void Playsound(AudioClip clip)
	{ 
		AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
		audio.PlayOneShot (clip);
	}
}
