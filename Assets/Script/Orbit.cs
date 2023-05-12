using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
                // Khóa con trỏ vào giữa màn hình
                Cursor.lockState = CursorLockMode.Locked;
                // ẩn con trỏ chuột 
                Cursor.visible = false;
         }

        else if (Input.GetMouseButtonUp(0))
        {

            // Khóa con trỏ vào giữa màn hình
            Cursor.lockState = CursorLockMode.None;
            // ẩn con trỏ chuột 
            Cursor.visible = true;

        }

    }
}
