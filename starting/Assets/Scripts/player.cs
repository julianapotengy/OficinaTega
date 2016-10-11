using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	private PauseGame isPaused;
	public static bool caught = false;
	private GameObject tutorial;
	private Animator anim;
	
	private Rigidbody2D body;
	private float axisX, axisY;
	private int speed;

	private float activeZoom;
	private bool zoomOut = true;

	[HideInInspector] public float stamina, staminaCount;
	public Image staminaBar;
	public AudioClip breathing;
	public AudioClip SambaSound;
	public bool startsamba;
	private bool Canbreath, CanSamba, CatchMap, faceRight;
	public float medo;
	Camera Playermap;
	public bool medoShake;
	private bool medoactive;
	public Transform mainCamera;
	private float shakeDuration;
	private float shakeAmount;
	private float decreaseFactor;
	private Vector3 camPosition;
	private float lerp = 0;

	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
		caught = false;
	}

	void Start()
	{
		shakeDuration = 0;
		shakeAmount = 1f;
		decreaseFactor = 1;
		mainCamera = Camera.main.transform;
		medoactive = false;
		medoShake = false;
		CatchMap = false;
		Playermap = GameObject.Find ("CameraPlayer").GetComponent<Camera>();
		medo = 0; 
		startsamba = false; 
		CanSamba = true; 
		Canbreath = true; 
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

		speed = 15;
		stamina = 1;
		faceRight = true;
		anim = GetComponent<Animator> ();
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
		float move = Input.GetAxis("Horizontal");
		int realMove = (int)move;
		anim.SetInteger("speed", Mathf.Abs(realMove));
		if (move > 0 && !faceRight) Flip();
		else if (move < 0 && faceRight) Flip();
	}
	
	void Update ()
	{
		//if (!medoShake) {
			
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
		//}		
		if (!isPaused.paused)
		{	
			camPosition = mainCamera.position;
			//if (medo<= 25) mainCamera.rotation = Quaternion.Euler(Vector3.Lerp
			if (medo>=40)
			{
				if (!medoactive)
				StartCoroutine(medoshaking());
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
			if(PlayerPrefs.GetString("MODE") == "classic")
			{
				if (tutorial == null) 
					medo += Time.deltaTime/2;
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
				if (stamina > 0 && (axisX != 0 || axisY != 0))
					stamina -= 0.1f * Time.deltaTime;
			}
		}
	}

	void WalkAndRun()
	{
		axisX = Input.GetAxis ("Horizontal");
		axisY = Input.GetAxis ("Vertical");
		body.velocity = new Vector3(axisX * speed, axisY * speed, 0);

		if (axisY < 0)
			anim.SetBool("goDown", true);
		else anim.SetBool("goDown", false);
		if (axisY > 0)
			anim.SetBool("goUp", true);
		else anim.SetBool("goUp", false);
		if (tutorial != null) {
			lerp += Time.deltaTime/1.5f;
			tutorial.GetComponent<RectTransform>().localScale= Vector3.Lerp(tutorial.GetComponent<RectTransform>().localScale,new Vector3(1,1,1),lerp);
		
		}
		if (tutorial != null && Input.anyKey)
		{
			jumpTutorial();
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
	void OnCollisionEnter2D(Collision2D other){
	 if (other.gameObject.tag == "goldenHouse" || other.gameObject.tag == "housePremises") {
			GameObject.Find("Clue").GetComponent<Text>().text = "Descubra qual tecla apertar para entrar.";
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "housePremises" && Input.GetKeyDown(Clues.theKey))
		{
			medo+=10;
			shakeDuration =1f;
			GameObject.Find("Clue").GetComponent<Text>().text = "Essa não é sua casa, continue procurando. ";
		}
		else if (other.gameObject.tag == "goldenHouse" && Input.GetKeyDown(Clues.theKey))
		{
			Application.LoadLevel(2);
		}


	}
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "goldenHouse" || other.gameObject.tag == "housePremises") {
			GameObject.Find("Clue").GetComponent<Text>().text = "";
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "map")
		{
			if (PlayerPrefs.GetString ("DIFFICULTY") == "hard" || PlayerPrefs.GetString ("DIFFICULTY") == "medium")
			{
				GameObject.Find ("MapCam" + coll.gameObject.name).GetComponent<Camera> ().depth = 5;
				coll.GetComponent<SpriteRenderer> ().enabled = false; 
			}
			else
			{
				GameObject.Find ("Clue").GetComponent<Text> ().text = "Aperte B para abrir o mapa";
				CatchMap = true;
			}
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
				for (int i = 0; i < tempmaps.Length; i++)
				{
					Destroy(tempmaps[i]);
				}
				GameObject.Find	("Clue").GetComponent<Text>().text= "";
			}
		}
	}

	private void Flip()
	{
		faceRight = !faceRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
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

	public void jumpTutorial ()
	{
		Destroy (tutorial);
	}
	IEnumerator medoshaking()
	{
		medoactive = true;
		yield return new WaitForSeconds(5);
		medoShake = true;
		shakeDuration = 1f;
		yield return new WaitForSeconds(1);
		medoShake = false;
		medoactive = false;
	}
}
