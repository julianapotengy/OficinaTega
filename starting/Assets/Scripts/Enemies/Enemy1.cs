using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour
{
	private FieldOfVision field;
	private PauseGame isPaused;
	private GameObject tutorial;
	private GameObject player;

	public Transform[] places2Walk;
	private bool goTo2, goTo3, goTo4, goTo5;
	private bool[] goTo = new bool[4];

	private float timer;

	private PolyNavAgent pagent;
	private int rand;
	private bool arrived;
	public Vector3[] Places;
	public GameObject[]temp;
	public static bool canBeath;
	Animator anim;
	public Vector2 direction;

	void Awake()
	{
		field = GetComponentInChildren<FieldOfVision> ();
		anim = GetComponent<Animator> ();
	}

	void Start ()
	{
		canBeath = true;
		temp = GameObject.FindGameObjectsWithTag ("MovE1");
		Places =  new Vector3[temp.Length]; 
		for (int i = 0; i < temp.Length; i++)
		{
			Places[i] = temp[i].transform.position;
		}

		pagent = GetComponent<PolyNavAgent> ();
		rand = Random.Range (0, Places.Length);
		arrived = false; 
		tutorial = GameObject.Find ("Tutorial");
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < goTo.Length; i++)
		{
			goTo[i] = false; 
		}

		transform.DetachChildren ();
		places2Walk[1].gameObject.transform.SetParent (transform);
		places2Walk[2].gameObject.transform.SetParent (transform);
		transform.position = Places [Random.Range (0, Places.Length)];
		timer = 0;
	}

	void Update ()
	{
		if (!isPaused.paused && tutorial == null)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			WalkAndRun();
			for (int i = 0; i< temp.Length; i++)
			{
				Destroy(temp[i].gameObject);
			}
		}
	}

	void WalkAndRun()
	{
		Vector2 posiplayer = player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		if (player.GetComponent<player> ().medo >= 100)
		{
			pagent.SetDestination (posiplayer);
			direction = posiplayer;
		}

        if (field.saw && !player.GetComponent<player>().onPraça)
		{
			timer += Time.deltaTime;
			direction = posiplayer;
			if (timer > 1)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				pagent.SetDestination(posiplayer);
				pagent.maxSpeed = 10;
			}
		}
		
		if (!field.saw)
		{
			pagent.maxSpeed = 5; 
			if (!arrived)
			{
				pagent.SetDestination (Places [rand]);
				direction = Places [rand];
				if (pagent.remainingDistance <= 0.45f)
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
		if (pagent.movingDirection.x < 0) {
			transform.localScale = new Vector3(-2.5f,transform.localScale.y,transform.localScale.z);
		}
		else transform.localScale = new Vector3(2.5f,transform.localScale.y,transform.localScale.z);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.transform.parent == null && other.tag.Equals("Player"))
		{
			Application.LoadLevel(3);
		}
		if (field.saw)
		{
			if (other.gameObject.tag == "camLimit")
			{
				field.saw = false;
				rand = Random.Range (0,Places.Length);
				arrived = false; 
				timer = 0;
			}
		}
	}
}
