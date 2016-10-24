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
		player = GameObject.FindGameObjectWithTag ("Player");
		if(gameObject.transform.parent.name.Equals("Enemy3(Clone)"))
			enemy3 = gameObject.transform.parent.GetComponent<Enemy3>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			saw = true;
			leaved = true;
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
			if(gameObject.transform.parent.name.Equals("Enemy3(Clone)"))
				enemy3.GetComponent<Enemy3>().once = false;
		}
	}
}
