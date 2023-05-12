using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour {

    public float RotateSpeed = 15f;
    public GameObject Bullet_Effect;
    public AudioClip Bullet_E;
    public int puss_RK = 5;
    GameObject player;
    BulletController playerRocket;
   void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       // playerRocket = ;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0,RotateSpeed * Time.deltaTime,0);
	}
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
           
            if (Bullet_E)
            {
                AudioSource.PlayClipAtPoint(Bullet_E, transform.position, 600f);
            }
            GameObject rk_effct = Instantiate(Bullet_Effect, transform.position, Quaternion.identity);
            Debug.Log("phat hien va cham voi" + other.gameObject.name);
            PussRK();
           StartCoroutine( Destroy( rk_effct));
           

        }
    }
    IEnumerator Destroy(GameObject rk_effct)
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        Destroy(rk_effct, 1f);
    }
    void PussRK()
    {
        Debug.Log("player:" + player.transform.name);
        //tankplayer ko chua tep code bulletcontroller
        player.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<BulletController>().PlusBullet(puss_RK);
    }
}

    

    
