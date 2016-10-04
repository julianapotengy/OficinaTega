using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
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
}