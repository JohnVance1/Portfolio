using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    private float fMaxSpeed;
    private float fTempSpeed;
    private Vector3 acceleration;
    private Vector3 velocity;
    private float mass;
    private float fFrictionCoeff;

    void Start()
    {
        fMaxSpeed = 3.0f;
        mass = 0.1f;
        fFrictionCoeff = 4.0f;
    }

    
    void Update()
    {
        CalcPlayerMovement();

        velocity += acceleration * Time.deltaTime;
        //acceleration = Vector3.zero;
        transform.position += velocity * Time.deltaTime;
    }

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Applies friction to the vehicle.
    /// Acts opposite to velocity.
    /// </summary>
    /// <param name="coeff"></param>
    public void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction *= coeff;
        acceleration += friction;
    }

    void CalcPlayerMovement() // Change this to messing with the rigidbody - adding forces rather than changing the transform
    {
        // If shift is pressed, make the sprint bool true
        if (Input.GetKey(KeyCode.LeftShift))
            fTempSpeed = fMaxSpeed * 2;
        else
            fTempSpeed = fMaxSpeed;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 force = gameObject.transform.forward;
            ApplyForce(force.normalized * fTempSpeed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 force = gameObject.transform.forward;
            ApplyForce(force.normalized * -fTempSpeed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 force = gameObject.transform.right;
            ApplyForce(force.normalized * -fTempSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 force = gameObject.transform.right;
            ApplyForce(force.normalized * fTempSpeed);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            velocity.z = 0;
            acceleration.z = 0;
            if(Input.GetKey(KeyCode.S))
            {
                Vector3 force = gameObject.transform.forward;
                ApplyForce(force.normalized * -fTempSpeed);
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            velocity.z = 0;
            acceleration.z = 0;
            if(Input.GetKey(KeyCode.W))
            {
                Vector3 force = gameObject.transform.forward;
                ApplyForce(force.normalized * fTempSpeed);
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            velocity.x = 0;
            acceleration.x = 0;
            if(Input.GetKey(KeyCode.D))
            {
                Vector3 force = gameObject.transform.right;
                ApplyForce(force.normalized * fTempSpeed);
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            velocity.x = 0;
            acceleration.x = 0;
            if(Input.GetKey(KeyCode.A))
            {
                Vector3 force = gameObject.transform.right;
                ApplyForce(force.normalized * -fTempSpeed);
            }
        }
    }
}
