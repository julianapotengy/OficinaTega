using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Texts : MonoBehaviour
{
	public float letterPause = 0.04f;
	private string message;
	public Text text2;
	private float timer;
	
	void Start ()
	{
		message = GetComponent<Text>().text;
		GetComponent<Text>().text = "";
	}
	
	void Update()
	{
		if(GetComponent<Text>().text == message)
		{
			timer += Time.deltaTime;
			if(timer >= 2)
			{
				text2.GetComponent<Text2>().Wait();
				GetComponent<Text>().text = "";
			}
		}
	}

	public void Wait()
	{
		StartCoroutine (TypeText());
	}
	
	IEnumerator TypeText ()
	{
		foreach (char letter in message.ToCharArray())
		{
			GetComponent<Text>().text += letter;
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}     
	}
}
