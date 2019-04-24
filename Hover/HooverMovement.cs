using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HooverMovement : MonoBehaviour {
    public float speed = 20f;
    public float Maxspeed = 180f;
    public float turnSpeed = 3f;
    public float hoverForce = 9000f;
    public float hoverHeight = 0.5f;
    private float powerInput;
    private float rotationspeed = 5f;
    private float turnInput;
    private Rigidbody carRigidbody;    
    private Vector3 prev_up;
    private float current_speed;

    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        prev_up = transform.up;

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point);
            Debug.Log("hit");
            Vector3 desired_up = hit.normal;
            Quaternion tilt = Quaternion.FromToRotation(transform.up, desired_up);
            transform.rotation = tilt * transform.rotation;
            prev_up.Normalize();
            transform.localPosition = prev_up * hoverHeight;
        }

        Vector3 AccelForce = new Vector3(0f, 0f, powerInput * speed*100);

        if (Input.GetKey("q"))
        {
            AccelForce = new Vector3(0f, 0f, AccelForce.z * 0);
            carRigidbody.velocity = Vector3.zero;
            if (speed < Maxspeed)
            {
                speed *= 1.2f;
            }
        }

        if(carRigidbody.velocity.magnitude <= speed)
        {
            carRigidbody.AddRelativeForce(AccelForce);
        }
        else
        {
            carRigidbody.AddRelativeForce(AccelForce);
        }

        if (turnInput == 0)
        {
            carRigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        }
    }
}
