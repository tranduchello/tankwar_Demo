using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public float speedH = 2f;
    //public float speedV = 2f;
    //private float yaw = 0f;
    //private float pitch = 0f;
    //private void Start()
    //{

    //}
    //private void Update()
    //{
    //    yaw += speedH * Input.GetAxis("Mouse X");
    //    pitch -= speedV * Input.GetAxis("Mouse Y");

    //    transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    //}

    public GameObject turret; //điểm nằm trong player mà camera di chuyển theo điểm đó
    Transform followPoint;
    // Use this for initialization
    void Start()
    {
        followPoint = new GameObject().transform; //tạo 1 gameobject trên unity
        followPoint.name = "follow Point"; //đặt tên cho gameobject
        followPoint.position = transform.position; //đặt vị trí của Follwe Point
        //followPoint.SetParent(turret.transform); //chọn turret làm dối tượng cha

    }

    // Update is called once per frame
    void Update()
    {
        //di chuyển camera tới vị trí của followPoint
        transform.position = Vector3.Slerp(transform.position, followPoint.position, 2 * Time.deltaTime);

        // Debug.Log(transform.eulerAngles);
        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, turret.transform.eulerAngles.x, 20),
            Mathf.LerpAngle(transform.eulerAngles.y, turret.transform.eulerAngles.y, 20),
        transform.eulerAngles.z);
    }
}
