using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bullet : MonoBehaviour {

    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    //public AudioSource m_explosionAudio;
    public AudioClip Shoot;
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 10f;
    public float m_ExplosionRadius = 5f;

    public GameObject explosion;
    

    public float force = 1000;
    public GameObject[] EnemyObjs;
    Vector3 targer;
    bool status = false;
    public GameObject FoundObject;
    public float tocdoduoi = 6f;
    // Use this for initialization

    public int attackDamage = 100;
    GameObject enemy;
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;
    bool enemyInRange;

    public Text EnemyText;

    void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
       
        playerHealth = GetComponent<PlayerHealth>();
    }



    void Start () {
        GameObject obj = GameObject.Find("DoituongCantim");
        //m_explosionAudio = GetComponent<AudioSource>();

        Debug.Log("obj. con:" + obj.transform.childCount);
       // EnemyText.text = "" + obj.transform.childCount;
        if (obj.transform.childCount != 0)
            EnemyObjs = new GameObject[obj.transform.childCount];
      
        for (int i=0;i<obj.transform.childCount;i++)
        {
            EnemyObjs[i] = obj.transform.GetChild(i).gameObject;
           
        }
        poolPlayer.Instance.ReturnToPool(gameObject);
        //Destroy(gameObject, m_MaxLifeTime);

	}
	
	// Update is called once per frame
	void Update () {
        if (status == false)
        {
            for (int i = 0; i < EnemyObjs.Length; i++)
            {
                //tinh khoang cach tu vien dan den cac doi tuong enemy neu <2f thi duoi theo doi tuong do
                float distanceObj = Vector3.Distance(gameObject.transform.position, EnemyObjs[i].transform.position);
                if (distanceObj < 100)
                {
                    FoundObject = EnemyObjs[i];

                    Debug.Log("tim thay doi tuong:" + FoundObject.transform.name);
                   
                    status = true;
                    break;
                }
            }
        }
        else
        {
            // Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<Rigidbody>().Sleep();
            //lap trinh duoi
            //gameObject.transform.tr
            //xác định hướng cần xoay tới
            try
            {
                Vector3 relativePos = FoundObject.transform.position - transform.position;
                // sử dụng hàm LookRotation để đưa ra vòng cần quay
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                // di chuyển camera theo vòng quay được tính
                transform.rotation = rotation;
                transform.position = Vector3.MoveTowards(transform.position, FoundObject.transform.position, Time.deltaTime * tocdoduoi);
            }
            catch
            {
                Debug.Log("Loi ko mong muon");
            }

        }
    }

    public void FireBullet(Vector3 point)
    {
        float dis = Vector3.Distance(point, transform.position);
        targer = new Vector3(point.x, point.y + dis / 20, point.z);
        gameObject.GetComponent<Rigidbody>().AddForce((targer - transform.position).normalized * force);
    }
    public void hut(Vector3 huong)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(huong * force);
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag== "Enemy")
        {
            if (Shoot)
            {
                AudioSource.PlayClipAtPoint(Shoot, transform.position, 750f);
            }
            //Debug.Log("da va cham voi enemy");
            //Debug.Log(transform.gameObject.name);
            GameObject exp1 = poolPlayer.Instance.SpawnFromPool(explosion, transform.position, Quaternion.identity);
            if (other.transform.name.Contains("GunShip_Enemy"))
            {
                Debug.Log("Va cham vao:" + other.transform.name);
                other.gameObject.GetComponent<WayPoint>().enabled = false;
                other.gameObject.GetComponent<GunShipController>().enabled = false;
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().mass = 100;
                other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(10000, 10000, 0));
                StartCoroutine(ExampleCoroutine(other.gameObject, exp1));
            }
            else
            {
                other.gameObject.GetComponent<EnemyHealth>().TaKeDamage(attackDamage);
                poolPlayer.Instance.ReturnToPool(gameObject);

                //Attack();
                //m_explosionAudio.Play();
                //Destroy(gameObject);
                //Destroy(exp1, 1.5f);
            }

        }
        else if(other.gameObject.tag == "Ground")
        {
            if(Shoot)
            {
                AudioSource.PlayClipAtPoint(Shoot, transform.position, 750f);
            }
            GameObject exp1 = poolPlayer.Instance.SpawnFromPool(explosion, transform.position, Quaternion.identity);
            poolPlayer.Instance.ReturnToPool(gameObject);
            //Destroy(gameObject);
            //Destroy(exp1, 1.5f);
        }
       

    }
    IEnumerator ExampleCoroutine(GameObject other,GameObject exp1)
    {

        yield return new WaitForSeconds(3);
        other.GetComponent<EnemyHealth>().TaKeDamage(attackDamage);
        poolPlayer.Instance.ReturnToPool(gameObject);

        //Attack();
        //m_explosionAudio.Play();
        //Destroy(gameObject);
        //Destroy(exp1, 1.5f);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
        float damage = relativeDistance * m_MaxDamage;

        damage = Mathf.Max(0f, damage);

        return 0f;
    }
    void Attack()
    {

        enemyHealth.TaKeDamage(attackDamage);
    }
}
