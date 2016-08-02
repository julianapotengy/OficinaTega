using UnityEngine;
using System.Collections;

public class Clues : MonoBehaviour
{
	//float HouseToWin;
	public GameObject clueObj;

	void Start ()
	{
		//HouseToWin = RandomHouse.casaouro;
		Instantiate (clueObj, GameObject.Find ("Move2Enemy1").transform.position + new Vector3(0,0,-3), Quaternion.identity);
	}
}
