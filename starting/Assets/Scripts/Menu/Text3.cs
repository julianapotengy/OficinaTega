using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text3 : MonoBehaviour
{
	public float letterPause = 0.2f;
	string message;
	private float timer;
	private GameObject history;
	private GameObject credits;
	private bool showCredits;
	
	void Start ()
	{
		message = GetComponent<Text> ().text;
		GetComponent<Text> ().text = "";
		history = GameObject.Find ("HistoryCredits");
		credits = GameObject.Find ("Credits");
		credits.SetActive(false);
		showCredits = false;
	}
	
	void Update()
	{
		if(GetComponent<Text>().text == message)
		{
			timer += Time.deltaTime;
			if(timer >= 1)
			{
				credits.SetActive(true);
				showCredits = true;
				GetComponent<Text>().text = "";
				timer = 0;
			}
		}
		if(showCredits)
		{
			timer += Time.deltaTime;
			if(timer >= 10)
				history.SetActive(false);
		}
	}
	
	public void Wait()
	{
		StartCoroutine (TypeText3());
	}
	
	IEnumerator TypeText3 ()
	{
		foreach (char letter in message.ToCharArray())
		{
			GetComponent<Text>().text += letter;
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}
	}
}
