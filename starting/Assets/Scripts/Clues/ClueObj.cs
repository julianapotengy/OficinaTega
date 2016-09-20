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
<<<<<<< HEAD

=======
>>>>>>> origin/master
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
		if (coll.gameObject.tag == "player")
		{
			stringClueTxt = Clues.clues2Show[0];
			Clues.cluesColected.Add(stringClueTxt);
			deleteAlert.showAlert = true;
<<<<<<< HEAD
			coll.gameObject.GetComponent<player>().fear = 0;
=======
			coll.gameObject.GetComponent<player>().medo= 0 ; 
>>>>>>> origin/master
			GameManager.Playsound(NotificationSound);
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTxt.text = "Aperte i para abrir o caderno";
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTxt.text = "";
			Clues.clues2Show.RemoveAt(0);
			Destroy(gameObject);
		}
	}
}
