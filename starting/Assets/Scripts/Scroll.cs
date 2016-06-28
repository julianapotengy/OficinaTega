using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour
{

	void Start ()
	{
	
	}

	void Update ()
	{
		float d = Input.GetAxis("Mouse ScrollWheel");
		if (d > 0f)
		{
			Camera.main.orthographicSize += 1; // scroll up
		}
		else if (d < 0f)
		{
			Camera.main.orthographicSize -= 1; // scroll down
		}
	}
}
