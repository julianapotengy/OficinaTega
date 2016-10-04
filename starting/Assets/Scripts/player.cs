using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	private PauseGame isPaused;
	public static bool caught = false;
	private GameObject tutorial;
	
	private Rigidbody2D body;
	private float speed, axisX, axisY;

	public Sprite[] faces = new Sprite[4];
	private SpriteRenderer sp;

	private float activeZoom;
	private bool zoomOut = true;

	[HideInInspector] public float stamina, staminaCount;
	public Image staminaBar;
	public AudioClip breathing;
	public AudioClip SambaSound;
	public bool startsamba;
	private bool Canbreath, CanSamba, CatchMap;
	public float medo = 0;
	Camera Playermap;

	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
		caught = false ;
	}

	void Start()
	{
		CatchMap = false;
		Playermap = GameObject.Find ("CameraPlayer").GetComponent<Camera>();
		medo = 0; 
		startsamba = false; 
		CanSamba = true; 
		Canbreath = true; 
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

		speed = 15;
		sp = GetComponent<SpriteRenderer> ();
		stamina = 1;

		tutorial = GameObject.Find ("Tutorial");
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
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

		if (!isPaused.paused)
		{
			if(PlayerPrefs.GetString("MODE") == "classic")
			{
				medo += Time.deltaTime/2;
				if (medo >= 100)
				{
					Application.LoadLevel(3);
				}
			}

			if (startsamba)
			{
				if (CanSamba)
					StartCoroutine(Samba());
			}
			WalkAndRun ();
			if (stamina <= 0)
			{
				zoomOut = true;
				speed = 15; 		
			}
			
			staminaCount = (stamina / 1f) * 1.2f;
			staminaBar.GetComponent<Image>().fillAmount = staminaCount;

			if (stamina < 0.25f)
			{
				if (Canbreath)
					StartCoroutine(respirar());
				staminaBar.GetComponent<Image> ().color = Color.red;
			}
			else
				staminaBar.GetComponent<Image> ().color = Color.white;
			if (zoomOut)
			{
				Camera.main.orthographicSize = Mathf.Lerp (12, 20, 5f * (Time.time - activeZoom));
				if (stamina < 1 && !Input.GetKey (KeyCode.Space))
				{
					if (stamina <= 0.5f && axisX ==0 && axisY == 0)
						stamina += 0.04f * Time.deltaTime;
					else if (stamina <= 0.5f)
					{
						stamina += 0.02f * Time.deltaTime;
					}
					if (stamina >= 0.5f && axisX == 0 && axisY == 0)
						stamina += 0.08f * Time.deltaTime;
					else if (stamina >= 0.5f)
						stamina += 0.06f * Time.deltaTime;
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
		if (Input.GetKeyDown(KeyCode.B) && CatchMap)
		{
			Playermap.depth = 20;
			Time.timeScale = 0;
		}
		if (Input.GetKeyUp(KeyCode.B) && CatchMap)
		{
			Playermap.depth = -20;
			Time.timeScale = 1;
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "goldenHouse" && Input.GetKeyDown(Clues.theKey))
		{
			Application.LoadLevel(2);
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "map")
		if (PlayerPrefs.GetString ("DIFFICULTY") == "hard" || PlayerPrefs.GetString ("DIFFICULTY") == "medium")
		{
			GameObject.Find ("MapCam" + coll.gameObject.name).GetComponent<Camera> ().depth = 5;
			coll.GetComponent<SpriteRenderer> ().enabled = false; 
		} 
		else
		{
			GameObject.Find	("Clue").GetComponent<Text>().text= "Aperte B para abrir o mapa";
			CatchMap = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{

		if (coll.gameObject.tag == "map")
		{
			GameObject.Find("MapCam" + coll.gameObject.name).GetComponent<Camera>().depth = -12;
			coll.GetComponent<SpriteRenderer>().enabled = true; 
			if (PlayerPrefs.GetString("DIFFICULTY") == "easy")
			{
				GameObject[] tempmaps= GameObject.FindGameObjectsWithTag("map");
				for (int i = 0 ; i<tempmaps.Length;i++)
				{
					Destroy(tempmaps[i]);
				}
				GameObject.Find	("Clue").GetComponent<Text>().text= "";
			}
		}
	}

	IEnumerator respirar()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "respirando" && Canbreath)
		{
			GameManager.Playsound (breathing);
			Canbreath = false; 
		} 
		yield return new WaitForSeconds(breathing.length);
		Canbreath = true; 
	}

	IEnumerator Samba()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "sambasound" && CanSamba)
		{
			GameManager.Playsound (SambaSound);
			CanSamba = false; 
			startsamba = false; 
		} 
		yield return new WaitForSeconds(SambaSound.length);
		CanSamba = true;
		startsamba = false; 
	}
}
