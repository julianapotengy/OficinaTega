using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject[] Enemies;
	private GameObject player;
	public Image medoimg;
	GameObject fadein;
	bool startfadin;
	void Start ()
	{
		startfadin = true;
		fadein = GameObject.Find ("FadeIn");
		fadein.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 1);

		medoimg = GameObject.Find ("Medo").GetComponent<Image> ();
		player = GameObject.FindGameObjectWithTag ("player");

		for (int i = 0; i < 3; i++)
		{
			Instantiate (Enemies [0], new Vector3 (146,18,-11), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [1], new Vector3 (-40,-1,0), Quaternion.Euler (0, 0, 0));
			Instantiate (Enemies [2], new Vector3 (216,110,0), Quaternion.Euler (0, 0, 0));
		}
	}

	void Update ()
	{
		if (startfadin) {
			fadein.GetComponent<SpriteRenderer> ().color -= new Color(0,0,0,0.5f*Time.deltaTime);
			if (fadein.GetComponent<SpriteRenderer> ().color.a <=0)
				startfadin= false;
		}
		if(PlayerPrefs.GetString("MODE") == "classic")
			medoimg.fillAmount = player.GetComponent<player> ().medo / 100f;
	}

	public static void Playsound(AudioClip clip)
	{ 
		AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
		audio.PlayOneShot (clip);
	}
	public static void ButtonPaperClip()
	{
		AudioClip sound = Resources.Load ("Sounds/ButtonPressedPaper") as AudioClip;
		Playsound (sound);
	}
	public static void ButtonMenuClip()
	{
		AudioClip sound = Resources.Load ("Sounds/MenuButtonPressed") as AudioClip;
		Playsound (sound);
	}
	public static void ButtonHighlightedClip()
	{
		AudioClip sound = Resources.Load ("Sounds/ButtonHighlighted") as AudioClip;
		Playsound (sound);
	}

}
