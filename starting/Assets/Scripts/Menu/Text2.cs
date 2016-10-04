using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Text2 : MonoBehaviour
{
	public float letterPause = 0.2f;
	string message;
	public Text text3;
	private float timer;
	
	void Start ()
	{
		message = GetComponent<Text> ().text;
		GetComponent<Text> ().text = "";
	}
	
	void Update()
	{
		if(GetComponent<Text>().text == message)
		{
			timer += Time.deltaTime;
			if(timer >= 1)
			{
				text3.GetComponent<Text3>().Wait();
				GetComponent<Text>().text = "";
			}
		}
	}
	
	public void Wait()
	{
		StartCoroutine (TypeText2());
	}
	
	IEnumerator TypeText2 ()
	{
		foreach (char letter in message.ToCharArray())
		{
			GetComponent<Text>().text += letter;
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}
	}
}
