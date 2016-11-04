using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryTexts : MonoBehaviour
{
	public Image headphoneImage;
	public Text historyText;
	private float timer;
	private bool canWrite;

	void Start ()
	{
		timer = 0;
		canWrite = false;
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= 5 && !canWrite)
		{
			headphoneImage.enabled = false;
			historyText.GetComponent<Texts>().Wait();
			canWrite = true;
		}
		if (Input.anyKey)
			goToMenu ();
	}

	public void goToMenu()
	{
        Application.LoadLevel("Menu");
	}
}
