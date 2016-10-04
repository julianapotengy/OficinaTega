using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	private bool loadScene;
	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loading;

	void Start ()
	{
		loadScene = false;
	}

	void Update ()
	{
		if(Input.GetKey(KeyCode.Space) && !loadScene)
		{
			loadScene = true;
			loading.text = "Carregando...";
			StartCoroutine(LoadNewScene());
		}
		if(loadScene)
		{
			loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, Mathf.PingPong(Time.time, 1));
		}
	}
	IEnumerator LoadNewScene()
	{
		yield return new WaitForSeconds(3);
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = Application.LoadLevelAsync(scene);
		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone)
		{
			yield return null;
		}
		
	}

}
