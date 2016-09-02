using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClueObj : MonoBehaviour
{
	//private string[] clue = new string[3]{"Casa marrom","Casa laranja","Casa dourada"};
	private Text clueTxt;
	public string stringClueTxt;

	void Start ()
	{
		clueTxt = GameObject.Find ("Clue").GetComponent<Text> ();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			Clues.cluesColected.Add(stringClueTxt);
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
			Destroy(gameObject);
		}
	}
}
