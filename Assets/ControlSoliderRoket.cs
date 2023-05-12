using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSoliderRoket : MonoBehaviour
{
    public Animator ani;
    public Collider m_ObjCollider;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetInteger("status", 0);
        m_ObjCollider = GetComponent<Collider>();

        //

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "TankPlayer")
        {
            m_ObjCollider.isTrigger = true;

            //Debug.Log("da va cham");
            ani.SetInteger("status", 1);

            Destroy(gameObject, 3f);
        }
    }
}
