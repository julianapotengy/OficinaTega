using UnityEngine;
using System.Collections;

public class Campodevisao : MonoBehaviour
{
	public bool viu = false;
	private GameObject player;
	
	void Start ()
	{

	}

	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "jogador")
			viu = true;
	}
}
