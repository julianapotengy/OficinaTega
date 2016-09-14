using UnityEngine;
using System.Collections;

public class SoundArrounds : MonoBehaviour {
	player Player ; 
	// Use this for initialization
	void Start () {
		Player = GetComponentInParent<player> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy") {		
			Player.startsamba= true ; }
	}
	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy") {			
			Player.startsamba= true ;
		}
	}

}
