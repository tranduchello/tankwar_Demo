using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
   // [SerializeField] private GameObject turret;
    public float speed = 2f;
    public float lerpSpeed = 10f;
    public float minAngleY; // góc quay tối thiểu theo trục y
    public float maxAngleY; // góc quay tối đa theo trục y
    public float minAngleX; // góc quay tối thiểu theo trục x
    public float maxAngleX; // góc quay tối đa theo trục x

   

    private float xDeg = 0f;
    private float yDeg = 0f;
    private Quaternion fromRotation;
    private Quaternion toRotation;
    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        xDeg -= Input.GetAxis("Mouse X") * speed;
        yDeg += Input.GetAxis("Mouse Y") * speed;
        yDeg = Mathf.Clamp(yDeg, minAngleY, maxAngleY); // giới hạn góc quay theo trục y
        xDeg = Mathf.Clamp(xDeg, minAngleX, maxAngleX); // giới hạn góc quay theo trục x

        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDeg, yDeg, 0f);
        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
    }
    
}
