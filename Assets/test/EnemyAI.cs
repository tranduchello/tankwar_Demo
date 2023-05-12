using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;// khai báo biến 
    //NavMeshAgent agent;// điều khier tìm đường 
    Animator anim;// khai basoo hành động
    //public float shootDistance = 50f;// khoảng cách bắn

    public GameObject bulletPrefab; // khai báo đạn
    public GameObject vitri;// vị trí bắnđạn
    public float bulletSpeed; // tốc độ đạn

    private float timeSinceLastShot = 0f; // thời gian lần đạn bắn cuối cùng
    float shootingTime = 0f;// thời gian đang bắn
                            // float movingTime = 0f;// thời gian đang di chuyển
    bool isShooting = false;// đang bắn
    bool isWaiting = false;// đợi bắn
    Vector3 randomPosition;// khởi tạo vị trí hiện tại của đjch 



    float distanceToPlayer;
    public float shootingRange = 250f; // Khoảng cách tầm bắn của enemy
    public float minDistanceToPlayer = 200f; // Khoảng cách tối thiểu giữa enemy và player trước khi enemy dừng và random đến điểm mới
    public float movingTime = 5f; // Thời gian di chuyển enemy đến điểm mới
    public float minRandomDistance = 210f; // Khoảng cách tối thiểu giữa điểm mới và player trước khi enemy di chuyển đến điểm mới
    public float maxRandomDistance = 250f; // Khoảng cách tối đa giữa điểm mới và player trước khi enemy di chuyển đến điểm mới

    private NavMeshAgent navMeshAgent;// khai báo biến Nav
    private Transform playerTransform;// để lưu vị trí player
    private bool isMovingToNewPosition = false;//xác định enemy đang di chuyển đến điểm mới hay không.
    private float currentMovingTime = 0f;//để lưu trữ thời gian di chuyển của enemy đến điểm mới.
    private Vector3 newPosition;//vị trí của điểm mới.
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;// tìm player theo tag
        anim.SetTrigger("walk");
        
        //navMeshAgent.stoppingDistance = minDistanceToPlayer;// dừng enemy lại 


    }
    void Update()
    {
        //Tính toán khoảng cách giữa enemy và player.
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.transform.position);
        //navMeshAgent.SetDestination(newPosition);
        //Nếu khoảng cách giữa enemy và player lớn hơn tầm bắn của enemy hoặc nhỏ hơn khoảng cách tối thiểu giữa enemy và player
        if (distanceToPlayer >= minDistanceToPlayer && distanceToPlayer < shootingRange)
        {

            print("dung");

           // navMeshAgent.isStopped = true;
            anim.ResetTrigger("walk");
            anim.SetTrigger("attack");
            //Run(); 

        }
        //Ngược lại, nếu khoảng cách giữa enemy và player nằm trong khoảng tầm bắn của enemy và lớn hơn khoảng cách tối thiểu giữa enemy và player

        else if (distanceToPlayer < minDistanceToPlayer)
        {
            newPosition = FindNewPosition();


        }
        else if (distanceToPlayer <= minDistanceToPlayer && distanceToPlayer <= shootingRange)
        {


            print("sai");
            //navMeshAgent.stoppingDistance = shootingRange;
            if (!isMovingToNewPosition)// Nếu enemy chưa di chuyển đến điểm mới
            {
                navMeshAgent.SetDestination(playerTransform.transform.position);
                StartCoroutine(MoveToNewPosition());
                currentMovingTime = movingTime;
                // Gán thời gian di chuyển của enemy đến điểm mới cho currentMovingTime.

            }
        }
    }
    IEnumerator MoveToNewPosition()
    {
        isMovingToNewPosition = true;// enemy đang trong quá trình di chuyển đến vị trí mới.

        while (currentMovingTime > 0f)
        {
            currentMovingTime -= Time.deltaTime;//để giảm thời gian di chuyển dần xuống cho đến khi nó bằng 0.
            yield return null;
        }
        // sử dụng hàm FindNewPosition() để tìm một vị trí mới cho enemy di chuyển đến
        newPosition = FindNewPosition();

        //đặt đích đến của NavMeshAgent tới vị trí mới tìm được
        navMeshAgent.SetDestination(newPosition);
        //để đợi 5 giây trước khi đánh dấu rằng enemy đã đến vị trí mới và quá trình di chuyển kết thúc

        // enemy di chuyển đến vị trí mới tiếp theo
        isMovingToNewPosition = false;
    }
    public void Run()
    {
        //navMeshAgent.isStopped = true;
        //navMeshAgent.destination = transform.position;
        //anim.ResetTrigger("walk");
        //anim.SetTrigger("attack");
    }
    public void StopBack()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minRandomDistance, maxRandomDistance);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(randomDirection);

    }
    public void Radisus()
    {
        Vector3 playerPosition = playerTransform.transform.position;
        // Tính toán vị trí mới của enemy cách player 5 đơn vị
        Vector3 directionToPlayer = gameObject.transform.position - playerPosition;
        Vector3 newPosition = gameObject.transform.position + directionToPlayer.normalized * 5f;
        navMeshAgent.SetDestination(newPosition);
    }

    private Vector3 FindNewPosition()
    {
        // tạo một vector randomDirection ngẫu nhiên trong phạm vi giữa khoảng cách minRandomDistance và maxRandomDistance.
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minRandomDistance, maxRandomDistance);
        // thêm vị trí của player vào vector randomDirection để enemy không đi quá xa khỏi player.
        randomDirection += playerTransform.position;

        //tìm một điểm trên NavMesh gần vị trí randomDirection, và lưu trữ vị trí đó trong NavMeshHit hit
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxRandomDistance, NavMesh.AllAreas);
        // trả về vị trí được tìm thấy
        return hit.position;
    }

    public void shoot()
    {
        timeSinceLastShot += Time.deltaTime;

        // If it's time for the next shot
        if (timeSinceLastShot >= 0.2) // You can change this value to fire a bullet every x seconds
        {
            // Create a new bullet object
            GameObject bullet = poolPlayer.Instance.SpawnFromPool(bulletPrefab, vitri.transform.position, Quaternion.identity);
            bullet.SetActive(true);

            // Rotate the bullet towards the player
            bullet.transform.LookAt(target);

            // Push the bullet towards the player
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            // Reset the time since last shot
            timeSinceLastShot = 0f;
        }
        else
        {
            return;
        }
    }

    public void EnemyDeathAnim()
    {
        anim.SetTrigger("Dead");
    }
}
