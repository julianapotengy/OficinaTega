using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClueObj : MonoBehaviour
{
	private Text clueTxt;
	public string stringClueTxt;
	private Clues deleteAlert;
	public GameObject alert;
	public AudioClip NotificationSound;

	void Start ()
	{
		clueTxt = GameObject.Find ("Clue").GetComponent<Text> ();
		alert.SetActive (false);
		deleteAlert = GameObject.Find ("GameManager").GetComponent<Clues> ();
	}

	void Update()
	{
		if (deleteAlert.showClue)
			alert.SetActive(false);
	
		if(Time.timeScale != 0)
			transform.position = new Vector2 (transform.position.x, transform.position.y+ (-0.05f+ Mathf.PingPong (Time.time/4, 0.10f)));
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name.Equals("HandsCollider"))
		{
			stringClueTxt = Clues.clues2Show[0];
			Clues.cluesColected.Add(stringClueTxt);
			deleteAlert.showAlert = true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<player>().medo = 0;
			GameManager.Playsound(NotificationSound);
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.name.Equals("HandsCollider"))
			clueTxt.text = Clues.clues2Show[0];
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.name.Equals("HandsCollider"))
		{
			clueTxt.text = "";
			Clues.clues2Show.RemoveAt(0);
			Destroy(gameObject);
		}
	}
}
