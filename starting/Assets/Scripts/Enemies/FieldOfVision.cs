using UnityEngine;
using System.Collections;

public class FieldOfVision : MonoBehaviour
{
	[HideInInspector] public bool saw;
	[HideInInspector] public bool leaved;
	private GameObject player;
	
	void Start ()
	{
		saw = false;
		leaved = false; 
	}

	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "player")
			saw = true;
			leaved = true; 
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
		}
	}
}
