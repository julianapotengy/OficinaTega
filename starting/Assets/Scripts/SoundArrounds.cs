using UnityEngine;
using System.Collections;

public class SoundArrounds : MonoBehaviour
{
	player player;

	void Start ()
	{
		player = GetComponentInParent<player> ();
	}

	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy")
		{		
			player.startsamba = true;
		}
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy")
		{			
			player.startsamba = true;
		}
	}
}
