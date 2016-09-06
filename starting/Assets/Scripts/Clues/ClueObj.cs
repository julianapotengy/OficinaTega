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
			Clues.cluesColected.Add(stringClueTxt);
			deleteAlert.showAlert = true;
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
			clueTxt.text = "Pressione i para abrir o caderno";
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTxt.text = "";
			Destroy(gameObject);
		}
	}
}
