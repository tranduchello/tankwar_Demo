  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour {


    //public GameObject explosion;// là một đối tượng dùng để hiển thị hiệu ứng vụ nổ khi viên đạn va chạm với đối tượng.
    public int attackDamage = 5;
    EnemyHealth enemyHealth;
    public AudioSource buttetAudiosource;
    public GameObject explosion;// là một đối tượng dùng để hiển thị hiệu ứng vụ nổ khi viên đạn va chạm với đối tượng.
    // hàm này được gọi khi đối tượng đang va chạm với 1 đối tượng khác

    void Start () 
    { 
        buttetAudiosource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        // nếu đối tượng va chạm có tad là enemy
        if (other.gameObject.tag == "Enemy")
        {
            // nếu có âm thanh phát nổ tại vị trí hiện tại của đối tượng

            buttetAudiosource.Play();
           
            // lấy component EnemyHealth của đối tượng va chạm  vF GỌI HÀM tackDame()
            GameObject exp1 = poolPlayer.Instance.SpawnFromPool(explosion, transform.position, Quaternion.identity);
            exp1.SetActive(true);
            Destroy(exp1, 1f);
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            Attack();
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            poolPlayer.Instance.ReturnToPool(gameObject);
        }

        else if (other.gameObject.tag == "Ground")
        {
            GameObject exp1 = Instantiate(explosion, transform.position, Quaternion.identity);
            exp1.SetActive(true);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            poolPlayer.Instance.ReturnToPool(gameObject);
            Destroy(exp1,1f);

        }
        else if (other.gameObject != gameObject)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            poolPlayer.Instance.ReturnToPool(gameObject);
        }


    }
    void Attack()
    {
        // gây sát thương dame cho ennemy
        enemyHealth.TaKeDamage(attackDamage);
    }

}
