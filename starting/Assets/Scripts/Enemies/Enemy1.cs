using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour
{
	private FieldOfVision field;
	private PauseGame isPaused;

	private Transform my;
	private Rigidbody2D body;
	private GameObject player;

	public Transform[] places2Walk;
	private bool goTo2, goTo3, goTo4, goTo5;
	private bool[] goTo = new bool[4];

	private float timer;

	private PolyNavAgent Pagent;
	int rand; 
	bool chegou;

	void Start ()
	{
		Pagent = GetComponent<PolyNavAgent> ();
		rand = Random.Range (2, places2Walk.Length);
		chegou = false; 

		field = GetComponentInChildren<FieldOfVision> ();
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
		player = GameObject.Find ("Player");

		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < goTo.Length; i++)
		{
			goTo[i] = false; 
		}

		transform.DetachChildren ();
		places2Walk[1].gameObject.transform.SetParent (transform);
		transform.position = places2Walk [Random.Range (2, places2Walk.Length)].position;
		timer = 0;
	}

	void Update ()
	{
		if (!isPaused.paused)
		{
			WalkAndRun();
		}
	}

	void WalkAndRun()
	{
		if (field.saw)
		{
			timer += Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.red;

			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x + my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;

			if (timer > 1)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, -9.2f);
				Pagent.SetDestination(posiplayer);
				Pagent.maxSpeed = 25; 
			}
		}
		
		if (!field.saw)
		{
			Pagent.maxSpeed = 10; 
			if (!chegou)
			{
				Pagent.SetDestination (places2Walk [rand].position);
				if (Pagent.remainingDistance <= 0.45f)
					chegou = true; 
			}
			else
			{ 
				rand = Random.Range (2, places2Walk.Length);
				chegou = false; 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (field.saw)
		{
			if (other.gameObject.tag == "camLimit")
				{
					field.saw = false;
					rand = Random.Range (2, places2Walk.Length);
					chegou = false; 
					timer = 0;
					GetComponent<SpriteRenderer>().color = Color.white;
				}
		}
	}
}
