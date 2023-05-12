using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thabom : MonoBehaviour
{

    //float thoigianbatdau, thoigiangiua;
    //bool trangthaiquay;
    public GameObject dan;
    public GameObject vitridan1;
    public GameObject vitridan2;
    public float lucbandan;
    Rigidbody rgdan;

   public bool xoay = false;


    // Use this for initialization
    public void Start()
    {
        //thoigianbatdau = Time.time;
        //Debug.Log("thoigianbatdau :" + thoigianbatdau);
        StartCoroutine(XoayBan());
    }
    public IEnumerator bandan()
    {
        int dem = 0;
        while (dem < 1)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject taodanmoi1;
            GameObject taodanmoi2;
            taodanmoi1 = Instantiate(dan, vitridan1.transform.position, dan.transform.rotation) as GameObject;
            rgdan = taodanmoi1.GetComponent<Rigidbody>();
            taodanmoi2 = Instantiate(dan, vitridan2.transform.position, dan.transform.rotation) as GameObject;
            rgdan = taodanmoi2.GetComponent<Rigidbody>();
            // rgdan.AddForce(new Vector2(1,0) * lucbandan);
            rgdan.AddForce(-vitridan1.transform.up * lucbandan);
            rgdan.AddForce(-vitridan2.transform.up * lucbandan);
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
    
