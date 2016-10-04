using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public void changeScene(int i)
	{
		Application.LoadLevel (i);
	}

	public void Exit()
	{
		Application.Quit ();
	}
}