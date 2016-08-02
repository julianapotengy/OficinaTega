using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
	private FieldOfVision field;
	private Transform my;
	private Rigidbody2D body;
	private GameObject player;

	private Vector3 originalPosition;
	private Vector3 originalPositionR;

	public Transform[] places2Walk;
	private bool goTo1, GoTo2;
	private bool[] goTo = new bool[2];

	public Transform[] places = new Transform[4];
	private bool[] playerGoTo = new bool[4];

	private float speed;

	private bool fadeIn;
	private float fadeNum;

	void Start ()
	{
		fadeNum = 0;
		fadeIn = false; 
		field = GetComponentInChildren<FieldOfVision> ();
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("player");

		originalPosition = transform.position;
		originalPositionR = transform.eulerAngles;

		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < goTo.Length; i++)
		{
			goTo[i] = false; 
		}
		
		transform.DetachChildren ();
		places2Walk [1].gameObject.transform.SetParent (transform);

		speed = 0.4f;
	}

	void Update ()
	{
		if (fadeIn)
		{
			fadeNum += 5;
			GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.r,
			                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.g,
			                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.b,
			                                                                           fadeNum/255);
			player.SetActive(false);
			GetComponent<SpriteRenderer>().enabled = false;

			GameObject.Find("Stamina").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("OrangeStamina").GetComponent<SpriteRenderer>().enabled = false;
			
			if(fadeNum > 255)
			{
				int rand = Random.Range(0, playerGoTo.Length);
				playerGoTo[rand] = true;
				player.transform.position = places[rand].transform.position;
				
				transform.position = originalPosition;
				transform.rotation = Quaternion.Euler(originalPositionR);
				field.saw = false;
				fadeIn = false;
				fadeNum = 0;

				player.SetActive(true);
				GameObject.Find("Stamina").GetComponent<SpriteRenderer>().enabled = true;
				GameObject.Find("OrangeStamina").GetComponent<SpriteRenderer>().enabled = true;

				GetComponent<SpriteRenderer>().enabled = true ; 
				GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.r,
				                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.g,
				                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.b,
				                                                                           fadeNum / 255);
			}

		
		}
		if (field.saw)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			body.rotation = angle;

			transform.position = new Vector3(transform.position.x, transform.position.y, -9.2f);
			transform.Translate(Vector3.up * speed);
		}

		if (!field.saw)
		{
			if (transform.position == places2Walk[2].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false;
				}
				goTo[1] = true;
			}
			else if (transform.position == places2Walk[3].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[0] = true;
			}

			if (goTo[0])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false;
				}
				goTo[0]= true;
				transform.position = Vector3.MoveTowards (transform.position, places2Walk[2].position, 0.2f);
				
				Vector2 playerPosition = places2Walk[2].position;
				float AngleRad = Mathf.Atan2 (-playerPosition.x + my.position.x, playerPosition.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (goTo[1])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[1] = true;
				transform.position = Vector3.MoveTowards(transform.position, places2Walk[3].position, 0.2f);
				
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
				fadeIn = true;
				GetComponent<SpriteRenderer>().color = Color.white;
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
				
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}
}
