using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	private PauseGame isPaused;
	public static bool caught = false;
	public float fear;
	
	private Rigidbody2D body;
	private float speed, axisX, axisY;

	public Sprite[] faces = new Sprite[4];
	private SpriteRenderer sp;

	private float activeZoom;
	private bool zoomOut = true;

	[HideInInspector] public float stamina, staminaCount;
	public Image staminaBar;
	private GameObject tutorial;

	public AudioClip breathing;
	public AudioClip SambaSound;
	public bool startsamba;
<<<<<<< HEAD
	bool canBreath;
	bool canSamba;

=======
	bool Canbreath;
	bool CanSamba;
	public float medo = 0 ; 
>>>>>>> origin/master
	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
		caught = false;
	}

	void Start()
	{
<<<<<<< HEAD
		startsamba = false;
		canSamba = true;
		canBreath = true;

=======
		medo = 0; 
		startsamba = false; 
		CanSamba = true; 
		Canbreath = true; 
>>>>>>> origin/master
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();
		tutorial = GameObject.Find ("TutorialPanel");

		fear = 0;
		speed = 15;
		sp = GetComponent<SpriteRenderer> ();
		stamina = 1;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space) && zoomOut && stamina > 0)
		{
			speed = 30;
			activeZoom = Time.time;
			zoomOut = false;
		}
		else if(!Input.GetKey(KeyCode.Space) && !zoomOut)
		{
			speed = 15;
			activeZoom = Time.time;
			zoomOut = true;
		}
	}
	
	void Update ()
	{
<<<<<<< HEAD
=======
		medo += Time.deltaTime;
>>>>>>> origin/master
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
		fear += Time.deltaTime;
		if (!isPaused.paused)
		{
			if (startsamba)
			{
				StartCoroutine(Samba());
			}
			WalkAndRun ();
			if (stamina <= 0)
			{
				zoomOut = true;
				speed = 15; 		
			}
			
			staminaCount = (stamina / 1) * 1.2f;
			staminaBar.GetComponent<Image>().fillAmount = staminaCount;

			if (stamina < 0.25f)
			{
				StartCoroutine(respirar());
				staminaBar.GetComponent<Image>().color = Color.red;
			}
			else staminaBar.GetComponent<Image>().color = Color.white;
			if (zoomOut)
			{
				Camera.main.orthographicSize = Mathf.Lerp (12, 20, 5f * (Time.time - activeZoom));
				if (stamina < 1 && !Input.GetKey (KeyCode.Space))
				{
					if (stamina <= 0.5f)
						stamina += 0.04f * Time.deltaTime;
					if (stamina >= 0.5f)
						stamina += 0.08f * Time.deltaTime;
				}
			}
			else
			{
				Camera.main.orthographicSize = Mathf.Lerp (20, 12, 5f * (Time.time - activeZoom));
				if (stamina > 0 && (axisX !=0 || axisY !=0))
					stamina -= 0.1f * Time.deltaTime;
			}
		}
	}

	void WalkAndRun()
	{
		axisX = Input.GetAxis ("Horizontal");
		axisY = Input.GetAxis ("Vertical");
		body.velocity = new Vector3(axisX * speed, axisY * speed, 0);

		if (axisX < 0)
		{
			sp.sprite = faces [2];
			Destroy(tutorial); 
		}
		if (axisX > 0)
		{
			sp.sprite = faces [1];
			Destroy(tutorial); 
		}
		if (axisY < 0)
		{
			sp.sprite = faces[0];
			Destroy(tutorial); 
		}
		if (axisY > 0)
		{
			sp.sprite = faces[3];
			Destroy(tutorial);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "goldenHouse")
		{
			Application.LoadLevel(2);
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "map")
		{
			GameObject.Find("MapCam" + coll.gameObject.name).GetComponent<Camera>().depth = 5;
			coll.GetComponent<SpriteRenderer>().enabled = false; 
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "map")
		{
			GameObject.Find("MapCam" + coll.gameObject.name).GetComponent<Camera>().depth = -12;
			coll.GetComponent<SpriteRenderer>().enabled = true; 
		}
	}

	IEnumerator respirar()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "respirando" && canBreath)
		{
			GameManager.Playsound (breathing);
			canBreath = false; 
		} 
		yield return new WaitForSeconds(breathing.length);
		canBreath = true; 
	}

	IEnumerator Samba()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "sambasound" && canSamba)
		{
			GameManager.Playsound (SambaSound);
			canSamba = false; 
			startsamba = false; 
		} 
		yield return new WaitForSeconds(SambaSound.length);
		canSamba = true;
		startsamba = false; 
	}
}
