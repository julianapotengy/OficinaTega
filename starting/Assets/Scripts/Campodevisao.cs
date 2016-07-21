using UnityEngine;
using System.Collections;

public class Campodevisao : MonoBehaviour
{
	[HideInInspector] public bool viu;
	[HideInInspector] public bool saiu;
	private GameObject player;
	
	void Start ()
	{
		viu = false;
		saiu = false; 

	}

	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador")
			viu = true;
			saiu = true; 
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador")
		{
			saiu = true ; 
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador")
		{
			saiu = false ; 
		}
	}
}
