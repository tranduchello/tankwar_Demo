using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public float startHealth = 20f;// sức khỏe ban đầu
    public float health;// 
    NavShess navShess;
    AudioSource audioEnemy;

    [Header("Unity Stuff")]
    public Image healthBar;// giá trị fill  của hình ảnh healBaer
    //explo
    public GameObject destroyedVersion;
    ScoreDisplay scorehandle;

    private void OnEnable()
    {
        health = startHealth;
        if (scorehandle == null)
        {
            scorehandle = GameObject.FindGameObjectWithTag("GameolayHandle").GetComponent<ScoreDisplay>();
        }
    }
    private void Awake()
    {
    }
    void Start()
    {
        navShess = GetComponent<NavShess>();
       
        // khởi tạo biến health
        health = startHealth;
        audioEnemy = GetComponent<AudioSource>();
    }
    // hàm này được sd để giảm  sức khỏa của đối tượng dựa trên tham số amount
    public void TaKeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health/startHealth;
        // nếu heath <= 0 thì phát âm thanh và kích hoạt hai hàm 

        
        if (health <= 0)
        {

            if (navShess != null)
            {
                Debug.Log("abcd");
                navShess.EnemyDeathAnim();

            }
            audioEnemy.Play();
            StartCoroutine(Die());
            //Explode()

        }

    }
    public void ResetEnemyHealth()
    {
        Debug.Log("dsfs");
        health = startHealth;
        healthBar.fillAmount= 1f;
    }
    // dừng âm thanh  và hủy đối tượng 

    IEnumerator Die()
    {

        yield return new WaitForSeconds(1f);
        Debug.Log("Doi tuong destroy:" + gameObject.name);

        ResetEnemyHealth();
        poolPlayer.Instance.ReturnToPool(gameObject);

        scorehandle.score++;
        scorehandle.bonusHi();
    }

}
