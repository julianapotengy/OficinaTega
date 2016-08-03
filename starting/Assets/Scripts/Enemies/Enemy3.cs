using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
	private FieldOfVision field;
	[HideInInspector] public bool once;
	private PauseGame isPaused;

	private Transform my; 
	private Rigidbody2D body;
	private GameObject player;

	private Vector3 originalPosition;
	private Vector3 originalPositionR;
	
	public Transform[] places2Walk;
	private bool goTo1, goTo2, goTo3, goTo4;
	private bool[] goTo = new bool[4];

	private float speed;

	public Transform mainCamera;
	private float shakeDuration;
	private float shakeAmount;
	private float decreaseFactor;
	private Vector3 camPosition;

	void Start ()
	{
		field = GetComponentInChildren<FieldOfVision> ();
		once = false;
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

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
		places2Walk[1].gameObject.transform.SetParent(transform);

		speed = 0.4f;

		shakeDuration = 0;
		shakeAmount = 0.7f;
		decreaseFactor = 1;

	}

	void Update ()
	{
		if (!isPaused.paused)
		{
			WalkAndRun ();
			camPosition = mainCamera.position;
			
			if (shakeDuration > 0)
			{
				mainCamera.localPosition = camPosition + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				mainCamera.localPosition = camPosition;
			}
		}
	}

	void WalkAndRun()
	{
		if (field.saw)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
			Vector2 posiplayer = player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x + my.position.x, posiplayer.y - my.position.y);
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
				goTo[2] = true;
			}
			else if (transform.position == places2Walk[4].position)
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[3] = true;
			}
			else if (transform.position == places2Walk[5].position)
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
			else if (goTo[2])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[2] = true;
				transform.position = Vector3.MoveTowards(transform.position, places2Walk[4].position, 0.2f);
				
				Vector2 posiplayer = places2Walk[4].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x + my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
			else if (goTo[3])
			{
				for (int i = 0; i < goTo.Length; i++)
				{
					goTo[i] = false; 
				}
				goTo[3] = true;
				transform.position = Vector3.MoveTowards(transform.position, places2Walk[5].position, 0.2f);
				
				Vector2 posiplayer = places2Walk[5].position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x + my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				body.rotation = angle;
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (field.saw && field.leaved && !once)
		{
			if(other.gameObject.name.Equals("Player"))
			{
				shakeDuration = 0.5f;
				player.GetComponent<Player>().stamina /= 2;
				once = true;
				field.saw = false;

				transform.position = originalPosition;
				transform.rotation = Quaternion.Euler(originalPositionR);
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}

		if (!field.leaved)
		{
			field.saw = false; 
			once = false; 
		}
	}
}
