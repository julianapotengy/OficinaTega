using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryTexts : MonoBehaviour
{
	private GameObject history;
	public Image headphoneImage;
	public Text headphoneText;
	public Text historyText;

	private GameObject credits;
	private float timer;

	void Start ()
	{
		history = GameObject.Find ("History");
		credits = GameObject.Find ("Credits");
		timer = 0;
		credits.SetActive(false);
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if(timer >= 5)
		{
			headphoneImage.enabled = false;
			headphoneText.enabled = false;
			historyText.text = "Marcelo é um menino que se perdeu de seus pais durante um evento em Pinheiro Negro…";
		}
		if (timer >= 10)
		{
			historyText.text = "Para encontrar a casa em que está hospedado, ele precisa achar as dicas que encontra " +
								"pela rua, mas existe um problema...";
		}
		if (timer >= 15)
		{
			historyText.text = "Durante a noite após o evento, as pessoas andam pelas ruas vestidas de bate-bolas " +
								"para descontar sua raiva em quem estiver sozinho na rua.";
		}
		if (timer >= 20)
		{
			historyText.enabled = false;
			credits.SetActive(true);
		}
		if(timer >= 30)
		{
			history.SetActive(false);
		}
	}

	public void goToMenu()
	{
		history.SetActive(false);
	}
}
