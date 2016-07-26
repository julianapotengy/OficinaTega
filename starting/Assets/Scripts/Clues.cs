using UnityEngine;
using System.Collections;

public class Clues : MonoBehaviour
{
	float HouseToWin;
	public GameObject dicaobj;

	void Start ()
	{
		HouseToWin = RandomHouse.casaouro;
		Instantiate (dicaobj, GameObject.Find ("Move2Enemy1").transform.position +new Vector3(0,0,-3), Quaternion.identity);
		Debug.Log ("hey");
	}

	void Update ()
	{
	
	}
}
