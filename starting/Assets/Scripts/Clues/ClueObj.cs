using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClueObj : MonoBehaviour
{
	private string[] clue = new string[3]{"Casa marrom","Casa laranja","Casa dourada"};
	private Text clueTx;

	void Start ()
	{
		clueTx = GameObject.Find ("Clue").GetComponent<Text> ();
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTx.text = clue[RandomHouse.goldenHouse].ToString();
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTx.text = "";
		}
	}
}
