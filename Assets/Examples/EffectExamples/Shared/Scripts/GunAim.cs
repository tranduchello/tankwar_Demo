using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class GunAim:MonoBehaviour
{
    NavMeshAgent agent;// điều khiển đường đi 
    Transform target;//  vị trí người chơi 
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        //agent.updatePosition = true;
        agent.SetDestination(target.position);
    }
}

