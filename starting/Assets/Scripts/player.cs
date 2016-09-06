using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private FieldOfVision field;
	private PauseGame isPaused;

	private Rigidbody2D body;
	private float speed, axisX, axisY;

	public Sprite[] faces = new Sprite[3];
	private SpriteRenderer sp;

	private float activeZoom;
	private bool zoomOut = true;

	[HideInInspector] public float stamina, staminaCount;
	public GameObject staminaBar;
	
	private GameObject tutorial;

	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
	}

	void Start()
	{
		field = GameObject.Find ("Enemy(Clone)").GetComponentInChildren<FieldOfVision> ();
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
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

		if (!isPaused.paused)
		{
			WalkAndRun ();
			if (stamina <= 0)
			{
				zoomOut = true;
				speed = 15; 		
			}
			
			staminaCount = (stamina / 100f) * 2.45f;
			staminaBar.transform.localScale = new Vector3 (staminaCount, staminaBar.transform.localScale.y, staminaBar.transform.localScale.z);

			if (stamina < 25)
				staminaBar.GetComponent<SpriteRenderer> ().color = Color.red;
			else
				staminaBar.GetComponent<SpriteRenderer> ().color = Color.white;
			if (zoomOut)
			{
				Camera.main.orthographicSize = Mathf.Lerp (7, 12, 5f * (Time.time - activeZoom));
				if (stamina < 100 && !Input.GetKey (KeyCode.Space) ) {
					if (stamina <= 50)
						stamina += 4 * Time.deltaTime;
					if (stamina >= 50)
						stamina += 8 * Time.deltaTime;
				}
			}
			else
			{
				Camera.main.orthographicSize = Mathf.Lerp (12, 7, 5f * (Time.time - activeZoom));
				if (stamina > 0)
					stamina -= 10 * Time.deltaTime;
			}
		}
	}

	void WalkAndRun()
	{
		if (field.saw)
			GetComponent<SpriteRenderer> ().color = Color.cyan;
		else
			GetComponent<SpriteRenderer> ().color = Color.white;

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
			sp.sprite= faces[0];
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
			Application.LoadLevel(5);
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
}
