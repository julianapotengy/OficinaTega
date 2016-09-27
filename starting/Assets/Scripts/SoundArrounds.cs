using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SoundArrounds : MonoBehaviour
{
	private player Player;
	private float time;
	private Rigidbody2D rb;
	private GameObject compass;
	private float low;
	private GameObject nearBatebola;
	private List< float> salvartodos;

	void Start ()
	{
		time = 0;
		rb = GetComponent<Rigidbody2D> ();
		Player = GetComponentInParent<player> ();
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
				var batebolasNear = Physics2D.OverlapCircleAll(transform.position,148.3756f);
				salvartodos = new List<float>();
				int i = 0;
				low = 10000000000;
				
				foreach (Collider2D batebolas in batebolasNear)
				{
					if (batebolas.gameObject.tag == "enemy")
					{
						salvartodos.Add( Vector3.Distance(batebolas.gameObject.transform.position, Player.gameObject.transform.position));
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
						if (Vector3.Distance(batebolas.gameObject.transform.position, Player.gameObject.transform.position)<= low)
						{
							nearBatebola = batebolas.gameObject;
						}
					}
				}
				
				var dir = nearBatebola.transform.position - Player.transform.position;
				dir.Normalize();
				var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				compass.transform.FindChild("agulha").GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0,0,angle-90);
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		compass.SetActive (false);
	}
}
