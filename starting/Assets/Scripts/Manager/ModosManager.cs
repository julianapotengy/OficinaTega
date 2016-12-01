using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModosManager : MonoBehaviour
{
	private GameObject showLevels2Choose;
	[SerializeField] private int scene;
	[SerializeField] private Text loading;
    [SerializeField] GameObject fundoloading;
	private AsyncOperation async;

	void Start ()
	{
        showLevels2Choose = GameObject.FindGameObjectWithTag ("ShowLevels2Choose");
		showLevels2Choose.SetActive (false);
		loading.gameObject.SetActive (false);
		showLevels2Choose.SetActive (true);
	}


	/*void Update()
	{
		if(canLoad && !loadScene)
		{
			loading.gameObject.SetActive (true);
            fundoloading.SetActive(true);
			loadScene = true;
			loading.text = "Carregando...";
			StartCoroutine(LoadNewScene());
		}
		if(loadScene)
		{
            loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, Mathf.PingPong(Time.time, 1));
		}
	}*/


	public void SetEasyDifficulty()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("DIFFICULTY", "easy");
		showLevels2Choose.SetActive (false);
		loading.gameObject.SetActive (true);
		fundoloading.SetActive(true);
		StartCoroutine(LoadLevel(scene));
	}

	public void SetMediumDifficulty()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("DIFFICULTY", "medium");
		showLevels2Choose.SetActive (false);
		loading.gameObject.SetActive (true);
		fundoloading.SetActive(true);
		StartCoroutine(LoadLevel(scene));
	}

	public void SetHardDifficulty()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("DIFFICULTY", "hard");
		showLevels2Choose.SetActive (false);
		loading.gameObject.SetActive (true);
		fundoloading.SetActive(true);
		StartCoroutine(LoadLevel(scene));
	}

	IEnumerator LoadLevel (int level)
	{
		async = Application.LoadLevelAsync(level);
		while (!async.isDone)
		{
			loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, Mathf.PingPong(Time.time, 1));
			yield return null;
		}
	}

	public void buttonHigh()
	{
		GameManager.ButtonHighlightedClip ();
	}
}
