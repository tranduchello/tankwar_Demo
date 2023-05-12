using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dandichuyentheoplayer : MonoBehaviour {

    public float speed;
    private Transform player;
    private Vector3 target;

    public float damage = 10;

    public AudioClip bom;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (transform.position.x == target.x &&
            transform.position.y == target.y &&
            transform.position.z == target.z)
        {
            DestroyProjecttile();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("da va cham");
            DestroyProjecttile();
        }
    }

    void DestroyProjecttile()
    {
        Destroy(gameObject);
    }
}
