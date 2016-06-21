﻿using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	Camera cam;
	Transform my;
	Rigidbody2D body;

	private float Speed = 0.2f;
	
	void Awake ()
	{
		cam = Camera.main;
		my = GetComponent <Transform> ();
		body = GetComponent <Rigidbody2D> ();
	}
	
	
	void Update ()
	{
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.up * Speed);
		}
		if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.down * Speed);
		}
		if (Input.GetKey(KeyCode.Space))
		{
			Speed = 0.5f;
		}
		else Speed = 0.2f;

		// Distance from camera to object.  We need this to get the proper calculation.
		float camDis = cam.transform.position.x - my.position.x;
		
		// Get the mouse position in world space. Using camDis for the Z axis.
		Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
		
		float AngleRad = Mathf.Atan2 (-mouse.x - -my.position.x, mouse.y - my.position.y);
		float angle = (180 / Mathf.PI) * AngleRad;
		
		body.rotation = angle;
	}
}