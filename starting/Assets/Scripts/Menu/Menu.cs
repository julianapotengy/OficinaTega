using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	GameObject light;

	void Start()
	{
		if (Application.loadedLevel == 0)
		{
			light = GameObject.Find ("light2");
			StartCoroutine (lighteffect ());
		}
	}

	public void changeScene(int i)
	{
		GameManager.ButtonMenuClip ();
		Application.LoadLevel (i);
	}

	public void Exit()
	{
		GameManager.ButtonMenuClip ();
		Application.Quit ();
	}

	public void buttonHigh()
	{
		GameManager.ButtonHighlightedClip ();
	}

	IEnumerator lighteffect()
	{
		bool controle = true;
		while (true)
		{
			controle =!controle;
			light.SetActive(controle);
			yield return new WaitForSeconds(1.2f);
		}
	}
}