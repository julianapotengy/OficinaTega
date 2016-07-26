using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjDica : MonoBehaviour
{
	string[] dica = new string[3]{"Casa vermelha","Casa amarela","Casa dourada"};
	Text clueTx;

	void Start ()
	{
		clueTx = GameObject.Find ("CLUE").GetComponent<Text> ();
	}

	void Update ()
	{
	
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player")
		{
			clueTx.text = dica[RandomHouse.casaouro].ToString();
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
