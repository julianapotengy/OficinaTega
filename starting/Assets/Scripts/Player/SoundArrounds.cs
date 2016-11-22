using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SoundArrounds : MonoBehaviour
{
	public player Player;
	private float time;
	private GameObject compass;
	private float low;
	public GameObject nearBatebola;
	public List<float> salvartodos;
	public Collider2D[] batebolasNear;

	void Start ()
	{
		time = 0;
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ();;
		compass = GameObject.Find ("Compass");
		compass.SetActive (false);
	}

	void Update ()
	{
		time++;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy" && time > 1)
		{		
			if(PlayerPrefs.GetString("DIFFICULTY") == "easy" || PlayerPrefs.GetString("DIFFICULTY") == "medium")
			{
				Player.startsamba = true; 
				compass.SetActive (true);
			}
		}
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "enemy" && time > 1)
		{	
			if(PlayerPrefs.GetString("DIFFICULTY") == "easy" || PlayerPrefs.GetString("DIFFICULTY") == "medium")
			{
				Player.startsamba = true;
				compass.SetActive (true);
				batebolasNear = Physics2D.OverlapCircleAll(transform.position,100);
				salvartodos = new List<float>();
				int i = 0;
				low = 1000000;

				foreach (Collider2D batebolas in batebolasNear)
				{
					if (batebolas.gameObject.tag == "enemy")
					{
						salvartodos.Add(Vector3.Distance(batebolas.gameObject.transform.position, Player.gameObject.transform.position));
						i++;
					}
				}
				for (int c = 0; c < salvartodos.Count; c++)
				{
					if (salvartodos[c] < low)
					{
						low = salvartodos [c];
					}
				}
				foreach (Collider2D batebolas in batebolasNear)
				{
					if (batebolas.gameObject.tag == "enemy")
					{
						if (Mathf.Round(Vector3.Distance(batebolas.gameObject.transform.position, Player.gameObject.transform.position))<= low)
						{
							nearBatebola = batebolas.gameObject;
						}
					}
				}
				if(nearBatebola != null)
				{
					Vector3 dir = nearBatebola.gameObject.transform.position - Player.gameObject.transform.position;
					dir.Normalize();
					var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
					compass.transform.FindChild("agulha").GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, angle - 90);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		Player.startsamba = false;
		compass.SetActive (false);
	}
}
