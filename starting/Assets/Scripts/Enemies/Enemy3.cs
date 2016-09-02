using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
	private FieldOfVision field;
	[HideInInspector] public bool once;
	private PauseGame isPaused;
	
	private GameObject player;

	private Vector3 originalPosition;
	private Vector3 originalPositionR;

	private PolyNavAgent pagent;
	public Transform[] places2Walk;
	private bool goTo1, goTo2, goTo3, goTo4;
	private bool[] goTo = new bool[4];
	
	public Transform mainCamera;
	private float shakeDuration;
	private float shakeAmount;
	private float decreaseFactor;
	private Vector3 camPosition;
	
	private bool mask; 
	private bool arrived;
	private int rand; 
	private float timer; 
	private GameObject shock; 
	private AudioClip shockSound;

	void Start ()
	{
		field = GetComponentInChildren<FieldOfVision> ();
		once = false;
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

		player = GameObject.FindGameObjectWithTag ("player");

		originalPosition = transform.position;
		originalPositionR = transform.eulerAngles;

		pagent = GetComponent<PolyNavAgent> ();
		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < goTo.Length; i++)
		{
			goTo[i] = false; 
		}
		
		transform.DetachChildren ();
		places2Walk[1].gameObject.transform.SetParent(transform);
		transform.position = places2Walk [Random.Range (2, places2Walk.Length)].position;

		shakeDuration = 0;
		shakeAmount = 0.7f;
		decreaseFactor = 1;

		mask = false;
		arrived = false; 
		rand = Random.Range (2, places2Walk.Length);
		timer = 0;
		shock = GameObject.Find ("susto");
		shock.SetActive (false);
		shockSound = Resources.Load ("Game/Susto") as AudioClip;
	}

	void Update ()
	{
		if (!isPaused.paused)
		{
			WalkAndRun ();
			camPosition = mainCamera.position;
			if (mask)
			{
				timer+=Time.deltaTime;
				if (timer <= 1f)
					shock.SetActive(true); 
				else
				{
					shock.SetActive(false);
					timer = 0;
					mask = false;
				}
			}
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
			transform.position = new Vector3(transform.position.x, transform.position.y, -9.2f);
			pagent.SetDestination(posiplayer);
			pagent.maxSpeed = 20; 

			if (!field.leaved)
			{
				arrived = false;
				field.saw = false; 
				once = false; 
				GetComponent<SpriteRenderer>().color = Color.white;
				rand = Random.Range (2, places2Walk.Length);
			}
		}

		if (!field.saw)
		{
			pagent.maxSpeed = 10; 
			if (!arrived)
			{
				pagent.SetDestination (places2Walk [rand].position);
				if (pagent.remainingDistance <= 0.4f)
					arrived = true; 
			}
			else
			{ 
				rand = Random.Range (2, places2Walk.Length);
				arrived = false;
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (field.saw && field.leaved && !once)
		{
			if(other.gameObject.name.Equals("Player"))
			{
				shakeDuration = 1.5f;
				player.GetComponent<Player>().stamina =(player.GetComponent<Player>().stamina >25)? 25:10 ;
				once = true;
				GameManager.Playsound(shockSound);
				field.saw = false;
				mask = true; 
				transform.position = originalPosition;
				transform.rotation = Quaternion.Euler(originalPositionR);
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}
}
