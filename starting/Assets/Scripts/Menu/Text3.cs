using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text3 : MonoBehaviour
{
	public float letterPause = 0.04f;
	string message;
	private float timer;
	private GameObject history;
    public GameObject historyTetxs;

    void Start ()
	{
		message = GetComponent<Text> ().text;
		GetComponent<Text> ().text = "";
		history = GameObject.Find ("HistoryCredits");
	}
	
	void Update()
	{
		if(GetComponent<Text>().text == message)
		{
			timer += Time.deltaTime;
			if(timer >= 2)
			{
				Application.LoadLevel("Menu");
				timer = 0;
			}
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
