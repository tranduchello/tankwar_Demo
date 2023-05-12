using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BulletController : MonoBehaviour {


    public GameObject  bullet_rocket;// đạn  thường 
    public GameObject bullet_Fire;// đạn đuổi 
    public Transform spw;// vị trí sinh đạn thường
    public Transform spw2;// vt sinh đạn đuổi
    public float luc = 200f;// lực đuổi 
    public float lucbullet=300f;// lực thường

    public int Rocket = 10;// số rocket
    bool bandan;
    bool numberAmmo;// kiểm tra đạn thường 
    bool PussRocket;
    [SerializeField] int ammoLimit;// giới hạn số lượng 

    // Update is called once per frame

    void Update () 
    {

        if (ammoLimit > 0)
        {
            numberAmmo = true;
            if (Input.GetMouseButtonDown(1))
            {

                ButtonFire();
                ammoLimit--;
            }
            else if (ammoLimit <= 0)
            {
                numberAmmo=false;
            }
        }
        if (Rocket > 0)
        {
            bandan = true;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ButtRocket();
                Rocket--;
            }
            else if(Rocket <= 0){

                bandan = false;
            }
        }
    }
    public void ButtonFire()
    {
            GameObject bullet1 = poolPlayer.Instance.SpawnFromPool(bullet_rocket, spw.position, spw.rotation);
            bullet1.SetActive(true);
            bullet1.GetComponent<Rigidbody>().AddForce(spw.forward * lucbullet);
    }
    public void ButtRocket()
    {
        GameObject bullet3 = poolPlayer.Instance.SpawnFromPool(bullet_Fire, spw2.position, spw2.rotation);
        bullet3.SetActive(true);
        bullet3.GetComponent<Rigidbody>().AddForce(spw2.forward * luc);
    }
    public void PlusBullet(int PussB)
    {
        //debug cho anh tai day
        Debug.Log("A");
        if(Rocket < 10)
        {
            PussRocket = true;
            Rocket += PussB;
            Debug.Log("B" + Rocket);
        }
        else if(Rocket>= 10)
        {
            PussRocket = false;
            //Debug.Log("C:");
        }
    }
}
