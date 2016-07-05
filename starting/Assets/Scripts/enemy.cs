using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
	Campodevisao campo;
	private Camera cam;
	private Transform my;
	private Rigidbody2D body;
	private GameObject player;

	private Vector3 posicasa;
	private Vector3 posicasaR;

	public Transform[] locais;
	private bool Goto2,Goto3,Goto4,Goto5;
	private bool[] GoTo = new bool[4];
	private float Speed;
	private float timer;

	void Start ()
	{
		campo = GetComponentInChildren<Campodevisao> ();
		cam = Camera.main;
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
		player = GameObject.Find ("Player");

		posicasa = transform.position;
		posicasaR = transform.eulerAngles;

		locais = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < GoTo.Length; i++)
		{
			GoTo[i] = false; 
		}

		transform.DetachChildren ();
		locais [1].gameObject.transform.SetParent (transform);

		Speed = 0.4f;
		timer = 0;
	}

	void Update ()
	{
		WalkAndRun();
	}

	void WalkAndRun()
	{
		if (campo.viu)
		{
			for (int i = 0; i < GoTo.Length; i++)
			{
				GoTo[i] = false; 
			}

			timer += Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.red;			
			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;

			if (timer > 1)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,-9.2f);
				transform.Translate(Vector3.up * Speed);
			}
		}
		
		if (!campo.viu)
		{
			if (transform.position == locais [2].position)
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false;
				}
				int rand = Random.Range(0,GoTo.Length);
				Debug.Log (rand);
				GoTo[rand] = true;
			}
			else if (transform.position == locais [3].position)
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				int rand = Random.Range(0,GoTo.Length);
				Debug.Log (rand);
				GoTo[rand] = true;
			}
			else if (transform.position == locais [4].position)
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				int rand = Random.Range(0,GoTo.Length);
				Debug.Log (rand);
				GoTo[rand] = true;
			}
			else if (transform.position == locais [5].position)
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				int rand = Random.Range(0,GoTo.Length);
				Debug.Log (rand);
				GoTo[rand] = true;
			}
			
			if (GoTo[1])
			{
				for (int i = 0; i < GoTo.Length; i++)
				{
					GoTo[i] = false;
				}
				GoTo[1]= true;
				transform.position = Vector3.MoveTowards (transform.position, locais [3].position, 0.2f);

				Vector2 posiplayer = locais [3].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (GoTo[2])
			{
				for (int i = 0; i<GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				GoTo[2] = true;
				transform.position = Vector3.MoveTowards (transform.position, locais [4].position, 0.2f);

				Vector2 posiplayer = locais [4].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (GoTo[3])
			{
				for (int i = 0; i<GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				GoTo[3]= true;
				transform.position = Vector3.MoveTowards (transform.position, locais [5].position, 0.2f);

				Vector2 posiplayer = locais [5].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (GoTo[0])
			{
				for (int i = 0; i<GoTo.Length; i++)
				{
					GoTo[i] = false; 
				}
				GoTo[0] = true; 
				transform.position = Vector3.MoveTowards (transform.position, locais [2].position, 0.2f);

				Vector2 posiplayer = locais[2].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (campo.viu)
		{
			if (other.gameObject.tag == "limite")
				{
					transform.position = posicasa;
					transform.rotation =Quaternion.Euler(posicasaR);
					campo.viu = false;
					
					timer = 0;
					GetComponent<SpriteRenderer>().color = Color.white;
				}
		}
	}
}
