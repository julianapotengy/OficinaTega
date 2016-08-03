using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private FieldOfVision field;
	private PauseGame isPaused;

	private Rigidbody2D body;
	private float speed;

	public Sprite[] faces = new Sprite[3];
	private SpriteRenderer sp;

	private float activeZoom;
	private bool zoomOut = true;

	[HideInInspector] public float stamina, staminaCount;
	public GameObject staminaBar;

	void Awake ()
	{
		body = GetComponent <Rigidbody2D> ();
	}

	void Start()
	{
		field = GameObject.Find ("Enemy").GetComponentInChildren<FieldOfVision> ();
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();

		speed = 15;
		sp = GetComponent<SpriteRenderer> ();
		stamina = 100;
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

		if(!isPaused.paused)
		{
			WalkAndRun ();
			if (stamina <= 0)
			{
				zoomOut = true;
				speed = 15; 		
			}
			
			staminaCount = (stamina/100f) * 2.45f;
			staminaBar.transform.localScale = new Vector3(staminaCount,staminaBar.transform.localScale.y,staminaBar.transform.localScale.z);
			
			if (zoomOut)
			{
				Camera.main.orthographicSize = Mathf.Lerp (7, 12, 5f * (Time.time - activeZoom));
				if (stamina < 100 && !Input.GetKey(KeyCode.Space))
					stamina +=10 * Time.deltaTime;
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
		
		body.velocity = new Vector3 (0, 0, 0);
		
		if (Input.GetKey ("up"))
		{
			body.velocity = Vector3.up * speed;
		}
		if (Input.GetKey ("left"))
		{
			sp.sprite = faces[2];
			body.velocity = Vector3.left * speed;
		}
		if (Input.GetKey ("down"))
		{
			sp.sprite = faces[0];
			body.velocity = Vector3.down * speed;
		}
		if (Input.GetKey ("right"))
		{
			sp.sprite = faces[1];
			body.velocity = Vector3.right * speed;
		}
		if (Input.GetKey ("up") && Input.GetKey ("left"))
		{
			body.velocity = new Vector3(-1,1,0) * speed;
		}
		if (Input.GetKey ("up") && Input.GetKey ("right"))
		{
			body.velocity = new Vector3(1,1,0) * speed;
		}
		if (Input.GetKey ("down") && Input.GetKey ("left"))
		{
			body.velocity = new Vector3(-1,-1,0) * speed;
		}
		if (Input.GetKey ("down") && Input.GetKey ("right"))
		{
			body.velocity = new Vector3(1,-1,0) * speed; 
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name.Equals ("Enemy"))
		{
			Application.LoadLevel(5);
		}
		if (other.gameObject.tag == "goldenHouse")
		{
			Application.LoadLevel(4);
		}
	}
}
