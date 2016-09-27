using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject[] Enemies;
	private GameObject player;
	public Image medoimg;

	public GameObject[] showLevels2Choose;
	[HideInInspector] public string level;

	void Start ()
	{
		medoimg = GameObject.Find ("Medo").GetComponent<Image> ();
		player = GameObject.FindGameObjectWithTag ("player");

		for (int i = 0; i < 3; i++)
		{
			Instantiate (Enemies [0], new Vector3 (146,18,-11), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [1], new Vector3 (-40,-1,0), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [2], new Vector3 (216,110,0), Quaternion.Euler (0, 0, 0));
		}

		showLevels2Choose = GameObject.FindGameObjectsWithTag ("ShowLevels2Choose");
		Time.timeScale = 0;
	}

	void Update ()
	{
		medoimg.fillAmount = player.GetComponent<player> ().medo / 100f;
	}

	public static void Playsound(AudioClip clip)
	{ 
		AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
		audio.PlayOneShot (clip);
	}

	public void Easy()
	{
		level = "easy";
		Time.timeScale = 1;
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(false);
		}
	}

	public void Medium()
	{
		level = "medium";
		Time.timeScale = 1;
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(false);
		}
	}

	public void Hard()
	{
		level = "hard";
		Time.timeScale = 1;
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(false);
		}
	}
}
