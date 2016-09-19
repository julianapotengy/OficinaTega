using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
	private PauseGame isPaused;
	public static bool caught = false ;
	
	private Rigidbody2D body;
	private float speed, axisX, axisY;

	public Sprite[] faces = new Sprite[4];
	private SpriteRenderer sp;

	private float activeZoom;
	private bool zoomOut = true;

	[HideInInspector] public float stamina, staminaCount;
	public GameObject staminaBar;
	public AudioClip breathing;
	public AudioClip SambaSound; 
	private GameObject tutorial;
	public bool startsamba;
	bool Canbreath;
	bool CanSamba;
	public float medo = 0 ; 
	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
		caught = false ;
	}

	void Start()
	{
		medo = 0; 
		startsamba = false; 
		CanSamba = true; 
		Canbreath = true; 
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

		speed = 15;
		sp = GetComponent<SpriteRenderer> ();
		stamina = 100;

		tutorial = GameObject.Find ("TutorialPanel");
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
		medo += Time.deltaTime;
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

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
			
			staminaCount = (stamina / 100f) * 2.45f;
			staminaBar.transform.localScale = new Vector3 (staminaCount, staminaBar.transform.localScale.y, staminaBar.transform.localScale.z);

			if (stamina < 25){
				StartCoroutine(respirar());
				staminaBar.GetComponent<SpriteRenderer> ().color = Color.red;
			}
			else
				staminaBar.GetComponent<SpriteRenderer> ().color = Color.white;
			if (zoomOut)
			{
				Camera.main.orthographicSize = Mathf.Lerp (12, 20, 5f * (Time.time - activeZoom));
				if (stamina < 100 && !Input.GetKey (KeyCode.Space) ) {
					if (stamina <= 50)
						stamina += 4 * Time.deltaTime;
					if (stamina >= 50)
						stamina += 8 * Time.deltaTime;
				}
			}
			else
			{
				Camera.main.orthographicSize = Mathf.Lerp (20, 12, 5f * (Time.time - activeZoom));
				if (stamina > 0 && (axisX !=0 || axisY !=0))
					stamina -= 10 * Time.deltaTime;
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
			Application.LoadLevel(4);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name.Equals ("Enemy(Clone)"))
		{

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
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "respirando" && Canbreath) {
			GameManager.Playsound (breathing);
			Canbreath = false ; 
		} 
		yield return new WaitForSeconds(breathing.length);
		Canbreath = true; 
	}
	IEnumerator Samba()
	{
		if (Object.FindObjectOfType <AudioSource> ().clip.name != "sambasound" && CanSamba) {
			GameManager.Playsound (SambaSound);
			CanSamba = false ; 
			startsamba = false ; 
		} 
		yield return new WaitForSeconds(SambaSound.length);
		CanSamba = true;
		startsamba = false; 
	}

		


}
