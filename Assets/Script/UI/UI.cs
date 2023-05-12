using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public int Roket = 10;
    public Text RoketText;
    public GameObject[] enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RoketText.text = " " + Roket;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Roket--;
            if(Roket <= 0)
            {
                Roket = 0;
            }
        }
	}
}
