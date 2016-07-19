using UnityEngine;
using System.Collections;

public class enemy2 : MonoBehaviour
{
	Campodevisao campo;
	private GameObject player;
	bool Umavez; 
	Transform my; 
	Rigidbody2D body;
	void Start ()
	{
		Umavez = false; 
		campo = GetComponentInChildren<Campodevisao> ();
		player = GameObject.FindGameObjectWithTag ("jogador");
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
	}

	void Update ()
	{
		WalkAndRun ();
	}

	void WalkAndRun()
	{
		if (campo.viu && campo.Saiu&& !Umavez)
		{
			player.GetComponent<player>().stamina /= 2;
			Umavez = true ; 


		}if (campo.viu && campo.Saiu) {
			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;
		}
		if (!campo.Saiu) {
			campo.viu = false ; 
			Umavez=false; 
		}
	}
}
