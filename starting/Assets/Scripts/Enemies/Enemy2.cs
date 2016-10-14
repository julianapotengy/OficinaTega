using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
	private FieldOfVision field;
	private PauseGame isPaused;
	private GameObject Player;
	private GameObject tutorial;
	private Vector3 originalPosition;
	private Vector3 originalPositionR;

	private PolyNavAgent pagent;
	public Transform[] places2Walk;
	private bool goTo1, GoTo2;
	private bool[] goTo = new bool[2];

	public GameObject[] places = new GameObject[4];
	private bool[] playerGoTo = new bool[4];

	private bool fadeIn;
	private float fadeNum;

	public AudioClip shock;
	private int rand;
	private bool arrived;
	public static bool canShock;
	public Vector3[] Places;
	public GameObject[]temp;

	void Awake()
	{
		field = GetComponentInChildren<FieldOfVision> ();
	}

	void Start ()
	{ 
		canShock = true;
		temp = GameObject.FindGameObjectsWithTag ("MovE2");
		Places = new Vector3[temp.Length]; 
		for (int i=0; i<temp.Length; i++)
		{
			Places[i] = temp[i].transform.position;
		}
		places = GameObject.FindGameObjectsWithTag ("Places") ;
		pagent = GetComponent<PolyNavAgent> ();
		rand = Random.Range (0, Places.Length);
		arrived = false;
		tutorial = GameObject.Find ("Tutorial");
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();
		Player = GameObject.FindGameObjectWithTag ("Player");

		originalPosition = transform.position;
		originalPositionR = transform.eulerAngles;

		places2Walk = GetComponentsInChildren<Transform> ();
		for (int i = 0; i < goTo.Length; i++)
		{
			goTo[i] = false; 
		}

		transform.DetachChildren ();
		places2Walk[1].gameObject.transform.SetParent(transform);
		transform.position = Places[Random.Range (0, Places.Length)];

		fadeNum = 0;
		fadeIn = false;
	}

	void Update ()
	{
		if (!isPaused.paused && tutorial == null)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			FadeIn();
			WalkAndRun();
			for (int i=0; i < temp.Length; i++)
			{
				Destroy(temp[i].gameObject);
			}
		}
	}

	void FadeIn()
	{
		if (fadeIn)
		{
			player.caught = true; 
			Debug.Log("Aqui");
			fadeNum += 5;
			GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.r,
			                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.g,
			                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.b,
			                                                                           fadeNum/255);
			Player.SetActive(false);
			GetComponent<SpriteRenderer>().sortingOrder = -2;
			GameObject.Find("Stamina").GetComponent<Image>().enabled = false;
			GameObject.Find("OrangeStamina").GetComponent<Image>().enabled = false;
			
			if(this.fadeNum > 255)
			{
				int rand = Random.Range(0, playerGoTo.Length);
				playerGoTo[rand] = true;
				Player.transform.position = places[rand].transform.position;
				
				transform.position = originalPosition;
				transform.rotation = Quaternion.Euler(originalPositionR);
				field.saw = false;
				fadeIn = false;
				fadeNum = 0;
				
				Player.SetActive(true);
				GameObject.Find("Stamina").GetComponent<Image>().enabled = true;
				GameObject.Find("OrangeStamina").GetComponent<Image>().enabled = true;
				GetComponent<SpriteRenderer>().sortingOrder = 2; 
				GetComponent<SpriteRenderer>().enabled = true ; 
				GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.r,
				                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.g,
				                                                                           GameObject.Find("FadeIn").GetComponent<SpriteRenderer>().color.b,
				                                                                           fadeNum / 255);
			}
		}
	}

	void WalkAndRun()
	{
		Vector2 posiplayer = Player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		if (Player.GetComponent<player> ().medo >= 100)
			pagent.SetDestination (posiplayer);
		if (player.caught)
		{
			StartCoroutine(stop());
		}
		if (field.saw)
		{
			GetComponent<SpriteRenderer>().color = Color.red;

			pagent.SetDestination(posiplayer);
			pagent.maxSpeed = 20;

			if (!field.leaved)
			{
				field.saw = false;
				rand = Random.Range (0, Places.Length);
				arrived = false;
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
		
		if (!field.saw)
		{
			pagent.maxSpeed = 10; 

			if (!arrived)
			{
				pagent.SetDestination (Places[rand]);
				if (pagent.remainingDistance <= 0.4f)
					arrived = true; 
			}
			else
			{ 
				rand = Random.Range (0, Places.Length);
				arrived = false; 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (field.saw)
		{
			if (other.gameObject.name.Equals ("Player"))
			{
				fadeIn = true;
				AudioSource audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
				audio.pitch = 1f;
				if (canShock) StartCoroutine(sound2());
				GetComponent<SpriteRenderer>().color = Color.white;
				player.caught = true; 
				other.gameObject.GetComponent<player>().medo += 20f;
			}
		}
	}
	 
	IEnumerator stop()
	{
		yield return new WaitForEndOfFrame ();
		{
			field.saw = false; 
			GetComponent<SpriteRenderer>().color = Color.white;
			rand = Random.Range (0, Places.Length);
			arrived = false;
			player.caught = false;
		}
	}

	IEnumerator sound2()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "respirando" && canShock)
		{
			GameManager.Playsound (shock);
			canShock = false; 
		} 
		yield return new WaitForSeconds(shock.length);
		canShock = true; 
	}


}
