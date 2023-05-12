using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
public enum CharacterState
{
    Idle,// dừng
    Move,// di chuyển
    RandomDestination,// di chuyển ngẫu nhiên
    Shoot, // bắn
    check
}
public class NavShess : MonoBehaviour
{
    private NavMeshAgent nma = null;
    Animator anim;// khai basoo hành động
    private Transform playerTransform;// để lưu vị trí player
    private Bounds bndFloor;// khu vực sàn trong game
    private Vector3 moveto;// đại diện cho điểm cần di chuyển đến
    private LineRenderer line = null;// vẽ đường thẳng từ vị trí hiện tại củ đối tượng đến đích
    private bool flag = false;// kiểm tra xem đã đến nơi hay chưa
    public float movingTime = 0.5f; // Thời gian di chuyển enemy đến điểm mới+
    private GameObject pole = null;

    public bool firstMove;
    public GameObject bulletPrefab; // khai báo đạn
    public GameObject vitri;// vị trí bắnđạn
    public float bulletSpeed; // tốc độ đạn

    private float timeSinceLastShot = 0f; // thời gian lần đạn bắn cuối cùng 
    float fireRate = 0.5f;
    public bool isetrandom;
    public bool isetShoot;
    public bool itemCheck;
    public bool isetfirstMoveAction;
    public int counter = 0;
    public CharacterState currentState = CharacterState.Idle;

    public void OnEnable()
    {
        nma = this.GetComponent<NavMeshAgent>();
        //Debug.Log("di chuyen 3");
        anim = GetComponent<Animator>();
        bndFloor = GameObject.Find("floor").GetComponent<Renderer>().bounds;
        pole = GameObject.Find("pole");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;// tìm player theo tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;// tìm player theo tag
        isetfirstMoveAction = false;

        //line = this.gameObject.AddComponent<LineRenderer>();
        //line.material = new Material(Shader.Find("Sprites/Default"));
        //line.widthMultiplier = 0.2f;



        //instantiate a line object
        //    line = this.gameObject.AddComponent<LineRenderer>();
        //    line.material = new Material(Shader.Find("Sprites/Default"));
        //    line.widthMultiplier = 0.2f;

        currentState = CharacterState.Move;// di chuyển
    }
    private void Start()
    {
       
    }
    private void Update()
    {
        switch (currentState)
        {
            case CharacterState.Idle:
                IdleMove();// kiểm tra 
                break;
            case CharacterState.Move:// di chuyển 
                if (!isetfirstMoveAction)
                {
                    //Debug.Log("sadgh");
                    isetfirstMoveAction = true;
                    StartCoroutine(firstMoveAction());
                }
                break;
            case CharacterState.RandomDestination:// di chuyển đến điểm random

                if (!isetrandom)
                {

                    //Debug.Log("random1");
                    isetrandom = true;
                    StartCoroutine(SetRandomDestination());
                    //IdleMove();
                }

                break;
            case CharacterState.Shoot:// bắn.
                                      //if (!isetShoot)
                                      //{
                                      //isetShoot = true;
                shoot();
                //}

                break;
            case CharacterState.check:// kiểm tra xem có đi đúng k

                    CheckPointOnPath();
                
                break;
        }
    }
    void IdleMove()
    {

        if (nma.hasPath == false && isetrandom == false)
        {
            //Debug.Log("den");
           currentState = CharacterState.check;
            StartCoroutine(SetRandomDestination());

        }
        else if (isetrandom == true)
        {
            //Debug.Log("dung");
            currentState = CharacterState.Shoot;
            //shoot();

        }
    }
    IEnumerator firstMoveAction()// xử lý di chuyển 
    {
        //Debug.Log("di chuyen");
        anim.SetTrigger("walk");
        float rx = Random.Range(-100, 100);
        float rz = Random.Range(-100, 100);
        moveto = new Vector3(rx, playerTransform.position.y, rz);

        nma.SetDestination(moveto); //figure out path, starts gameobject moving
        yield return new WaitForSeconds(1f);
        //firstMove = false;
        currentState = CharacterState.Idle;
        pole.transform.position = new Vector3(moveto.x, pole.transform.position.y, moveto.z);

    }

    IEnumerator SetRandomDestination()// random ngãu nhiên đến điểm mới
    {

        //flag = true;
        currentState = CharacterState.Shoot;
        //Debug.Log("random");
        anim.SetTrigger("attack");
        anim.ResetTrigger("walk");
        yield return new WaitForSeconds(10f);

        //1. pick a point
        //anim.SetTrigger("walk");

        float rx = Random.Range(-100, 100);
        float rz = Random.Range(-100, 100);
        moveto = new Vector3(rx, this.transform.position.y, rz);
        nma.SetDestination(moveto); //figure out path, starts gameobject moving
                                    //2. show the destination  hiển thị điểm đến
        pole.transform.position = new Vector3(moveto.x, pole.transform.position.y, moveto.z);
        anim.ResetTrigger("attack");
        anim.SetTrigger("walk");
        //3. draw line (#1 tutorial)
        //if (nma.path.corners.Length >= 1)
        //{
        //    line.positionCount = nma.path.corners.Length;
        //    for (int i = 0; i < nma.path.corners.Length; i++)
        //    {
        //        line.SetPosition(i, nma.path.corners[i]);
        //    }
        //}
        isetrandom = false;
        currentState = CharacterState.Idle;
        yield break;



    }
    public void CheckPointOnPath()// kiểm tra 
    {

        Debug.Log("diem");
        anim.ResetTrigger("attack");
        anim.SetTrigger("walk");
        //3. draw line (#1 tutorial)
        //if (nma.path.corners.Length >= 1)
        //{
        //    line.positionCount = nma.path.corners.Length;
        //    for (int i = 0; i < nma.path.corners.Length; i++)
        //    {
        //        line.SetPosition(i, nma.path.corners[i]);
        //    }
        //}
        //4. check
        if (nma.pathEndPosition != moveto)
        {
            //point is not on navmesh!!! tadaaa!
            //SetRandomDestination();
            currentState = CharacterState.RandomDestination;
        }
    }
    public void shoot()// bắn đạn
    {
        Vector3 lookVector = playerTransform.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);


        // If it's time for the next shot
        if (Time.time > timeSinceLastShot) // You can change this value to fire a bullet every x seconds
        {
            timeSinceLastShot = Time.time + fireRate;
            //Debug.Log("ban");
            //    // Create a new bullet object
            GameObject bullet = poolPlayer.Instance.SpawnFromPool(bulletPrefab, vitri.transform.position, Quaternion.identity);
            bullet.SetActive(true);

            // Rotate the bullet towards the player
            bullet.transform.LookAt(playerTransform);

            // Push the bullet towards the player
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            // Reset the time since last shot
            //timeSinceLastShot = 0f;
        }
    }

    public void EnemyDeathAnim()
    {
        anim.SetTrigger("Dead");
    }
}
