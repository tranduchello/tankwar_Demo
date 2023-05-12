using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoockAtMouse : MonoBehaviour {
    [SerializeField]
    private Transform _turretBarrel;
	
	// Update is called once per frame
	void FixeUpdate () {

        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;


        if(Physics.Raycast(rayOrigin, out hitInfo))
        {
            if(hitInfo.collider != null)
            {
                _turretBarrel.rotation = Quaternion.LookRotation(hitInfo.point);
            }
        }

	}
}
