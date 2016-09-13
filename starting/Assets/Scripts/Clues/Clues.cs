using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Clues : MonoBehaviour
{
	public GameObject clueObj;
	private GameObject alert;
	[HideInInspector] public bool showAlert;

	private PauseGame isPaused;
	[HideInInspector] public bool showClue;

	string[] initialClues = new string[4]{"Fuja dos bate bolas","Procure mais dicas","Ache sua casa","Existem cameras pela cidade"};
	string[,] array2d = new string[6,3]{{"Sua casa não é dourada","Sua casa não é laranja","Sua casa é da cor do chocolate"},
						{"Sua casa não é marrom","Sua casa não é dourada","Sua casa é da cor de uma fruta"},
						{"Sua casa não é laranja","Sua casa não é marrom","Sua casa é camuflada"},
						{"Sua casa não é verde","Sua casa não é roxa","Sua casa é da cor do mar"},
						{"Sua casa não é azul", "Sua casa não é roxa","Sua casa é da cor do mato"},
						{"Sua casa não é verde","Sua casa não é azul","Sua casa é da cor de um tipo de uva"}};
	Vector3[] InitialLocation = new Vector3[4]{new Vector3(10,0,0),new Vector3(-20,0,0),new Vector3(0,40,0),new Vector3(0,-40,0)};

	private List<GameObject> clueStart = new List<GameObject>();
	private List<GameObject> cluesTxt = new List<GameObject>();
	private GameObject notepad;

	public List<Text> notebookClues = new List<Text>();
	public static List<string> cluesColected = new List<string>();

	void Awake()
	{
		alert = GameObject.FindGameObjectWithTag ("Alert");
	}

	void Start ()
	{
		isPaused = GameObject.Find ("GameManager").GetComponent<PauseGame> ();
		showClue = false;

		notebookClues = new List<Text>();
		notepad = GameObject.Find ("Notepad");
		cluesColected = new List<string>();
		StartCoroutine (WaitClue ());

		for (int i = 0; i < initialClues.Length; i++)
		{
			clueStart.Add(Instantiate(clueObj, GameObject.Find ("Player").transform.position + InitialLocation[i],
			                          Quaternion.identity) as GameObject);
			clueStart[i].GetComponent<ClueObj>().stringClueTxt = initialClues[i];
			clueStart[i].GetComponent<ClueObj>().alert = alert;
		}
	}

	void Update()
	{
		if(!isPaused.paused)
		{
			for (int i = 0; i < cluesColected.Count; i++)
			{
				if (cluesColected[i] != null)
					notebookClues[i].text = i + 1 + ". " + cluesColected[i];
			}

			if (Input.GetKeyDown (KeyCode.I))
			{
				ShowClues();
				showAlert = false;
			}
			if (showAlert)
				alert.SetActive(true);
			else 
				alert.SetActive(false);
			
			if(showClue)
				notepad.SetActive(true);
			else if(!showClue)
				notepad.SetActive(false);
		}
	}

	IEnumerator WaitClue()
	{
		yield return new WaitForSeconds (0.001f);
		for (int i = 0; i < 3; i++)
		{
			cluesTxt.Add(Instantiate (clueObj, GameObject.Find ("Player").transform.position + new Vector3(0,10*(i+1),0),
			                          Quaternion.identity) as GameObject);
			cluesTxt[i].GetComponent<ClueObj>().stringClueTxt = array2d[RandomHouse.goldenHouse, i];
			cluesTxt[i].GetComponent<ClueObj>().alert = alert;
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

	public void ShowClues()
	{
		showClue = !showClue;
	}
}
