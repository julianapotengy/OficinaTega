using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour
{
	private bool Up;

	void Update ()
	{
		if(Up)
		{
			gameObject.transform.Translate(0.1f, 0.1f, 0);
			Up = false;
		}
		else
		{
			gameObject.transform.Translate(-0.1f, -0.1f, 0);
			Up = true;
		}
	}
}
