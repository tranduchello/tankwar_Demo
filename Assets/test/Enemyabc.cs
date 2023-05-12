using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class Enemyabc : MonoBehaviour
{


    public Transform[] waypoints;
    public GameObject enemyPrefab;
    public int spawnCount; // số lượng con enemy cần sinh ra
    public float spawnInterval; // thời gian giữa các lần sinh ra enemy
    public int maxEnemiesOnScreen ; // giới hạn số lượng enemy trên màn hình
    private int enemiesSpawned; // số lượng enemy đã sinh ra

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }
    void SpawnEnemy()
    {
       
        //Debug.Log("sinhra:" + spawnCount);
        //Debug.Log("sl:" + enemiesSpawned);
        if (enemiesSpawned >= spawnCount)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemiesOnScreen) // nếu đã có đủ số lượng enemy trên màn hình thì không sinh thêm
        {
            return;
        }

        Transform spawnPoint = waypoints[Random.Range(0, waypoints.Length)];
        GameObject solidors = poolPlayer.Instance.SpawnFromPool(enemyPrefab, spawnPoint.position, Quaternion.identity);
        solidors.SetActive(true);
        

        enemiesSpawned++;
    }
}

