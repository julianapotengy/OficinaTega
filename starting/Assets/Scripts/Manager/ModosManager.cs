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
		Debug.Log (PlayerPrefs.GetString("MODE"));
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(true);
		}
	}

	public void SetSurvivelMode()
	{
		PlayerPrefs.SetString ("MODE", "survivel");
		Debug.Log (PlayerPrefs.GetString("MODE"));
		foreach(GameObject g in showLevels2Choose)
		{
			g.SetActive(true);
		}
	}

	public void SetEasyDifficulty()
	{
		PlayerPrefs.SetString ("DIFFICULTY", "easy");
		Debug.Log (PlayerPrefs.GetString("DIFFICULTY"));
		Application.LoadLevel ("Game");
	}

	public void SetMediumDifficulty()
	{
		PlayerPrefs.SetString ("DIFFICULTY", "medium");
		Debug.Log (PlayerPrefs.GetString("DIFFICULTY"));
		Application.LoadLevel ("Game");
	}

	public void SetHardDifficulty()
	{
		PlayerPrefs.SetString ("DIFFICULTY", "hard");
		Debug.Log (PlayerPrefs.GetString("DIFFICULTY"));
		Application.LoadLevel ("Game");
	}
}
