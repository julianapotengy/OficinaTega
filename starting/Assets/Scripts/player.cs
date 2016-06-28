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

	void Awake ()
	{
		cam = Camera.main;
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
	}

	void Start()
	{
		campo = GameObject.Find ("enemy").GetComponentInChildren<Campodevisao> ();
		Speed = 0.2f;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space) && zoomOut)
		{
			Speed = 0.5f;
			activeZoom = Time.time;
			zoomOut = false;
		}
		else if(!Input.GetKey(KeyCode.Space) && !zoomOut)
		{
			Speed = 0.2f;
			activeZoom = Time.time;
			zoomOut = true;
		}
	}
	
	void Update ()
	{
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

		if (campo.viu)
			GetComponent<SpriteRenderer> ().color = Color.cyan;
		else
			GetComponent<SpriteRenderer> ().color = Color.white;

		if(Input.GetKey(KeyCode.W))
			transform.Translate(Vector3.up * Speed);
		if(Input.GetKey(KeyCode.S))
			transform.Translate(Vector3.down * Speed);

		if (zoomOut)
			Camera.main.orthographicSize = Mathf.Lerp(7, 12, 5f * (Time.time - activeZoom));
		else
			Camera.main.orthographicSize = Mathf.Lerp(12, 7, 5f * (Time.time - activeZoom));

		float camDis = cam.transform.position.x - my.position.x;

		Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
		
		float AngleRad = Mathf.Atan2 (-mouse.x - -my.position.x, mouse.y - my.position.y);
		float angle = (180 / Mathf.PI) * AngleRad;
		
		body.rotation = angle;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name.Equals("enemy"))
			Debug.Log("gameover");

	}
}
