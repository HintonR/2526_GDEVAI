using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Animations;

public enum State
{
    Idle,
    Running,
    Casting
}

public class AIControl : MonoBehaviour
{
    public Transform player;

    Animator _anim;

    float rotationSpeed = 2.0f;
    float speed = 2.0f;
    float visionDist = 20.0f;
    float visionAngle = 30.0f;
    float castRange = 5.0f;

    State state;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
  
    void LateUpdate()
    {
        Vector3 dir = player.position - this.transform.position;
        float angle = Vector3.Angle(dir, this.transform.forward);

        if (dir.magnitude < visionDist && angle < visionAngle)
        {
            dir.y = 0;
            
            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(dir),
                                        Time.deltaTime * rotationSpeed);

            if (dir.magnitude > castRange)
            {
                if (state != State.Running)
                {
                    state = State.Running;
                    _anim.SetTrigger("IsRunning");
                }
            }
            else 
            {
                if (state != State.Casting)
                {
                    state = State.Casting;
                    _anim.SetTrigger("IsCasting");
                }
            }
        }
        else {
            if (state != State.Idle)
            {
                state = State.Idle;
                _anim.SetTrigger("IsIdle");
            }
        }

        if (state == State.Running)
        {
            transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
}
