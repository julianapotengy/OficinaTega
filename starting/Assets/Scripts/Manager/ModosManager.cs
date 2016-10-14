using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModosManager : MonoBehaviour
{
	private GameObject showLevels2Choose;
	private GameObject showModos;
	private bool loadScene;
	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loading;
	private bool canLoad;

	void Start ()
	{
		showLevels2Choose = GameObject.FindGameObjectWithTag ("ShowLevels2Choose");
		showLevels2Choose.SetActive (false);
		showModos = GameObject.FindGameObjectWithTag ("ShowModos");
		loadScene = false;
		loading.gameObject.SetActive (false);
		canLoad = false;
	}

	void Update()
	{
		if(canLoad && !loadScene)
		{
			loading.gameObject.SetActive (true);
			loadScene = true;
			loading.text = "Carregando...";
			StartCoroutine(LoadNewScene());
		}
		if(loadScene)
		{
			loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, Mathf.PingPong(Time.time, 1));
		}
	}

	public void SetClassicMode()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("MODE", "classic");
		showLevels2Choose.SetActive (true);
	}

	public void SetSurvivelMode()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("MODE", "survivel");
		showLevels2Choose.SetActive (true);
	}

	public void SetEasyDifficulty()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("DIFFICULTY", "easy");
		canLoad = true;
		showLevels2Choose.SetActive (false);
		showModos.SetActive (false);
	}

	public void SetMediumDifficulty()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("DIFFICULTY", "medium");
		canLoad = true;
		showLevels2Choose.SetActive (false);
		showModos.SetActive (false);
	}

	public void SetHardDifficulty()
	{
		GameManager.ButtonPaperClip ();
		PlayerPrefs.SetString ("DIFFICULTY", "hard");
		canLoad = true;
		showLevels2Choose.SetActive (false);
		showModos.SetActive (false);
	}

	IEnumerator LoadNewScene()
	{
		yield return new WaitForSeconds(1);
		AsyncOperation async = Application.LoadLevelAsync(scene);
		while (!async.isDone)
		{
			yield return null;
		}
	}

	public void buttonHigh()
	{
		GameManager.ButtonHighlightedClip ();
	}
}
