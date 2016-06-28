using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

	void Start () {
	
	}

	void Update () {
	
	}

	public void trocacena(int i)
	{
		Application.LoadLevel (i);
	}
}