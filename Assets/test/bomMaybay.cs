using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomMaybay : MonoBehaviour
{
    public GameObject bulletPrefab; // đối tượng đạn
    public Transform firePoint; // vị trí bắn
    void Update()
    {
        // bắn đạn khi nhấn phím Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // tạo một đối tượng đạn mới
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        // set mục tiêu di chuyển của đạn là player
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
