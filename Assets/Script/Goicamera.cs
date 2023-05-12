using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goicamera : MonoBehaviour {
    public Transform xoaycamera;
    public Transform Maincamera;
    bool status = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            status = !status;
            if (status)
            {
                xoaycamera.gameObject.SetActive(true);
                Maincamera.gameObject.SetActive(false);
               // Time.timeScale = 0;
            }
            else
            {
                Maincamera.gameObject.SetActive(true);
                xoaycamera.gameObject.SetActive(false);
                //Time.timeScale = 1;
            }
        }
	}
}
    
