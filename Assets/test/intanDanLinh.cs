using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intanDanLinh : MonoBehaviour
{
    public GameObject bulletPrefab; // Đối tượng đạn
    public GameObject vitri;
    public Transform player; // Transform của player
    public float bulletSpeed; // Tốc độ di chuyển của viên đạn

    private float timeSinceLastShot = 0f; // Thời gian kể từ lần bắn đạn cuối cùng

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Nếu đã đến thời điểm bắn đạn tiếp theo
        if (timeSinceLastShot >= 1f) // Để bắn 1 viên đạn mỗi 0.2 giây, bạn có thể thay đổi giá trị này
        {
            // Tạo một đối tượng đạn mới
            GameObject bullet = Instantiate(bulletPrefab, vitri.transform.position, Quaternion.identity);

            // Quay đối tượng đạn về phía player
            bullet.transform.LookAt(player);

            // Đẩy đối tượng đạn di chuyển về phía player
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            // Đặt lại thời gian kể từ lần bắn đạn cuối cùng
            timeSinceLastShot = 0f;
        }
    }
}
 