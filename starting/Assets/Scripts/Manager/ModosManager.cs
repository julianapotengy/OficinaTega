using UnityEngine;
using System.Collections;

public class ModosManager : MonoBehaviour
{
	public GameObject[] showLevels2Choose;

	void Start ()
	{
		showLevels2Choose = GameObject.FindGameObjectsWithTag ("ShowLevels2Choose");
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(false);
		}
	}

	public void SetClassicMode()
	{
		PlayerPrefs.SetString ("MODE", "classic");
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(true);
		}
	}

	public void SetSurvivelMode()
	{
		PlayerPrefs.SetString ("MODE", "survivel");
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(true);
		}
	}

	public void SetEasyDifficulty()
	{
		PlayerPrefs.SetString ("DIFFICULTY", "easy");
		Application.LoadLevel ("Game");
	}

	public void SetMediumDifficulty()
	{
		PlayerPrefs.SetString ("DIFFICULTY", "medium");
		Application.LoadLevel ("Game");
	}

	public void SetHardDifficulty()
	{
		PlayerPrefs.SetString ("DIFFICULTY", "hard");
		Application.LoadLevel ("Game");
	}
}
