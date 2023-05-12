using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShipController : MonoBehaviour
{
    public GameObject bulletPrefab; // Đối tượng đạn
    public Transform player; // Transform của player
    public float bulletSpeed ; // Tốc độ di chuyển của viên đạn

    private float timeSinceLastShot = 0f; // Thời gian kể từ lần bắn đạn cuối cùng

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Nếu đã đến thời điểm bắn đạn tiếp theo
        if (timeSinceLastShot >= 5f) // Để bắn 1 viên đạn mỗi 0.2 giây, bạn có thể thay đổi giá trị này
        {
            // Tạo một đối tượng đạn mới
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Quay đối tượng đạn về phía player
            bullet.transform.LookAt(player);

            // Đẩy đối tượng đạn di chuyển về phía player
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            // Đặt lại thời gian kể từ lần bắn đạn cuối cùng
            timeSinceLastShot = 0f;
        }
    }


    ////float thoigianbatdau, thoigiangiua;
    ////bool trangthaiquay;
    //public GameObject dan;
    //public GameObject vitridan1;

    //public float lucbandan;
    //Rigidbody rgdan;

    //public bool xoay = false;

    //AudioSource AS;
    //public AudioClip enzin;

    //void Awake()
    //{
    //    AS = gameObject.AddComponent<AudioSource>();
    //    AS.clip = enzin;
    //    AS.volume = 1;
    //    AS.maxDistance = 5000;
    //}

    //// Use this for initialization
    //public void Start()
    //{
    //    //Debug.Log("thoigianbatdau :" + thoigianbatdau);
    //    StartCoroutine(XoayBan());
    //    AS.Play();
    //}
    //public IEnumerator bandan()
    //{
    //    int dem = 0;
    //    while (dem < 3)
    //    {
    //        yield return new WaitForSeconds(0.3f);
    //        GameObject taodanmoi1;

    //        taodanmoi1 = Instantiate(dan, vitridan1.transform.position, dan.transform.rotation) as GameObject;
    //        rgdan = taodanmoi1.GetComponent<Rigidbody>();

    //        // rgdan.AddForce(new Vector2(1,0) * lucbandan);
    //        rgdan.AddForce(-vitridan1.transform.forward * lucbandan);

    //        dem++;
    //    }
    //}
    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        Destroy(gameObject, 2f);
    //    }
    //}
    //public IEnumerator XoayBan()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        //Debug.Log("Dung");
    //        xoay = true;
    //        StopCoroutine(bandan());
    //        yield return new WaitForSeconds(1f);
    //        xoay = false;
    //        //Debug.Log("Ban");
    //        StartCoroutine(bandan());
    //    }
    //}
}
