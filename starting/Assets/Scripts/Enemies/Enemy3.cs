using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
	private FieldOfVision field;
	[HideInInspector] public bool once;
	private PauseGame isPaused;
	private PolyNavAgent Pagent;
	private GameObject player;

	private Vector3 originalPosition;
	private Vector3 originalPositionR;
	
	public Transform[] places2Walk;
	private bool goTo1, goTo2, goTo3, goTo4;
	private bool[] goTo = new bool[4];
	
	public Transform mainCamera;
	private float shakeDuration;
	private float shakeAmount;
	private float decreaseFactor;
	private Vector3 camPosition;

	private bool mascara; 
	private bool chegou;
	private int rand; 
	private float timer; 
	private GameObject susto; 
	private AudioClip sustosnd;

	void Start ()
	{
		sustosnd = Resources.Load ("Game/Susto") as AudioClip;
		mascara = false;
		susto = GameObject.Find ("susto");
		susto.SetActive (false);
		timer = 0; 
		chegou = false; 
		rand = Random.Range (2, places2Walk.Length);
		Pagent = GetComponent<PolyNavAgent> ();
		field = GetComponentInChildren<FieldOfVision> ();
		once = false;
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

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
		transform.position = places2Walk [Random.Range (2, places2Walk.Length)].position;

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
			if (mascara)
			{
				timer+=Time.deltaTime;
				if (timer <= 1f)susto.SetActive(true); 
				else{ susto.SetActive(false);
					timer = 0;
					mascara = false ; }
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
			Pagent.SetDestination(posiplayer);
			Pagent.maxSpeed = 20; 

			if (!field.leaved)
			{
				chegou = false ; 
				Debug.Log("hey");
				field.saw = false; 
				once = false; 
				GetComponent<SpriteRenderer>().color = Color.white;
				rand = Random.Range (2, places2Walk.Length);
			}
		}

		if (!field.saw)
		{
			Pagent.maxSpeed = 10; 
			if (!chegou)
			{
				Pagent.SetDestination (places2Walk [rand].position);
				if (Pagent.remainingDistance <= 0.4f)
					chegou = true; 
			}
			else
			{ 
				rand = Random.Range (2, places2Walk.Length);
				chegou = false;
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
				player.GetComponent<Player>().stamina /= 2;
				once = true;
				GameManager.Playsound(sustosnd);
				field.saw = false;
				mascara = true; 
				transform.position = originalPosition;
				transform.rotation = Quaternion.Euler(originalPositionR);
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}


	}
}
