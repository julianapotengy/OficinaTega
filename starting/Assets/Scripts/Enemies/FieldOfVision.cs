using UnityEngine;
using System.Collections;

public class FieldOfVision : MonoBehaviour
{
	[HideInInspector] public bool saw;
	[HideInInspector] public bool leaved;
	private GameObject enemy3;
	
	void Start ()
	{
		saw = false;
		leaved = false;
		enemy3 = GameObject.Find ("Enemy3");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "player")
		{
			saw = true;
			leaved = true;
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
		}
	}
}
