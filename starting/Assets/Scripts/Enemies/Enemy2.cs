using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
	FieldOfVision field;
	private Transform my;
	private Rigidbody2D body;
	private GameObject player;

	private Vector3 posicasa;
	private Vector3 posicasaR;

	public Transform[] places2Walk;
	private bool Goto1, Goto2;
	private bool[] GoTo = new bool[2];

	public Transform[] lugares = new Transform[4];
	private bool[] playerGoTo = new bool[4];

	private float Speed;

	void Start ()
	{
		field = GetComponentInChildren<FieldOfVision> ();
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("player");

		posicasa = transform.position;
		posicasaR = transform.eulerAngles;

		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < GoTo.Length; i++)
		{
			GoTo[i] = false; 
		}
		
		transform.DetachChildren ();
		places2Walk [1].gameObject.transform.SetParent (transform);

		Speed = 0.4f;
	}

	void Update ()
	{
		if (field.saw)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;

			transform.position = new Vector3(transform.position.x, transform.position.y, -9.2f);
			transform.Translate(Vector3.up * Speed);
		}

		if (!field.saw)
		{
			if (transform.position == places2Walk[2].position)
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false;
				}
				GoTo[1] = true;
			}
			else if (transform.position == places2Walk[3].position)
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				GoTo[0] = true;
			}

			if (GoTo[0])
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false;
				}
				GoTo[0]= true;
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[2].position, 0.2f);
				
				Vector2 posiplayer = places2Walk[2].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x + my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (GoTo[1])
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				GoTo[1] = true;
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[3].position, 0.2f);
				
				Vector2 posiplayer = places2Walk[3].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x + my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (field.saw)
		{
			if (other.gameObject.name.Equals ("Player"))
			{
				int rand = Random.Range(0, playerGoTo.Length);
				playerGoTo[rand] = true;
				
				if (playerGoTo[0])
				{
					player.transform.position = lugares[0].transform.position;
				}
				else if (playerGoTo[1])
				{
					player.transform.position = lugares[1].transform.position;
				}
				else if (playerGoTo[2])
				{
					player.transform.position = lugares[2].transform.position;
				}
				else if (playerGoTo[3])
				{
					player.transform.position = lugares[3].transform.position;
				}

				transform.position = posicasa;
				transform.rotation =Quaternion.Euler(posicasaR);
				field.saw = false;
				
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (field.saw)
		{
			if (other.gameObject.tag == "limit")
			{
				transform.position = posicasa;
				transform.rotation =Quaternion.Euler(posicasaR);
				field.saw = false;
				
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}
}
