using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
	private FieldOfVision field;
	private GameObject player;
	private bool once; 
	private Transform my; 
	private Rigidbody2D body;

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
			Vector2 playerPosition = player.transform.position;
			float AngleRad = Mathf.Atan2 (-playerPosition.x + my.position.x, playerPosition.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;
		}

		if (!field.leaved)
		{
			field.saw = false; 
			once = false; 
		}
	}
}
