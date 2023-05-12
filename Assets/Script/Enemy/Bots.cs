using UnityEngine;
using System.Collections;

public class Bots : MonoBehaviour
{
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    float speed;
    float finalSpeed = 1;
    public GameObject turret;
    public Transform[] rayOrigin;
    GameObject player;
    float distance;
    Vector3 playerPos;
    float timeToFind;
    bool patrol, avoidObj;
    float nextTimeRotate, timeRotate, finalSpeedRotate;
    bool rotating;

    public Transform[] rightWheels, leftWheels; //banh xe tang
    float currentMoveSpeed;//toc do di chuyen theo time
    float currentRotateSpeed;
    float finalMoveSpeed;//toc do dat duoc


    public GameObject cannonBulletBotPrefab, spawnCannonBulletPos;
    float timeShoot;

    void Awake()
    {
        // playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    // Use this for initialization
    void Start()
    {
        //AssignPlayer();
        patrol = true;
        avoidObj = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, finalMoveSpeed, Time.deltaTime);// time.deltatime <=>(1.5f/1.5f)* Time*deltaTime
        transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime);

        currentRotateSpeed = Mathf.MoveTowards(currentRotateSpeed, finalMoveSpeed, (360 / 7 / 1.5f) * Time.deltaTime);
        transform.Rotate(Vector3.up * currentRotateSpeed * Time.deltaTime);
        Vector3 newTurretDirection = Vector3.RotateTowards(turret.transform.forward, player.transform.position - transform.position, 0.5f * Time.deltaTime, 0.0f);
        turret.transform.rotation = Quaternion.LookRotation(newTurretDirection);
        Debug.Log("Patrol: " + patrol + "  avoidObj: " + avoidObj + "   finalSpeed" + finalSpeed);
        speed = Mathf.MoveTowards(speed, finalSpeed, Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (patrol == true)
        {
            GetRotate();
            RotateBot();
        }


        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 50)
        {

            Ray ray = new Ray(transform.position, player.transform.position - transform.position);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                //Neu nhin thay Player
                if (hit.collider.tag == "Player")
                {

                    playerPos = hit.transform.position;
                    //avoidObj = false;			// Da nhin thay, khong can auto ne tuong nua

                    timeToFind = 10;        // Set thoi gian tim kiem khi khong nhin thay players
                                            //look to player
                    
                    //if (Mathf.Abs(turret.transform.forward - player.transform.position + transform.position) <) {
                    StartCoroutine(Example(3));
                    //AutoSHoot ();
                    //}
                    if (distance > 10)
                    {
                        patrol = false;         // Khong can di tuan tra nua
                        Vector3 newTankDirection = Vector3.RotateTowards(transform.forward, player.transform.position - transform.position, Time.deltaTime, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newTankDirection);
                    }
                    else
                    {
                        patrol = true;
                        //avoidObj = true;
                    }
                }
                //Neu khong con nhin thay Player
                else
                {

                    if (timeToFind > 0)
                    {
                        Vector3 lastPlayerPos = playerPos;
                        timeToFind = Mathf.MoveTowards(timeToFind, 0, Time.deltaTime); // Thoi gian tim kiem Player giam dan trong 5s
                                                                                       //Tim kiem Player
                                                                                       //Xoay xe tang ve lastPlayerPos;
                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, lastPlayerPos - transform.position, Time.deltaTime, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDirection);
                        //Neu goc xoay nho hon 40 do , movetorward den do
                    }
                    else
                    {
                        patrol = true;
                        nextTimeRotate = Time.time + 3; // Cho them 3s moi di tuan tro lai
                    }

                }
            }
        }


        RaycastHit wallHit;
        Ray AIRay1 = new Ray(rayOrigin[0].position, rayOrigin[0].forward);
        Ray AIRay2 = new Ray(rayOrigin[1].position, rayOrigin[1].forward);
        Ray AIRay3 = new Ray(rayOrigin[2].position, rayOrigin[2].forward);
        Ray AIRay4 = new Ray(rayOrigin[3].position, rayOrigin[3].forward);

        if (avoidObj == true)
        {

            if (Physics.Raycast(AIRay1, out wallHit, 1))
            {

                patrol = false;
                if (wallHit.collider.transform.root.position != transform.position)
                {

                    transform.Rotate(Vector3.up * -40 * Time.deltaTime);
                    Debug.DrawRay(AIRay1.origin, AIRay1.direction * 20, Color.red);
                }
            }
            else patrol = true;


            if (Physics.Raycast(AIRay1, out wallHit, 1) && Physics.Raycast(AIRay2, out wallHit, 1))
            {
                finalSpeed = -1;
            }
            else
            {
                finalSpeed = 1;
            }

            if (Physics.Raycast(AIRay2, out wallHit, 1))
            {
                patrol = false;
                if (wallHit.collider.transform.root.position != transform.position)
                {
                    transform.Rotate(Vector3.up * 40 * Time.deltaTime);
                    Debug.DrawRay(AIRay2.origin, AIRay2.direction * 20, Color.red);
                }
            }
            else patrol = true;

            if (Physics.Raycast(AIRay3, out wallHit, 1))
            {
                patrol = false;
                if (wallHit.collider.transform.root.position != transform.position)
                {
                    transform.Rotate(Vector3.up * 20 * Time.deltaTime);
                    Debug.DrawRay(AIRay3.origin, AIRay3.direction * 20, Color.red);
                }
            }
            else patrol = true;

            if (Physics.Raycast(AIRay4, out wallHit, 1))
            {
                patrol = false;
                if (wallHit.collider.transform.root.position != transform.position)
                {
                    transform.Rotate(Vector3.up * -20 * Time.deltaTime);
                    Debug.DrawRay(AIRay4.origin, AIRay4.direction * 20, Color.red);
                }
            }
            else patrol = true;
            //}
        }

        void AssignPlayer()
        {
            player = GameObject.Find("TankPlayer");
        }

        void GetRotate()
        {
            if (Time.time > nextTimeRotate)
            {
                nextTimeRotate = Time.time + Random.Range(4f, 8f);
                timeRotate = Time.time + Random.Range(1f, 3f);
                finalSpeedRotate = Random.Range(-1.0f, 1.0f);
                rotating = true;
            }
        }

        void RotateBot()
        {
            if (rotating == true)
            {
                transform.Rotate(Vector3.up * 40 * finalSpeedRotate * Time.deltaTime);
                if (Time.time > timeRotate)
                    rotating = false;
            }
        }


        void AutoSHoot()
        {
            if (Time.time > timeShoot)
            {
                timeShoot = Time.time + Random.Range(6f, 9f);
                GameObject cannonBuletBot = Instantiate(cannonBulletBotPrefab, spawnCannonBulletPos.transform.position, Quaternion.identity) as GameObject;
                //cannonBuletBot.GetComponent<CannonBulletController> ().RecieverCannonWay (playerPos);
                cannonBuletBot.GetComponent<Rigidbody>().AddForce(spawnCannonBulletPos.transform.forward * 1000);
            }
        }

        IEnumerator Example(float s)
        {
            yield return new WaitForSeconds(s);
            AutoSHoot();
        }

    }
}