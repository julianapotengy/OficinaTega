using UnityEngine;
using System.Collections;

public class RandomHouse : MonoBehaviour
{
	private GameObject[] places;
	public Sprite[] spHouses = new Sprite[6];

	private bool specialHouse; 
	public static int goldenHouse;
    [SerializeField] GameObject grafites;
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
				specialHouse = true; 
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

            if (Random.Range(0,10f)<3f)
            {                            
                GameObject gra=  Instantiate(grafites, places[i].transform.position, Quaternion.identity) as GameObject;
                 gra.transform.parent = places[i].transform;
                 gra.transform.localScale = new Vector3(1, 1, 1);
            }
		}
	}
}
