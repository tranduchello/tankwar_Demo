using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverBullet : MonoBehaviour {

    public GameObject exffectbullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject != gameObject)
        {
            GameObject ex = Instantiate(exffectbullet, transform.position, Quaternion.identity);
            
        }
        
    }
}
