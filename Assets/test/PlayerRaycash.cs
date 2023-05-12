using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycash : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject bullet_rocket;
    public Transform spw;
    public float lucbullet;
    public int khoangcach;

    private bool canFire = true;
    private int bulletCount = 0;
    private float fireDelay = 1f;
    private float fireTimer = 0f;
    //public GameObject Fire;
    //public GameObject Hitpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(FirePoint.position,transform.TransformDirection(Vector3.forward),out hit, khoangcach))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                int bulletCount = 0;
                while (bulletCount < 5)
                {
                    ButtonFire();
                    bulletCount++;
                }
                //ButtonFire();
            }
        }
        
    }
    public void ButtonFire()
    {
        GameObject bullet1 = poolPlayer.Instance.SpawnFromPool(bullet_rocket, spw.position, spw.rotation);
        bullet1.SetActive(true);
        bullet1.GetComponent<Rigidbody>().AddForce(spw.forward * lucbullet);
    }
}
