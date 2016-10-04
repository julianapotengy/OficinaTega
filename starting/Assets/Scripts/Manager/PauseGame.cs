using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
	public GameObject[] pauseObjects;
	[HideInInspector] public bool paused= false;
	private bool soundPaused;
	private AudioSource audio;
	
	void Start ()
	{
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
		paused = false;
		audio = Object.FindObjectOfType <AudioSource>() as AudioSource;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P))
			Pause ();
		if (Input.GetKeyDown (KeyCode.M))
				MuteButton ();
		
		if (soundPaused)
			audio.mute = true;
		else if (!soundPaused)
			audio.mute = false;
	}
	
	public void Reload()
	{
		GameManager.ButtonPaperClip ();
		Application.LoadLevel(Application.loadedLevel);

	}

	public void Modos()
	{
		GameManager.ButtonPaperClip ();		
		Application.LoadLevel (4);

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

	public void Pause()
	{
		GameManager.ButtonPaperClip ();
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

	public void MuteButton()
	{
		soundPaused = !soundPaused;
	}
}
