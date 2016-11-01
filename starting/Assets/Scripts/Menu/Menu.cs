using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	[SerializeField] GameObject light;
	[SerializeField] AudioClip lightaudio;
	void Start()
	{
		StartCoroutine (lighteffect ());

	}

	public void changeScene(int i)
	{
		GameManager.ButtonPaperClip ();
		Application.LoadLevel (i);
	}

	public void Exit()
	{
		GameManager.ButtonPaperClip ();
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
			if (light!= null){
			controle =!controle;
				light.SetActive(controle);}
			//GameManager.Playsound(lightaudio);
			yield return new WaitForSeconds(1.2f);
		}
	}
}