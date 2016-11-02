using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GrafiteManager : MonoBehaviour {
    public List<GameObject> grafites;
	// Use this for initialization
	void Start () {
        grafites = new List<GameObject>();
        foreach (Transform t in transform)
        {
            grafites.Add(t.gameObject);
        }
        int rand = Random.Range(0, grafites.Count);
        grafites[rand].SetActive(true);
        for (int i = 0; i<grafites.Count;i++)
        {
            if (i!=rand)
            {
                Destroy(grafites[i]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
