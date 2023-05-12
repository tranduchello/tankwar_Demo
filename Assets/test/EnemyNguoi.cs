using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNguoi : MonoBehaviour
{
    public Transform player; // tham chiếu đến GameObject của player
    public List<Transform> waypoints; // danh sách các điểm waypoint
    public float stoppingDistance = 5f; // khoảng cách giữa enemy và player trước khi dừng lại
    public float shootingInterval = 5f; // thời gian bắn giữa các lần

    private NavMeshAgent agent; // component NavMeshAgent của enemy
    private float nextShootTime; // thời điểm bắn tiếp theo
    private int waypointIndex; // chỉ số của điểm waypoint hiện tại

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextShootTime = Time.time;
    }

    void Update()
    {
        // nếu player không tồn tại, không làm gì cả
        if (player == null)
        {
            return;
        }

        // nếu enemy đang di chuyển đến player, không làm gì cả
        if (agent.pathPending || agent.remainingDistance > stoppingDistance)
        {
            return;
        }

        // enemy đến gần player, dừng lại và nhắm bắn
        agent.isStopped = true;
        transform.LookAt(player);
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootingInterval;
        }

        // nếu đến thời điểm bắn tiếp theo, cho phép enemy di chuyển và chọn điểm waypoint tiếp theo
        if (Time.time >= nextShootTime + 2f)
        {
            agent.isStopped = false;
            waypointIndex++;
            if (waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    void Shoot()
    {
        // thực hiện bắn ở đây
        Debug.Log("Enemy shoots!");
    }
}
