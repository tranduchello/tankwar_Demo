using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gu : MonoBehaviour
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
        agent.updatePosition = true;
        agent.SetDestination(target.transform.position);
    }
}
