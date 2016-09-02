using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Clues : MonoBehaviour
{
	public GameObject clueObj;

	string[] initialClues = new string[3]{"Fuja dos bate bolas","Procure mais dicas","Ache sua casa"};
	string[,] array2d = new string[3,3]{{"Sua casa não é dourada","Sua casa não é laranja","Sua casa é da cor do chocolate"},{"Sua casa não é marrom","Sua casa não é dourada","Sua casa é da cor de uma fruta"},{"Sua casa não é laranja","Sua casa não é marrom","Sua casa é camuflada"}}	;

	private List<GameObject> clueStart = new List<GameObject>();
	private List<GameObject> cluesTxt = new List<GameObject>();
	private GameObject notepad;

	public List<Text> notebookClues = new List<Text>();
	public static List<string> cluesColected = new List<string>();

	void Start ()
	{
		notebookClues = new List<Text>();
		notepad = GameObject.Find ("Notepad");

		cluesColected = new List<string>();
		StartCoroutine (WaitClue ());

		for (int i = 0; i < initialClues.Length; i++)
		{
			clueStart.Add(Instantiate(clueObj, GameObject.Find ("Player").transform.position + new Vector3((i+Random.Range(-1,2))* 15,0,0), Quaternion.identity) as GameObject);
			clueStart[i].GetComponent<ClueObj>().stringClueTxt = initialClues[i];
		}
	}

	void Update()
	{
		for (int i = 0; i < cluesColected.Count; i++)
		{
			if (cluesColected[i] != null)
				notebookClues[i].text = i + 1 + ". " + cluesColected[i];
		}
		if (Input.GetKey (KeyCode.I))
			notepad.SetActive (true);
		else 
			notepad.SetActive (false);
	}

	IEnumerator WaitClue()
	{
		yield return new WaitForSeconds (1);
		for (int i = 0; i < 3; i++)
		{
			cluesTxt.Add(Instantiate (clueObj, GameObject.Find ("Player").transform.position + new Vector3(0,10*(i+1),0), Quaternion.identity) as GameObject);
			cluesTxt[i].GetComponent<ClueObj>().stringClueTxt = array2d[RandomHouse.goldenHouse, i];
		}
		getTexts();
	}

	void getTexts()
	{
		foreach (Transform t in notepad.transform)
		{
			foreach (Transform texto in t.transform)
			{
				if (texto.gameObject.name.Substring (0, 4) == "Text")
				{
					notebookClues.Add(texto.gameObject.GetComponent<Text>());
				}
			}
		}
		notepad.SetActive (false);
	}
}
