using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
	public GameObject[] pauseObjects;
	[HideInInspector] public bool paused;
	
	void Start ()
	{
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
		paused = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			Pause();
		}
	}

	public void Pause()
	{
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			paused = true;
			showPaused();
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			paused = false;
			hidePaused();
		}
	}
	
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void showPaused()
	{
		foreach(GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
	}
	
	public void hidePaused()
	{
		foreach(GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
	}
	
	public void LoadLevel(int level)
	{
		Application.LoadLevel(level);
	}
}
