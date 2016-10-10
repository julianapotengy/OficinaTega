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
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			stringClueTxt = Clues.clues2Show[0];
			Clues.cluesColected.Add(stringClueTxt);
			deleteAlert.showAlert = true;
			coll.gameObject.GetComponent<player>().medo = 0; 
			GameManager.Playsound(NotificationSound);
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			clueTxt.text = Clues.clues2Show[0];
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			clueTxt.text = "";
			Clues.clues2Show.RemoveAt(0);
			Destroy(gameObject);
		}
	}
}
