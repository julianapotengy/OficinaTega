using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
	Campodevisao campo ; 
	Camera cam;
	Transform my;
	Rigidbody2D body;
	public GameObject player; 
	float Speed = 0.4f; 
	Vector3 posicasa; 
	void Start ()
	{
		posicasa = transform.position;
		campo = GetComponentInChildren<Campodevisao> ();
		cam = Camera.main;
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
	}

	void Update ()
	{
		if (campo.viu) {

			// Distance from camera to object.  We need this to get the proper calculation.
		
			Vector2 posiplayer= player.transform.position;
			float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
			float angle = (180 / Mathf.PI) * AngleRad;
			
			body.rotation = angle;
			transform.Translate(Vector3.up * Speed);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (campo.viu) {
		if (other.gameObject.tag == "limite"){
				transform.position = posicasa;
				campo.viu = false ; 
			}

		}
	}
}
