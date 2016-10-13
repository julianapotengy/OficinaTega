using UnityEngine;
using System.Collections;

public class FieldOfVision : MonoBehaviour
{
	[HideInInspector] public bool saw;
	[HideInInspector] public bool leaved;
	private Enemy3 enemy3;
	private GameObject player;
	
	void Start ()
	{
		saw = false;
		leaved = false;
		enemy3 = gameObject.transform.parent.GetComponent<Enemy3>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update()
	{
		if (!saw && !leaved)
		{
			player.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			saw = true;
			leaved = true;
			player.GetComponent<SpriteRenderer> ().color = Color.cyan;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			leaved = true; 
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			leaved = false;
			enemy3.GetComponent<Enemy3>().once = false;
			player.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
