using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using System;

public class poolPlayer : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }
    [SerializeField]
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public static poolPlayer Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = CreateObj(pool.prefab);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.name, objectPool);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public GameObject SpawnFromPool(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(gameObject.name))
        {
            return null;
        }
        GameObject objectToSpawn;
        if (PoolDictionary[gameObject.name].Count == 0)
        {
            objectToSpawn = CreateObj(gameObject); // extend more gameobject
        }
        else
        {
            objectToSpawn = PoolDictionary[gameObject.name].Dequeue();
        }
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        return objectToSpawn;
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        PoolDictionary[obj.name].Enqueue(obj);
       
    }
    //public void ResetObject(GameObject obj)
    //{
    //    // Reset object's properties and components as needed
    //    // Đặt lại các thuộc tính và thành phần của đối tượng nếu cần
    //    // Ví dụ: bạn có thể đặt lại vị trí, xoay, sức khỏe của nó, v.v.
    //    // For example, you might reset its position, rotation, health, etc.

    //    obj.transform.position = Vector3.zero;
    //    obj.transform.rotation = Quaternion.identity;
    //    // PoolDictionary[obj.name].Dequeue();
    //    if (enemyHealth != null)
    //    {
    //        enemyHealth.ResetEnemyHealth();
    //    }





    //}
    #region 
    public GameObject CreateObj(GameObject gameObject)
    {
        GameObject obj = Instantiate(gameObject, this.transform);
        obj.name = gameObject.name;
        obj.SetActive(false);
        return obj;
    }
    #endregion
}
