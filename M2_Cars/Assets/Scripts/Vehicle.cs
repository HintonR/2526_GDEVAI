using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float speed, rSpeed, accel, deccel, minSpeed, maxSpeed, brake;

    void Update()
    {
        Vector3 toTarget = target.position - transform.position;

        if (toTarget.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(toTarget.normalized, Vector3.up);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rSpeed * Time.deltaTime);
        }
        
        if (Vector3.Angle(target.forward, transform.forward) > brake && speed > 2)
            speed = Mathf.Clamp(speed - (deccel * Time.deltaTime), minSpeed, maxSpeed);
        else
            speed = Mathf.Clamp(speed + (accel * Time.deltaTime), minSpeed, maxSpeed);

        
        transform.position += transform.forward * speed;

        //transform.Translate(0,0,speed);

    }
}
