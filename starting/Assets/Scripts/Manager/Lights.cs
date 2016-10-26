using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lights : MonoBehaviour
{
	public List<Light> pointLights = new List<Light>();
	private float timer;

	void Start ()
	{
		pointLights = new List<Light>();
		foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("PoleLight")) {
			pointLights.Add(Obj.GetComponent<Light>());
		}

		timer = 1;
	}

	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			int rand = Random.Range(0, pointLights.Count);
			Debug.Log (rand);
			pointLights[rand].enabled = !pointLights[rand].enabled;

			timer = 1;
		}
	}
}
