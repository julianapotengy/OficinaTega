using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	private PauseGame isPaused;
	public static bool caught = false;
	private GameObject tutorial;
	private Animator anim;
    public bool onPraça;
	private Rigidbody2D body;
	private float axisX, axisY, activeZoom, shakeDuration, shakeAmount, decreaseFactor;
	private int speed;

	private bool zoomOut = true;
	public float stamina;
	[HideInInspector]public float staminaCount;
	public Image staminaBar;
	public AudioClip breathing, SambaSound, normalSong;
	public bool startsamba;
	private bool Canbreath, CanSamba, CatchMap, faceRight;
	public float medo;
	Camera Playermap;
	public bool medoShake;
	public Transform mainCamera;
	private Vector3 camPosition;
	private float lerp = 0;
	[SerializeField] Animation animation;
	[SerializeField] Image boyImg;
	[SerializeField] Sprite[] reactions;
	[SerializeField] GameObject uis;

	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
		caught = false;
	}

	void Start()
	{
        onPraça = false;
		shakeDuration = 0;
		shakeAmount = 1f;
		decreaseFactor = 1;
		mainCamera = Camera.main.transform;
		medoShake = false;
		CatchMap = false;
		Playermap = GameObject.Find ("CameraPlayer").GetComponent<Camera>();
		medo = 0; 
		startsamba = false; 
		CanSamba = true; 
		Canbreath = true; 
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();
		speed = 10;
		stamina = 1;
		faceRight = true;
		anim = GetComponent<Animator> ();
		tutorial = GameObject.Find ("Tutorial");
		uis.GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
	}

	void FixedUpdate()
	{
		if(!isPaused.paused)
		{
			if ((Input.GetKey(KeyCode.Space) ||Input.GetKey(KeyCode.LeftShift)) && zoomOut && stamina > 0)
			{
				speed = 14;
				activeZoom = Time.time;
				zoomOut = false;
				anim.speed = 1.5f;
			}
			else if(!(Input.GetKey(KeyCode.Space) ||Input.GetKey(KeyCode.LeftShift)) && !zoomOut)
			{
				speed = 10;
				activeZoom = Time.time;
				zoomOut = true;
				anim.speed = 1;
			}
			float move = Input.GetAxis("Horizontal");
			float realMove = move;
			anim.SetFloat("speed", Mathf.Abs(realMove));
			if (move > 0 && !faceRight)
				Flip();
			else if (move < 0 && faceRight)
				Flip();
		}
	}
	
	void Update ()
	{
        mainCamera.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, mainCamera.position.z);	
		camPosition = mainCamera.position;
		if (!isPaused.paused)
		{	
			faceUpdate();
			if (shakeDuration > 0)
			{
				mainCamera.position = camPosition + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0;
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
				{
					if (Camera.main.GetComponent <AudioSource> ().clip.name != "BatebolaNear")
					{
						Camera.main.GetComponent <AudioSource> ().clip = SambaSound;
						Camera.main.GetComponent <AudioSource> ().Play();
					}
					CanSamba = false;
				}
			}
			else if(!startsamba)
			{
				CanSamba = true;
				if (Camera.main.GetComponent <AudioSource> ().clip.name == "BatebolaNear")
				{
					Camera.main.GetComponent <AudioSource> ().clip = normalSong;
					Camera.main.GetComponent <AudioSource> ().Play();
				}
			}
			WalkAndRun ();
			if (stamina <= 0)
			{
				zoomOut = true;
				speed = 10; 		
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
				Camera.main.orthographicSize = Mathf.Lerp (7, 9, 5f * (Time.time - activeZoom));
				if (stamina < 1 && !(Input.GetKey(KeyCode.Space) ||Input.GetKey(KeyCode.LeftShift)))
				{
					if (stamina <= 0.5f && axisX == 0 && axisY == 0)
						stamina += 0.04f * Time.deltaTime;
					else if (stamina <= 0.5f)
					{
						stamina += 0.02f * Time.deltaTime;
					}
					if (stamina >= 0.5f && axisX == 0 && axisY == 0)
						stamina += 0.06f * Time.deltaTime;
					else if (stamina >= 0.5f)
						stamina += 0.04f * Time.deltaTime;
				}
			}
			else
			{
				Camera.main.orthographicSize = Mathf.Lerp (9, 7, 5f * (Time.time - activeZoom));
				if (stamina > 0 && (axisX != 0 || axisY != 0))
					stamina -= 0.1f * Time.deltaTime;
			}
		}
	}

	void WalkAndRun()
	{
		axisX = Input.GetAxis ("Horizontal");
		axisY = Input.GetAxis ("Vertical");

		body.velocity = new Vector2(axisX * speed, axisY * speed);

		if (axisY < 0)
			anim.SetBool("goDown", true);
		else anim.SetBool("goDown", false);
		if (axisY > 0)
			anim.SetBool("goUp", true);
		else anim.SetBool("goUp", false);

		if (tutorial != null)
		{
			lerp += Time.deltaTime/1.5f;
			tutorial.GetComponent<RectTransform>().localScale = Vector3.Lerp(tutorial.GetComponent<RectTransform>().localScale,new Vector3(1,1,1),lerp);
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

	void OnCollisionEnter2D(Collision2D other)
	{
	 if (other.gameObject.tag == "goldenHouse" || other.gameObject.tag == "housePremises")
		{
			GameObject.Find("Clue").GetComponent<Text>().text = "Descubra qual tecla apertar para entrar";
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "housePremises" && Input.GetKeyDown(Clues.theKey))
		{
			medo += 10;
			shakeDuration = 1;
			GameObject.Find("Clue").GetComponent<Text>().text = "Essa não é sua casa, continue procurando";
		}
		else if (other.gameObject.tag == "goldenHouse" && Input.GetKeyDown(Clues.theKey))
		{
			Application.LoadLevel(2);
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "goldenHouse" || other.gameObject.tag == "housePremises")
		{
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
        if (coll.gameObject.tag == "Praça")
        {
            onPraça = true;
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
        if (coll.gameObject.tag == "Praça")
        {
            onPraça = false;
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
		if (Camera.main.GetComponent <AudioSource> ().clip.name != "respirando" && Canbreath)
		{
			GameManager.Playsound (breathing);
			Canbreath = false; 
		} 
		yield return new WaitForSeconds(breathing.length);
		Canbreath = true; 
	}

	public void jumpTutorial ()
	{
		Destroy (tutorial);
		uis.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
	}

	private void faceUpdate()
	{
		if (medo > 75)
			boyImg.sprite = reactions [3];
		else if (medo>50)
			boyImg.sprite = reactions [2];
		else if (medo>25)
			boyImg.sprite = reactions [1];
		else
			boyImg.sprite = reactions [0];
	}
}
