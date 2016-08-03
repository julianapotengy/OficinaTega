using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lights : MonoBehaviour
{
	public List<Light> pointLights = new List<Light>();
	private float timer;
	private int[] rand = new int[7];

	void Start ()
	{
		pointLights.AddRange(GetComponentsInChildren<Light>());
		timer = 1;

		rand[0] = Random.Range(0, pointLights.Count);
		rand[1] = Random.Range(0, pointLights.Count);
		rand[2] = Random.Range(0, pointLights.Count);
		rand[3] = Random.Range(0, pointLights.Count);
		rand[4] = Random.Range(0, pointLights.Count);
		rand[5] = Random.Range(0, pointLights.Count);
		rand[6] = Random.Range(0, pointLights.Count);
	}

	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			pointLights[rand[0]].enabled = !pointLights[rand[0]].enabled;
			pointLights[rand[1]].enabled = !pointLights[rand[1]].enabled;
			pointLights[rand[2]].enabled = !pointLights[rand[2]].enabled;
			pointLights[rand[3]].enabled = !pointLights[rand[3]].enabled;
			pointLights[rand[4]].enabled = !pointLights[rand[4]].enabled;
			pointLights[rand[5]].enabled = !pointLights[rand[5]].enabled;
			pointLights[rand[6]].enabled = !pointLights[rand[6]].enabled;

			timer = 1;
		}
	}
}
