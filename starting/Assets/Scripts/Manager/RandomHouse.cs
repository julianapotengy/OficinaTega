using UnityEngine;
using System.Collections;

public class RandomHouse : MonoBehaviour
{
	private GameObject[] places;
	public Sprite[] spHouses = new Sprite[3];

	private bool specialHouse; 
	public static int goldenHouse;

	void Start ()
	{
		specialHouse = false; 
		goldenHouse = Random.Range(0, spHouses.Length);
		places = GameObject.FindGameObjectsWithTag ("housePremises");

		for (int i = 0; i < places.Length; i++)
		{
			int rand = Random.Range (0, spHouses.Length);
			if (rand == goldenHouse && !specialHouse)
			{ 
				places[i].GetComponent<SpriteRenderer> ().sprite = spHouses[rand];
				places[i].tag = "goldenHouse";
				specialHouse = true ; 
			}
			else if (rand == goldenHouse) 
			{
				if (goldenHouse == 0)
				{
					places[i].GetComponent<SpriteRenderer> ().sprite = spHouses[rand+1];
				}
				else places[i].GetComponent<SpriteRenderer> ().sprite = spHouses[rand-1];
			}
			else places[i].GetComponent<SpriteRenderer> ().sprite = spHouses[rand];
		}
	}
	void Update()
	{

	}
}
