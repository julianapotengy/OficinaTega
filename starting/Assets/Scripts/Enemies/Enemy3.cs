using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
	private FieldOfVision field;
	[HideInInspector] public bool once;
	private PauseGame isPaused;
	private GameObject player;
	private GameObject tutorial;
	private Vector3 originalPosition, originalPositionR;

	private PolyNavAgent pagent;
	public Transform[] places2Walk;
	private bool goTo1, goTo2, goTo3, goTo4;
	private bool[] goTo = new bool[4];
	
	public Transform mainCamera;
	private float shakeDuration, shakeAmount, decreaseFactor, timer;
	private Vector3 camPosition;
	
	private bool mask, arrived;
	private int rand;
	public static GameObject shock; 
	public AudioClip shockSound;

	public Vector3[] Places;
	public GameObject[]temp;
	public static bool canShock;
	Animator anim;

	void Awake()
	{
		field = GetComponentInChildren<FieldOfVision> ();
		anim = GetComponent<Animator> ();
	}

	void Start ()
	{
		canShock = true;
		mainCamera = Camera.main.transform;
		temp = GameObject.FindGameObjectsWithTag ("MovE3");
		Places =  new Vector3[temp.Length]; 
		for (int i = 0; i < temp.Length; i++)
		{
			Places[i] = temp[i].transform.position;
		}

		once = false;
		tutorial = GameObject.Find ("Tutorial");
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();
		player = GameObject.FindGameObjectWithTag ("Player");

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
		transform.position = Places[Random.Range (0,Places.Length)];

		shakeDuration = 0;
		shakeAmount = 0.7f;
		decreaseFactor = 1;

		mask = false;
		arrived = false; 
		rand = Random.Range (0,Places.Length);
		timer = 0;
		shock = GameObject.Find ("susto");
		StartCoroutine (wait ());
	}

	void Update ()
	{
		if (!isPaused.paused && tutorial == null)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			WalkAndRun ();
			for (int i = 0; i < temp.Length; i++)
			{
				Destroy(temp[i].gameObject);
			}
			camPosition = mainCamera.position;
			if (mask)
			{
				timer += Time.deltaTime;
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
				mainCamera.position = camPosition + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				mainCamera.position = camPosition;
			}
		}
	}

	void WalkAndRun()
	{
		Vector2 posiplayer = player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		if (player.GetComponent<player> ().medo >= 100)
			pagent.SetDestination (posiplayer);
		if (field.saw && !player.GetComponent<player>().onPraça)
		{
			pagent.SetDestination(posiplayer);
			pagent.maxSpeed = 12; 

			if (!field.leaved)
			{
				arrived = false;
				field.saw = false; 
				once = false; 
				rand = Random.Range (0, Places.Length);
			}
		}

		if (!field.saw)
		{
			pagent.maxSpeed = 7; 
			if (!arrived)
			{
				pagent.SetDestination (Places [rand]);
				if (pagent.remainingDistance <= 0.4f)
					arrived = true; 
			}
			else
			{ 
				rand = Random.Range (0, Places.Length);
				arrived = false;
			}
		}
		anim.SetFloat ("DirectionX",Mathf.Abs( pagent.movingDirection.x));
		anim.SetFloat("DirectionY",pagent.movingDirection.y);
		if (pagent.movingDirection.x < 0)
		{
			transform.localScale = new Vector3(-2.5f,transform.localScale.y,transform.localScale.z);
		}
		else transform.localScale = new Vector3(2.5f,transform.localScale.y,transform.localScale.z);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (field.saw && field.leaved && !once)
		{
			if(other.gameObject.name.Equals("Player"))
			{
				shakeDuration = 1.5f;
				player.GetComponent<player>().stamina =(player.GetComponent<player>().stamina > 0.25f)? 0.25f:0.1f;
				once = true;
				if (canShock)
					StartCoroutine(sound2());
				field.saw = false;
				mask = true; 
				transform.position = originalPosition;
				transform.rotation = Quaternion.Euler(originalPositionR);
				other.gameObject.GetComponent<player>().medo += 20f;
			}
		}
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds (0.001f);
			shock.SetActive (false);
	}

	IEnumerator sound2()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "Breathing" && canShock)
		{
			GameManager.Playsound (shockSound);
			canShock = false; 
		} 
		yield return new WaitForSeconds(shockSound.length);
		canShock = true; 
	}
}
