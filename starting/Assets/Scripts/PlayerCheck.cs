using UnityEngine;
using System.Collections;

public class PlayerCheck : MonoBehaviour {
	private GameObject Player;
	RectTransform rect;
	float widht =0;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");



		widht = GetComponent<SpriteRenderer>().bounds.size.x;

	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y - widht / 2 <Player.transform.position.y) {
			Debug.Log ("TA ALTO");
		} else
			Debug.Log ("ta BAIXO");
	
	}
}
