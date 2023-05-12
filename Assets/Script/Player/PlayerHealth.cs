using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour {


    public int startingHealth;// điểm  máu đầu tiên
    public int currentHealth;// điểm máu hiện tại
    public Slider healthSlider;// dùng slider  để thiện thị 
    public Image damageImage;// dùng chớp đỏ khi nhân vật tấn công

    public AudioSource deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    bool isDead;// biến kiểm tra xem nhân vật chết hay chưa
    bool damaged;// kiểm tra xem nhân vật đã tấn công hay chưa
    bool pussHP;// nhân vật đc hồi máu chưa

    void Awake()
    {
        deathClip = GetComponent<AudioSource>();

        currentHealth = startingHealth;// điểm máu của nhân vật được thiết lập bằng gia trị của biến startingHealth
    }
    // Update is called once per frame
    void Update () {
        //neu damaged == true thi mau se xuat hien
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        //hoac mau se bien mat va tro lai color == 0;
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        //reset the damaged flag
        damaged = false;
	}
    public void TakeDamage(int amount)
    {
        // Đánh dấu là nhân vật đã bị thương
        Debug.Log("amont:"+amount);
        damaged = true;

        // Giảm lượng máu hiện tại của nhân vật bằng lượng sát thương
        currentHealth -= amount;

        // Cập nhật giá trị thanh máu trên giao diện
        healthSlider.value = currentHealth;

       // playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();

            SceneManager.LoadScene("GameOver");
        }
       
    }
    void Death()
    {
        // Đánh dấu là nhân vật đã chết
        isDead = true;
        poolPlayer.Instance.ReturnToPool(gameObject);
        deathClip.Play();
    }
    public void PusHealth(int amont)
    {
        // Tăng lượng máu hiện tại của nhân vật bằng lượng hồi máu

        // Nếu lượng máu hiện tại nhỏ hơn 100
        if (currentHealth < 1000)
        {
            // Đánh dấu là nhân vật đã được hồi máu
            pussHP = true;
            // Tăng lượng máu hiện tại của nhân vật bằng lượng hồi máu
            currentHealth += amont;
            Debug.Log("mau da cong la" + currentHealth);
            // Cập nhật giá trị thanh máu trên giao diện
            healthSlider.value = currentHealth;
        }
        //Nếu lượng máu hiện tại lớn hơn hoặc bằng 100
        else if (currentHealth >= 100)
        {
            pussHP = false;
        }
    }
}
