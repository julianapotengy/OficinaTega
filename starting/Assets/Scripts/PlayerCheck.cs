using UnityEngine;
using System.Collections;

public class PlayerCheck : MonoBehaviour
{
	private GameObject Player;
	RectTransform rect;
	float myHeight = 0;

	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		myHeight = GetComponent<SpriteRenderer>().bounds.size.y;
	}

	void Update ()
	{

		if (transform.position.y - myHeight / 2 <=(Player.transform.position.y-Player.GetComponent<SpriteRenderer>().bounds.size.y/2))
		{
			GetComponent<SpriteRenderer>().sortingLayerName = "UpPlayer";
		}
		else
			GetComponent<SpriteRenderer>().sortingLayerName = "DownPlayer";
	
	}
}
