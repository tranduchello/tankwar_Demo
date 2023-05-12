using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomController : MonoBehaviour {

    public float delay = 3f;// thời gia trì hoãn trước khi bom nổ
    public float radius = 10f;//bán kính tác động của lực nổ
    public float force = 700f;// lực nổ

    public ParticleSystem explosionEffect;// hiệu ứng nổ
    public AudioSource m_explosionAudio;// âm thanh nổ


    float countdown;// biến đếm ngược
    bool hasExploded = false;// biến kiểm tra xem bom đã nổ chưa
   public AudioSource bomno;// âm thanh bom va chạm với đối tượng khác
    public AudioClip bomn1;// âm thanh bom va chạm vs đối tượng khác
   void Start()
    {
        // khỏi tạo biến ddeeem ngược bằng giá trị delay
        countdown = delay;       
    }
    void Update()
    {
        countdown -= Time.deltaTime;// gán gtri biến đếm ngược theo thời gian thực
        if (countdown <= 0f && !hasExploded)// nếu  đã hét thời gian đếm ngược và bom chưa nổ
        {
            Explode();// gọi hàm tạo hiệu ứng nổ
            hasExploded = true;// đánh dấu là bom đã nổ
           
        }
    }
    void Explode()
    {
        //tạo hiệu ứng nổ
        Instantiate(explosionEffect, transform.position, transform.rotation);
        

        // Lấy ra  các đối tượng gàn kề 
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // áp dụng nổ lên các đối tượng gần kề
                rb.AddExplosionForce(force, transform.position, radius);     
            }
        }
        // dừng hiệu ứng nổ và hủy dối tượng bom
        explosionEffect.Stop();
        Destroybom();
    }
    void OnCollisionEnter(Collision coll)
    {
        // nếu đối tượng va chạm không phải là chính đối tượng bom
        if (coll.gameObject != gameObject)
        {
            bomno = gameObject.AddComponent<AudioSource>();// thêm  một thành phần âm thanh vào đối tượng bom
            bomno.clip = bomn1;// thiết lập âm thanh cho thành phần âm thanh
            bomno.Play();// phát âm thanh
        }
    }
    void Destroybom()
    {
        Destroy(gameObject);// hủy đối tượng bom
        //bomno.Stop();//dừng âm thanh
    }
}
