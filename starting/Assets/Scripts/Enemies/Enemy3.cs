using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
	FieldOfVision field;
	private GameObject player;
	bool once; 
	Transform my; 
	Rigidbody2D body;

	void Start ()
	{
		once = false; 
		field = GetComponentInChildren<FieldOfVision> ();
		player = GameObject.FindGameObjectWithTag ("player");
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
	}

	void Update ()
	{
		WalkAndRun ();
	}

	void WalkAndRun()
	{
		if (field.saw && field.leaved && !once)
		{
			player.GetComponent<Player>().stamina /= 2;
			once = true; 
		}

		if (field.saw && field.leaved)
		{
			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;
		}

		if (!field.leaved)
		{
			field.saw = false ; 
			once = false; 
		}
	}
}
