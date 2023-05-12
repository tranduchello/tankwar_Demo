using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congvao : MonoBehaviour {

    public GameObject bantung;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

        }
    }
    void ExPlo()
    {

        Instantiate(bantung, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
