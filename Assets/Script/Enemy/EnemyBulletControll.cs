using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControll : MonoBehaviour {


    public int attackDamage = 20;// sát thương tấn công 

    GameObject player;// đối tượng người chơi 
    PlayerHealth playerHealth;// thông tin máu của người chơi
    EnemyHealth enemyHealth;// thông tin máu của kẻ địch
    bool playerInRange;// kiểm tra  xe, người chơi có nằm trong phạm vi tấn công của kẻ địch
    float time;// thời gian
    public AudioClip eShoot;// âm thanh khi enemy bắn 
    public GameObject explosion;// hiệu ứng
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");// tìm đối tượng của người chơi theo tag
        //Debug.Log("ten player:" + player.name);
        playerHealth = player.GetComponent<PlayerHealth>();// lấy thông tin về máu của người chơi
        enemyHealth = GetComponent<EnemyHealth>();// thông tin về máu của enemy
    }
    void OnCollisionEnter(Collision other)
    {
       // Debug.Log("da va cham;" + other.transform.name);
        //Debug.Log("da va cham;" +player.name);
        if (other.gameObject.name == player.name)// nếu va chạm vs người chơi 
        {
            if (eShoot)// phát ra âm thanh
            {
                AudioSource.PlayClipAtPoint(eShoot, transform.position, 500.0f);
            }
            // tạo hiệu ứng nổ ở vị trí hiện tại
            GameObject exp2 = Instantiate(explosion, transform.position, Quaternion.identity);
            // MusicAllGame.Instance.PlayMusicGame(6);//dung nay de phat
            gameObject.GetComponent<AudioSource>().Stop();// dừng âm thanh
            gameObject.GetComponent<AudioSource>().PlayOneShot(MusicAllGame.Instance.aClip[6]);


           //StartCoroutine(Camera.main.GetComponent<CameraController>().Rung());
            Destroy(gameObject);// hủy đối tượng 
            Destroy(exp2, 1.5f);// hủy  hiệu ứng nổ sau 1.5 giây
           // Debug.Log("da va cham;" + other.transform.name);
            //Debug.Log("va cham:");
            Attack();// tấn công người chơi 
            playerInRange = true;// người chơi trong phạm vi  của enemy

        }
        // trường hợp khác nếu chạm với đất 
        else if(other.gameObject.tag == "Ground")
        {
            if (eShoot)// nếu phát ra  âm thanh
            {
                AudioSource.PlayClipAtPoint(eShoot, transform.position, 500.0f);
            }
            // Tạo hiệu ứng nổ tại vị trí hiện tại
            GameObject exp2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(exp2, 1.5f);
        }
        // nếu đối tượng va chạm khác với đối tượng hiện tại
        else if(other.gameObject != gameObject)
        {
            // thì phát âm thanh bắn
            if (eShoot)
            {
                AudioSource.PlayClipAtPoint(eShoot, transform.position, 500.0f);
            }
            GameObject exp2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(exp2, 1.5f);
        }
    }
    // khi rời khỏi trigger
     void OnTriggerExit(Collider other)
    {
        // nếu đối tượng là player 
        if(other.gameObject == player)
        {
            // thì tấn công
            Debug.Log("va cham ");
            Attack();
            playerInRange = false;
        }
        
    }
    // Use this for initialization
    void Start () 
    {
        Destroy(gameObject, 3f);// hủy đối tượng 3s
		
	}
    void Attack()
    {
        //Debug.Log("attck");
        // gây sát thương cho player
            playerHealth.TakeDamage(attackDamage);

    }
}
