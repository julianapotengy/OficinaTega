using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundArrounds : MonoBehaviour {
	player Player ; 
	float time;
	GameObject compass;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		time = 0;
		rb = GetComponent<Rigidbody2D> ();
		Player = GetComponentInParent<player> ();
		compass = GameObject.Find ("bussola");
		compass.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		time++;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy"&& time>1) {		
			Player.startsamba= true ; 
			compass.SetActive (true);
		}

	}
	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy"&& time>1) {			
			Player.startsamba= true ;
			compass.SetActive (true);
			Physics2D.OverlapCircle(transform.position,148.3756f);

			var dir = coll.transform.position - compass.transform.FindChild("agulha").transform.position;
			dir.Normalize();
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			compass.transform.FindChild("agulha").transform.rotation = Quaternion.Euler(0,0,angle-90);
			Debug.Log (coll.gameObject.name);

		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		compass.SetActive (false);
	}


}
