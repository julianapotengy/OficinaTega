using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject[] Enemies;

	void Start ()
	{
		for (int i = 0; i < 3; i++)
		{
			Instantiate (Enemies [0], new Vector3 (146,18,-11), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [1], new Vector3 (-40,-1,0), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [2], new Vector3 (216,110,0), Quaternion.Euler (0, 0, 0));
		}
	}

	void Update ()
	{
	
	}

	public static void Playsound(AudioClip clip)
	{ 
		AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
		audio.PlayOneShot (clip);
	}
}
