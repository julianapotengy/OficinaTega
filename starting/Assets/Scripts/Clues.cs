using UnityEngine;
using System.Collections;

public class Clues : MonoBehaviour {
	float HouseToWin;
	string[] dica = new string[3]{"Casa vermelha","Casa Amarela","Casa Dourada"};
	public GameObject dicaobj;
	// Use this for initialization
	void Start () {
		HouseToWin = RandomHouse.casaouro;
		Instantiate (dicaobj, GameObject.Find ("Move2Enemy1").transform.position +new Vector3(0,0,-3), Quaternion.identity);
		Debug.Log ("hey");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
