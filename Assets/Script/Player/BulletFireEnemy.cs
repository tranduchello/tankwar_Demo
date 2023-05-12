using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireEnemy : MonoBehaviour {

    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;//  là một ParticleSystem (hệ thống hạt) dùng để hiển thị hiệu ứng vụ nổ.
    public float m_MaxDamage = 100f;// là mức sát thương tối đa của viên đạn.
    public float m_ExplosionForce = 100f;//là lực tác động của vụ nổ.
    public float m_MaxLifeTime = 2f;// : là thời gian tối đa để viên đạn tồn tại trên màn hình trước khi tự hủy.
    public float m_ExplosionRadius = 0.5f;// là bán kính của vụ nổ.

    public GameObject explosion;// là một đối tượng dùng để hiển thị hiệu ứng vụ nổ khi viên đạn va chạm với đối tượng.

    public float force = 1000;//là lực tác động của viên đạn khi được bắn ra.

    Vector3 targer;//là lực tác động của viên đạn khi được bắn ra.
    public int attackDamage = 5;
    PlayerHealth playerHealth;
    AudioSource audioSourceAmmoEnemy;
    // Use this for initialization


    // gọi bắn đạn , đưa đối tượng đến một vị trí point

    private void Start()
    {
        audioSourceAmmoEnemy = GetComponent<AudioSource>();
    }
    public void FireBullet(Vector3 point)
    {
        // tính khoảng cách  giữa điểm point và vị trí hiện tại của đối tượng
        float dis = Vector3.Distance(point, transform.position);
        // tính toán 1 vị trí mới tương đối với điểm point để đối tượng di chuyển tới đó
        targer = new Vector3(point.x, point.y + dis / 20, point.z);


        //    // thêm 1 lực để đẩy đối tượng điều hướng đến vị trí mới 
        gameObject.GetComponent<Rigidbody>().AddForce((targer - transform.position).normalized * force);
    }
    //// // Hàm này được gọi để đẩy đối tượng theo hướng huong
    public void hut(Vector3 huong)
    {
        // thêm một lực để đẩy đối tượng theo hướng 
        gameObject.GetComponent<Rigidbody>().AddForce(huong * force);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// 

    //// hàm này được gọi khi đối tượng đang va chạm với 1 đối tượng khác
    void OnCollisionEnter(Collision other)
    {
        // nếu đối tượng va chạm có tad là enemy
        if (other.gameObject.tag == "Player")
        {
            // nếu có âm thanh phát nổ tại vị trí hiện tại của đối tượng
            // lấy component EnemyHealth của đối tượng va chạm  vF GỌI HÀM tackDame()
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            GameObject exp1 = poolPlayer.Instance.SpawnFromPool(explosion, transform.position, Quaternion.identity);

            exp1.SetActive(true);
            Destroy(exp1, 1f);
            audioSourceAmmoEnemy.Play();    

            Attack();

            poolPlayer.Instance.ReturnToPool(gameObject);
            Rigidbody rg = GetComponent<Rigidbody>();
            rg.velocity = Vector3.zero;

        }

        else if (other.gameObject.tag == "Ground")
        {
            GameObject exp1 = poolPlayer.Instance.SpawnFromPool(explosion, transform.position, Quaternion.identity);
            exp1.SetActive(true);
            Destroy(exp1, 1f);
            poolPlayer.Instance.ReturnToPool(gameObject);
            Rigidbody rg = GetComponent<Rigidbody>();
            rg.velocity = Vector3.zero;

        }
        else if(other.gameObject != gameObject)
        {
            GameObject exp1 = poolPlayer.Instance.SpawnFromPool(explosion, transform.position, Quaternion.identity);
            exp1.SetActive(true);
            Destroy(exp1,1f);
            poolPlayer.Instance.ReturnToPool(gameObject);
            Rigidbody rg = GetComponent<Rigidbody>();
            rg.velocity = Vector3.zero;
        }
        else
        {
            StartCoroutine(DeactivateBullet());

        }


    }
    IEnumerator DeactivateBullet()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    void Attack()
    {
        // gây sát thương dame cho ennemy
        playerHealth.TakeDamage(attackDamage);
    }

}
