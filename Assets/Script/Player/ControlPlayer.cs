using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public Transform barrel;
    public float minHz = -30;
    public float maxHz = 30;
    public float minVt;
    public float maxVt;

    float horizontalRotation = 0;
    float verticalRotation = 0;

    void Update()
    {
        // horizontal and vertical rotation control
        //horizontalRotation = Mathf.Clamp(horizontalRotation + Input.GetAxis("Horizontal"), minHz, maxHz);
        horizontalRotation += Input.GetAxis("Horizontal");
        verticalRotation = Mathf.Clamp(verticalRotation + Input.GetAxis("Vertical"), minVt, maxVt);
        transform.rotation = Quaternion.Euler(-verticalRotation, horizontalRotation, 0);
    }

}

