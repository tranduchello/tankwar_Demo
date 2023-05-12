using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlTypes;


public class TurretMove : MonoBehaviour 
{
    public float horizontalSpeed = 5.0f;// tốc độ quay ngang
    public float verticalSpeed = 5.0f;// tốc độ quay dọc
    public float maxVerticalAngle = 80.0f;// góc tối đa theo trục dọc
    public float minVerticalAngle = -80.0f;// góc tối thiểu theo trục dọc
    public float maxDistance = 10.0f;// Khoảng cách tối đa giữa camera và đối tượng nhắm tới
    public float minDistance = 3.0f;// Khoảng cách tối thiểu giữa camera và đối tượng nhắm tới

    private float verticalRotation = 0.0f;// Góc quay hiện tại theo trục dọc
    public bool clickmn;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        clickmn = false;

    }
    void Update()
    {

        if (Input.GetMouseButtonUp(1))
        {
            clickmn = false;

            if (!clickmn)
            {
                // Khóa con trỏ vào giữa màn hình
                Cursor.lockState = CursorLockMode.Locked;
                // ẩn con trỏ chuột 
                Cursor.visible = false;
            }
           
        }
        else if (Input.GetMouseButtonUp(0))
        {

            // Khóa con trỏ vào giữa màn hình
            Cursor.lockState = CursorLockMode.None;
            // ẩn con trỏ chuột 
            Cursor.visible = true;
            clickmn = true;

        }
        // tính góc quay ngang hiện tại dựa trên di chuyển của chuột trên trục x
        float horizontalRotation = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;

        // giới hạn góc quay dọc trong khoảng từ minVerticalAngle đến maxVerticalAngle
        verticalRotation -= Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;
        // xoay camera theo góc quay ngang và dọc vừa tính toán được
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        // xoay camera theo góc quay ngang và dọc vừa tính toán được
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localEulerAngles.y + horizontalRotation, 0);

    }
}
