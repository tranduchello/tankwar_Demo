using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBox : MonoBehaviour {
    public float RotateSpeed = 15;
    public GameObject HP_Effect;
    public int pussHP = 50;

    public AudioClip HP;
    GameObject player;
    PlayerHealth playerHP;

    

     void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<PlayerHealth>();
    }
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Player")
        {
            if (HP)
            {
                AudioSource.PlayClipAtPoint(HP, transform.position, 600f);
            }
            Puss();
            GameObject hp_effct = Instantiate(HP_Effect, transform.position, Quaternion.identity);
            Debug.Log("va cham voi player");
            Destroy();
            Destroy(hp_effct, 1f);
            
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
    void Puss()
    {
        playerHP.PusHealth(pussHP);
    }
}
