using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    private GameObject[] characterList;

	// Use this for initialization
	void Start () {
        characterList = new GameObject[transform.childCount];
        //Fill the array with our molde
            for(int i = 0; i < transform.childCount; i++)
                characterList[i] = transform.GetChild(i).gameObject;
            //We toggle off their renderer
        foreach (GameObject go in characterList)
            go.SetActive(false);

        //we toggle on the first index
       // if(characterList[0])




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
