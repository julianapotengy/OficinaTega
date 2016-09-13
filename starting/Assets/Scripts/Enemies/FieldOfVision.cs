using UnityEngine;
using System.Collections;

public class FieldOfVision : MonoBehaviour
{
	[HideInInspector] public bool saw;
	[HideInInspector] public bool leaved;
	private GameObject enemy3;
	private GameObject player;
	
	void Start ()
	{
		saw = false;
		leaved = false;
		enemy3 = GameObject.Find ("Enemy3(Clone)");
		player = GameObject.FindGameObjectWithTag ("player");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "player")
		{
			saw = true;
			leaved = true;
			player.GetComponent<SpriteRenderer> ().color = Color.cyan;

			AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
			audio.pitch = 1.5f;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "player")
		{
			leaved = true; 
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "player")
		{
			leaved = false;
			enemy3.GetComponent<Enemy3>().once = false;
			player.GetComponent<SpriteRenderer> ().color = Color.white;

			AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
			audio.pitch = 1;
		}
	}
}
