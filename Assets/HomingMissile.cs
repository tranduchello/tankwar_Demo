using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour {

    public Transform target;
    public float force;
    public float rotateForce;
    public float secondsBeforeHoming;
    public float launchForce;

    private Rigidbody rb;
    private bool shouldFollow;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitBeforeHoming());
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (shouldFollow)
        {
            if (target != null)
            {
                Vector3 direction = target.position - rb.position;

                direction.Normalize();

                Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
                rb.angularVelocity = rotationAmount * rotateForce;
                rb.velocity = transform.forward * force;
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
       // Destroy(collision.collider.gameObject);
        Destroy(gameObject);
    }

    private IEnumerator WaitBeforeHoming()
    {
        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(secondsBeforeHoming);
        shouldFollow = true;
    }
}
