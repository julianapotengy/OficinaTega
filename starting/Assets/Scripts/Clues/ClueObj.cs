using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClueObj : MonoBehaviour
{
	//private string[] clue = new string[3]{"Casa marrom","Casa laranja","Casa dourada"};
	private Text clueTx;
	public string DicaTx;

	void Start ()
	{
		clueTx = GameObject.Find ("Clue").GetComponent<Text> ();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			Clues.ColectedDicas.Add(DicaTx);
			
		}
	}
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTx.text = "Aperte i para abrir o caderno";

		}

	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTx.text = "";
			Destroy(gameObject);
		}
	}
}
