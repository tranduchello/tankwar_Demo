using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WayPoint : MonoBehaviour {


    public GameObject[] waypoints;//// Mảng các điểm đến
    int current = 0;// vị trí điểm đến hiện tại
    public float speed;// Tốc độ di chuyển 
    float WPradius = 1;// bán kính xác định khoảng cách tới điểm đến để chuyển sang điểm đến tiếp theo
    bool statusDichuyen = true;// trạng thái đang di chuyển hay không
    bool tang = true;// biến để xác định hướng di chuyển của dối tượng
    Transform target;
    private int currentWaypoint = 0; // Waypoint hiện tại của đối tượng
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        // nếu khoảng cách đến điểm đến hiện tại nhỏ hơn bán kính xác định, đối tượng sex dừng lại
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            // thời gian dừng đến điểm hiện tại

            float thoigiandung = waypoints[current].GetComponent<InfoDiem>().Thoigiandung;

            statusDichuyen = false;// // đối tượng không di chuyern 
                                   // nhìn vào playerd
            transform.LookAt(target.transform.position);
            // thời gian dừng tại điểm đến

            Invoke("WaitTime", thoigiandung);

        }
        if (statusDichuyen)
        {
            //xác định hướng cần xoay tới
            Vector3 relativePos = waypoints[current].transform.position - transform.position;
            // sử dụng hàm LookRotation để đưa ra vòng cần quay
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // di chuyển đối tượng tới điểm đến
            transform.rotation = rotation;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }
    }

    // hàm chờ khi đối tượng  kết thúc thời gian dừng tại ddierm đến
    void WaitTime()
    {

        // Debug.Log("Het cho -> di chuyen toi diem tiep theo");
        // nếu không di chuyển 
        if (statusDichuyen == false)
        {

            if (tang)
            {
                current++;
                if (current < waypoints.Length - 1)
                    tang = true;
                else
                {
                    tang = false;
                }
            }
            else
            {
                current--;
                if (current > 0)
                    tang = false;
                else
                    tang = true;
            }
        }
        // đổi trạng thái không di chuyển sang đang di chuyern 
        statusDichuyen = true;

    }

}
