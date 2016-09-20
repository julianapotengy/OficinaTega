using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject[] Enemies;
<<<<<<< HEAD
	private GameObject player;
	public Image fearImg; 

	void Start ()
	{
		fearImg = GameObject.Find ("Fear").GetComponent<Image> ();
		player = GameObject.FindGameObjectWithTag ("player");
=======
	GameObject player;
	public Image medoimg; 
	void Start ()
	{
		medoimg = GameObject.Find ("Medo").GetComponent<Image> ();
		player = GameObject.Find ("Player");
		//player.GetComponent<player>().medo;
>>>>>>> origin/master
		for (int i = 0; i < 3; i++)
		{
			Instantiate (Enemies [0], new Vector3 (146,18,-11), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [1], new Vector3 (-40,-1,0), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [2], new Vector3 (216,110,0), Quaternion.Euler (0, 0, 0));
		}
	}

	void Update ()
	{
<<<<<<< HEAD
		fearImg.fillAmount = player.GetComponent<player> ().fear / 100f;
=======
		medoimg.fillAmount = player.GetComponent<player> ().medo / 100f;
>>>>>>> origin/master
	}

	public static void Playsound(AudioClip clip)
	{ 
		AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
		audio.PlayOneShot (clip);
	}
}
