using UnityEngine;
using System.Collections;

public class Clues : MonoBehaviour
{
	public GameObject clueObj;

	void Start ()
	{
		Instantiate (clueObj, GameObject.Find ("Move2Enemy1").transform.position + new Vector3(0,0,-3), Quaternion.identity);
	}
}
