using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryTexts : MonoBehaviour
{
	private GameObject history;
	public Image headphoneImage;
	public Text headphoneText;
	public Text historyText;
	private float timer;
	private bool canWrite;

	void Start ()
	{
		history = GameObject.Find ("HistoryCredits");
		timer = 0;
		canWrite = false;
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= 5 && !canWrite)
		{
			headphoneImage.enabled = false;
			headphoneText.enabled = false;
			historyText.GetComponent<Texts>().Wait();
			canWrite = true;
		}
		if (Input.anyKey)
			goToMenu ();
	}

	public void goToMenu()
	{
		history.SetActive(false);
	}
}
