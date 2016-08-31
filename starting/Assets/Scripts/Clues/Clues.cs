using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Clues : MonoBehaviour
{
	public GameObject clueObj;
	string[] initialClues= new string[3]{"Fuja dos bate bolas","Procure mais dicas","Ache sua casa"};
	string[,] array2d = new string[3,3]{{"Sua casa não é dourada","Sua casa não é laranja","Sua casa é da cor do chocolate"},{"Sua casa não é marrom","Sua casa não é dourada","Sua casa é da cor de uma fruta"},{"Sua casa não é laranja","Sua casa não é marrom","Sua casa é camuflada"}}	;
	List<GameObject> ClueInicio = new List<GameObject>();
	List<GameObject> CluesTx = new List<GameObject>();
	GameObject caderno; 
	public List<Text> CadernoDicas= new List<Text>();
	public static List<string> ColectedDicas = new List<string>();
	void Start ()
	{
		CadernoDicas= new List<Text>();
		caderno = GameObject.Find ("Caderno");

		ColectedDicas = new List<string>();
		Debug.Log(array2d);
		StartCoroutine (WaitClue ());

		for (int i = 0;i<initialClues.Length;i++){
		 ClueInicio.Add(Instantiate (clueObj, GameObject.Find ("Player").transform.position + new Vector3((i+Random.Range(-1,2))*15,0,0), Quaternion.identity) as GameObject);
			ClueInicio[i].GetComponent<ClueObj>().DicaTx = initialClues[i];
	}
	}
	void Update()
	{
		for (int i = 0; i<ColectedDicas.Count; i++) {
			if (ColectedDicas[i]!=null)
			CadernoDicas[i].text = i+1+". "+ ColectedDicas[i];
		}
		if (Input.GetKey (KeyCode.I)) {
			caderno.SetActive (true);
		} else 
			caderno.SetActive (false);
	}
IEnumerator WaitClue()
	{yield return new WaitForSeconds (1);
		for (int i =0;i<3;i++)
		{
			CluesTx.Add(Instantiate (clueObj, GameObject.Find ("Player").transform.position+new Vector3(0,10*(i+1),0) , Quaternion.identity) as GameObject);
			CluesTx[i].GetComponent<ClueObj>().DicaTx = array2d[RandomHouse.goldenHouse,i];

		}
		getTexts();
	}
void getTexts()
	{
		foreach (Transform t in caderno.transform) {
			foreach (Transform texto in t.transform) {
				if (texto.gameObject.name.Substring (0, 4) == "Text") {
					CadernoDicas.Add(texto.gameObject.GetComponent<Text>());
				}
			}
		
		}
		caderno.SetActive (false);
	}
}
