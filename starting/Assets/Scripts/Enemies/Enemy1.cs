using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour
{
	private FieldOfVision field;
	private Transform my;
	private Rigidbody2D body;
	private GameObject player;

	private Vector3 originalPosition;
	private Vector3 originalPositionR;

	public Transform[] places2Walk;
	private bool goTo2, goTo3, goTo4, goTo5;
	private bool[] goTo = new bool[4];

	private float speed;
	private float timer;

	void Start ()
	{
		field = GetComponentInChildren<FieldOfVision> ();
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
		player = GameObject.Find ("Player");

		originalPosition = transform.position;
		originalPositionR = transform.eulerAngles;

		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < goTo.Length; i++)
		{
			goTo[i] = false; 
		}

		transform.DetachChildren ();
		places2Walk[1].gameObject.transform.SetParent (transform);

		speed = 0.4f;
		timer = 0;
	}

	void Update ()
	{
		WalkAndRun();
	}

	void WalkAndRun()
	{
		if (field.saw)
		{
			for (int i = 0; i < goTo.Length; i++)
			{
				goTo[i] = false; 
			}

			timer += Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.red;

			Vector2 playerPosition = player.transform.position;
			float AngleRad = Mathf.Atan2 (-playerPosition.x + my.position.x, playerPosition.y - my.position.y);

			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;

			if (timer > 1)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, -9.2f);
				transform.Translate(Vector3.up * speed);
			}
		}
		
		if (!field.saw)
		{
			if (transform.position == places2Walk[2].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false;
				}
				int rand = Random.Range(0,goTo.Length);
				goTo[rand] = true;
			}
			else if (transform.position == places2Walk[3].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				int rand = Random.Range(0,goTo.Length);
				goTo[rand] = true;
			}
			else if (transform.position == places2Walk[4].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				int rand = Random.Range(0,goTo.Length);
				goTo[rand] = true;
			}
			else if (transform.position == places2Walk[5].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				int rand = Random.Range(0,goTo.Length);
				goTo[rand] = true;
			}
			
			if (goTo[1])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false;
				}
				goTo[1]= true;
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[3].position, 0.2f);

				Vector2 posiplayer = places2Walk[3].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (goTo[2])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[2] = true;
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[4].position, 0.2f);

				Vector2 posiplayer = places2Walk[4].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (goTo[3])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[3]= true;
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[5].position, 0.2f);

				Vector2 posiplayer = places2Walk[5].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (goTo[0])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[0] = true; 
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[2].position, 0.2f);

				Vector2 posiplayer = places2Walk[2].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (field.saw)
		{
			if (other.gameObject.tag == "camLimit")
				{
					transform.position = originalPosition;
					transform.rotation = Quaternion.Euler(originalPositionR);
					field.saw = false;
					
					timer = 0;
					GetComponent<SpriteRenderer>().color = Color.white;
				}
		}
	}
}
