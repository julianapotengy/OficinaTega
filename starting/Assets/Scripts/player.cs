using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
	Campodevisao campo;
	private Camera cam;
	private Transform my;
	private Rigidbody2D body;

	private float Speed;

	private float activeZoom;
	private bool zoomOut = true;

	private float stamina, staminaconta ;
	public GameObject staminabar;

	void Awake ()
	{
		cam = Camera.main;
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
	}

	void Start()
	{
		stamina = 100; 
		campo = GameObject.Find ("enemy").GetComponentInChildren<Campodevisao> ();
		Speed = 15;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space) && zoomOut && stamina > 0)
		{
			Speed = 30;
			activeZoom = Time.time;
			zoomOut = false;
		}
		else if(!Input.GetKey(KeyCode.Space) && !zoomOut)
		{
			Speed = 15;
			activeZoom = Time.time;
			zoomOut = true;
		}
	}
	
	void Update ()
	{
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

		if (stamina <= 0)
		{
			zoomOut = true;
			Speed = 15; 		
		}
		staminaconta = (stamina/100f) * 2.45f;
		staminabar.transform.localScale = new Vector3(staminaconta,staminabar.transform.localScale.y,staminabar.transform.localScale.z);

		if (campo.viu)
			GetComponent<SpriteRenderer> ().color = Color.cyan;
		else
			GetComponent<SpriteRenderer> ().color = Color.white;

		body.velocity = new Vector3 (0, 0, 0);


		 if (Input.GetKey ("up"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,0));
			body.velocity = Vector3.up * Speed;
		}
		 if (Input.GetKey ("left"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,90));
			body.velocity = Vector3.left * Speed;
		}
		 if (Input.GetKey ("down"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,180));
			body.velocity = Vector3.down * Speed;
		}
		 if (Input.GetKey ("right"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,270));
			body.velocity = Vector3.right * Speed;
		}
		if (Input.GetKey ("up") && Input.GetKey ("left"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,45));
			body.velocity = new Vector3(-1,1,0) * Speed;
		}
		if (Input.GetKey ("up") && Input.GetKey ("right"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,315));
			body.velocity = new Vector3(1,1,0) * Speed;
		}
		if (Input.GetKey ("down") && Input.GetKey ("left"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,135));
			body.velocity = new Vector3(-1,-1,0) * Speed;
		}
		if (Input.GetKey ("down") && Input.GetKey ("right"))
		{
			transform.rotation = Quaternion.Euler(new Vector3 (0,0,225));
			body.velocity = new Vector3(1,-1,0) * Speed; 
		}

		if (zoomOut)
		{
			Camera.main.orthographicSize = Mathf.Lerp (7, 12, 5f * (Time.time - activeZoom));
			if (stamina < 100 && !Input.GetKey(KeyCode.Space))
				stamina +=10*Time.deltaTime;
		} 
		else
		{
			Camera.main.orthographicSize = Mathf.Lerp (12, 7, 5f * (Time.time - activeZoom));
			if (stamina > 0)
				stamina -= 10 * Time.deltaTime;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name.Equals ("enemy"))
		{
			Debug.Log ("gameover");
			Application.LoadLevel (0);
		}
	}
}
