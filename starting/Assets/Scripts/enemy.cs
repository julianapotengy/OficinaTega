using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
	Campodevisao campo;
	Transform my;
	Rigidbody2D body;
	public GameObject player; 
	Vector3 posicasa;
	Vector3 posicasaR;
	public Transform[] locais;
	private float Speed;
	private float timer;
	private bool Goto2,Goto3,Goto4,Goto5;

	void Start ()
	{
		posicasa = transform.position;
		posicasaR = transform.eulerAngles;
		campo = GetComponentInChildren<Campodevisao> ();
		cam = Camera.main;
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
		Speed = 0.4f;
		timer = 0;
		locais = GetComponentsInChildren<Transform> ();
		Goto2 = false;
		Goto3 = false;
		Goto4 = false;
		Goto5 = false;
		transform.DetachChildren ();
		locais [1].gameObject.transform.SetParent (transform);
		  
	}

	void Update ()
	{
		if (campo.viu)
		{
			Goto2 = false;
			Goto3 = false;
			Goto4 = false;
			Goto5 = false;
			timer += Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.red;	

				Vector2 posiplayer= player.transform.position;
				float AngleRad = Mathf.Atan2 (-posiplayer.x - -my.position.x, posiplayer.y - my.position.y);
				float angle = (180 / Mathf.PI) * AngleRad;
				
				body.rotation = angle;
			if (timer > 1)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,-9.2f);
				transform.Translate(Vector3.up * Speed);
			}
		}

		if (!campo.viu)
		{
			if(transform.position == locais[2].position)
			{
				Goto2 =false ; 
				Goto3 = true ;
				transform.Rotate(new Vector3(0, 0, -90));
			}
			else if(transform.position == locais[3].position)
			{
				Goto3 = false ; 
				Goto4 = true ;
				transform.Rotate(new Vector3(0, 0, -90));
			}
			else if(transform.position == locais[4].position)
			{
				Goto4 = false ; 
				Goto5 = true ;
				transform.Rotate(new Vector3(0, 0, -90));
			}
			else if(transform.position == locais[5].position)
			{
				Goto5= false ; 
				Goto2 = true ;
				transform.Rotate(new Vector3(0, 0, -90));
			}

			if( Goto3)
			{
				transform.position = Vector3.MoveTowards(transform.position,locais[3].position,0.2f);
			}
			if (Goto4)
			{
				transform.position = Vector3.MoveTowards(transform.position,locais[4].position,0.2f);
			}
			if (Goto5)
			{
				transform.position = Vector3.MoveTowards(transform.position,locais[5].position,0.2f);
			}
			if(Goto2)
			{
				transform.position = Vector3.MoveTowards(transform.position,locais[2].position,0.2f);
			}


		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (campo.viu)
		{
			if (other.gameObject.tag == "limite")
				{
					transform.position = posicasa;
				transform.rotation =Quaternion.Euler(posicasaR);
					campo.viu = false;
					
					timer = 0;
					GetComponent<SpriteRenderer>().color = Color.white;
				}
		}
	}
}
