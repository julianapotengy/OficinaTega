using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CreditsManager : MonoBehaviour {
	public List<GameObject> credits = new List<GameObject>();
	int control = 1;
	float reduzer;
	[SerializeField] AudioClip audio;
	// Use this for initialization
	void Start () {
		credits = new List<GameObject>();
		reduzer = 255;
		control = 1;
		foreach (Transform t in transform) {
			credits.Add (t.gameObject);
		}
		StartCoroutine (test ());

	}
		
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator test()
	{//yield return new WaitForSeconds (2);
		while (control<=9) {

			if (credits[credits.Count- control].GetComponent<Image>().color.a<=0)
			{
				control++;
				reduzer =255;
				if (control==10){
					GameManager.Playsound(audio);
					Destroy(gameObject);}
					
					yield return new WaitForSeconds(2);
			}
			else 
			{
				reduzer -=2;
				credits[credits.Count- control].GetComponent<Image>().color = new Color(credits[credits.Count- control].GetComponent<Image>().color.r,
				                                                                        credits[credits.Count- control].GetComponent<Image>().color.b,
				                                                                        credits[credits.Count- control].GetComponent<Image>().color.g,reduzer/255);
			}
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
}
