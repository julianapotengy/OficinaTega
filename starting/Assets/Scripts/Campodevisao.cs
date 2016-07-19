using UnityEngine;
using System.Collections;

public class Campodevisao : MonoBehaviour
{
	public bool viu = false;
	private GameObject player;
	public bool Saiu ; 
	
	void Start ()
	{
		viu = false;
		Saiu = false; 

	}

	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador")
			viu = true;
			Saiu = true; 
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador") {
			Saiu = true ; 
		}

	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador") {
			Saiu = false ; 
		}
		
	}
}
