using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    Transform target;//  vị trí người chơi 
    NavMeshAgent agent;// điều khiển đường đi 
    Animator anim;// xử lý hnahf động
    public float shootDistance = 50f;
    public float rotationSpeed = 5.0f;
    //public GameObject bulletPrefab;
    // Start is called before the first frame update


    public GameObject bulletPrefab; // Đối tượng đạn
    public GameObject vitri;
    public GameObject player; // Transform của player
    public float bulletSpeed; // Tốc độ di chuyển của viên đạn

    private float timeSinceLastShot = 0f; // Thời gian kể từ lần bắn đạn cuối cùng
    public int bulletCount;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);// tính toán khoảng cách 

        if (distance > shootDistance)// nếu khoảng cách > 
        {
            Vector3 Distance = (target.position - transform.position).normalized;
            agent.updatePosition = true;
            anim.SetTrigger("walk");
            agent.SetDestination(target.transform.position);

        }

        else
        {

            agent.updatePosition = false;
            anim.ResetTrigger("walk");
            anim.SetTrigger("attack");
            // nhìn vào playerd
            transform.LookAt(target.transform.position);
            shoot();
            if (bulletCount == 10)
            {
                print("vao vao ");
                agent.SetDestination(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)));
                bulletCount = 0;
            }

        }

    }

    public void shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= 1f && bulletCount < 10)
        {
            GameObject bullet = Instantiate(bulletPrefab, vitri.transform.position, Quaternion.identity);
            bullet.transform.LookAt(target);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            timeSinceLastShot = 0f;
            bulletCount++;
        }

       
    }
    public void EnemyDeathAnim()
    {
        anim.SetTrigger("Die");
    }
}
