using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(WheelCollider))]
public class maybayHead : MonoBehaviour
{////by Marcos Schultz - www.schultzgames.com
    [Range(3, 50)]
    public int raysNumber = 36;
    [Range(45, 180)]
    public float raysMaxAngle = 135;
    public float rayWidth = 0.15f;

    WheelCollider _wheelCollider;
    float orgRadius;

    void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
        orgRadius = _wheelCollider.radius;
    }

    void FixedUpdate()
    {
        float radiusOffset = 0.0f;
        for (int x = 0; x <= raysNumber; x++)
        {
            Quaternion wheelAngle = Quaternion.AngleAxis(_wheelCollider.steerAngle, transform.up);
            float angleToRay = x * (raysMaxAngle / raysNumber) + ((180 - raysMaxAngle) / 2);
            Vector3 rayDirection = wheelAngle * Quaternion.AngleAxis(angleToRay, transform.right) * transform.forward;
            //

            //get wheel position and rotation .  vị trí bánh xe và quay 
            Vector3 worldPos;
            Quaternion worldRot;
            _wheelCollider.GetWorldPose(out worldPos, out worldRot);

            //raycast center
            RaycastHit hitCenter;
            if (Physics.Raycast(worldPos, rayDirection, out hitCenter, _wheelCollider.radius))
            {
                if (!hitCenter.transform.IsChildOf(transform.root) && !hitCenter.collider.isTrigger)
                {
                    Debug.DrawLine(worldPos, hitCenter.point, Color.red);
                    radiusOffset = Mathf.Max(radiusOffset, _wheelCollider.radius - hitCenter.distance);
                }
            }
            Debug.DrawRay(worldPos, rayDirection * orgRadius, Color.green);

            //raycast Right
            RaycastHit hitRight;
            if (Physics.Raycast(worldPos + (worldRot * Vector3.right) * rayWidth, rayDirection, out hitRight, _wheelCollider.radius))
            {
                if (!hitRight.transform.IsChildOf(transform.root) && !hitRight.collider.isTrigger)
                {
                    Debug.DrawLine(worldPos + (worldRot * Vector3.right) * rayWidth, hitRight.point, Color.red);
                    radiusOffset = Mathf.Max(radiusOffset, _wheelCollider.radius - hitRight.distance);
                }
            }
            Debug.DrawRay(worldPos + (worldRot * Vector3.right) * rayWidth, rayDirection * orgRadius, Color.green);

            //raycast Left
            RaycastHit hitLeft;
            if (Physics.Raycast(worldPos - (worldRot * Vector3.right) * rayWidth, rayDirection, out hitLeft, _wheelCollider.radius))
            {
                if (!hitLeft.transform.IsChildOf(transform.root) && !hitLeft.collider.isTrigger)
                {
                    Debug.DrawLine(worldPos - (worldRot * Vector3.right) * rayWidth, hitLeft.point, Color.red);
                    radiusOffset = Mathf.Max(radiusOffset, _wheelCollider.radius - hitLeft.distance);
                }
            }
            Debug.DrawRay(worldPos - (worldRot * Vector3.right) * rayWidth, rayDirection * orgRadius, Color.green);

            //set correct radius
            float newRadius = Mathf.Clamp(orgRadius + radiusOffset, orgRadius, orgRadius * 2.0f);
            if (Mathf.Abs(_wheelCollider.radius - newRadius) > 0.01f)
            {
                _wheelCollider.radius = Mathf.LerpUnclamped(_wheelCollider.radius, newRadius, 0.02f);
            }
        }
    }
}
