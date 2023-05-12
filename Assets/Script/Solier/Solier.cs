using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solier : MonoBehaviour {

    public GameObject dan;
    public GameObject vitridan1;
    public GameObject Player;

    public float lucbandan;// lực bắn của đạn
    Rigidbody rgdan;

    public bool xoay = false;// kiểm tra xoay


    // Use this for initialization
    public void Start()
    {
        //thoigianbatdau = Time.time;
        //Debug.Log("thoigianbatdau :" + thoigianbatdau);
        // chạy hàm xoay bắn
        StartCoroutine(XoayBan());
    }

    // Update is called once per frame
    void Update()
    {

        if (xoay)
        {
            //transform.Rotate(Player.transform.position, transform.position);
            transform.Rotate(new Vector3(0, 0, 1), 1 * Time.deltaTime);
        }
    }
    public IEnumerator bandan()
    {
        int dem = 0;
        while (dem < 1)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject taodanmoi1;

            taodanmoi1 = Instantiate(dan, vitridan1.transform.position, dan.transform.rotation) as GameObject;
            rgdan = taodanmoi1.GetComponent<Rigidbody>();

            // rgdan.AddForce(new Vector2(1,0) * lucbandan);
            rgdan.AddForce(vitridan1.transform.forward * lucbandan);

            dem++;
        }

    }
    public IEnumerator XoayBan()
    {
        while (true)
        {

            yield return new WaitForSeconds(1f);
            //Debug.Log("Dung");
            xoay = true;
            StopCoroutine(bandan());
            yield return new WaitForSeconds(1f);
            xoay = false;
            //Debug.Log("Ban");
            StartCoroutine(bandan());
        }
    }
}
