using UnityEngine;
using System.Collections;

public class FieldOfVision : MonoBehaviour
{
	[HideInInspector] public bool saw;
	[HideInInspector] public bool leaved;
	private Enemy3 enemy3;
	private GameObject player;
	Enemy1 enemy1;
	Enemy2 enemy2;
	Enemy3 enemy3scrpit;
	int pai;

	void Start ()
	{
		saw = false;
		leaved = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		if(gameObject.transform.parent.name.Equals("Enemy3(Clone)"))
			enemy3 = gameObject.transform.parent.GetComponent<Enemy3>();

		if (GetComponentInParent<Enemy1> () != null) {
			enemy1= GetComponentInParent<Enemy1> () ;	
			pai=1;
		}
		if (GetComponentInParent<Enemy2> () != null) {
			enemy2 = GetComponentInParent<Enemy2> () ;	
			pai=2;
		}
		if (GetComponentInParent<Enemy3> () != null) {
			enemy3 = GetComponentInParent<Enemy3> () ;	
			pai=3;
		}


	}
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			saw = true;
			leaved = true;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			leaved = true; 
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			leaved = false;
			if(gameObject.transform.parent.name.Equals("Enemy3(Clone)"))
				enemy3.GetComponent<Enemy3>().once = false;
		}
	}
}
