using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomFOV = 20f;  // Giá trị FOV khi zoom
    [SerializeField] private float zoomSpeed = 5f;  // Tốc độ zoom

    private float defaultFOV;

    private void Start()
    {
        defaultFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))  // Nhấn chuột phải để nhắm
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
        }
        else  // Thoát khỏi tâm ngắm
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, defaultFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
