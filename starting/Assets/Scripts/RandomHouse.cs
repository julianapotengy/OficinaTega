using UnityEngine;
using System.Collections;

public class RandomHouse : MonoBehaviour
{
	GameObject[] locais;
	public Sprite[] casas;
	bool casaespecial; 
	float casaouro;

	void Start ()
	{
		casaespecial = false; 
		casaouro = Random.Range(0,casas.Length);
		locais = GameObject.FindGameObjectsWithTag ("localcasa");

		for (int i = 0; i < locais.Length; i++)
		{
			int rand = Random.Range (0, casas.Length);
			if (rand == casaouro && !casaespecial)
			{ 
				locais [i].GetComponent<SpriteRenderer> ().sprite = casas [rand];
				locais[i].tag = "casaouro";
				casaespecial = true ; 
			}
			else if (rand ==casaouro) 
			{
				if (casaouro ==0)
				{
					locais [i].GetComponent<SpriteRenderer> ().sprite = casas [rand+1];
				}
				else locais [i].GetComponent<SpriteRenderer> ().sprite = casas [rand-1];
			}
			else locais [i].GetComponent<SpriteRenderer> ().sprite = casas [rand];
		}
	}

	void Update ()
	{
	
	}
}
