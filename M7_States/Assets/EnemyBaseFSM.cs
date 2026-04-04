using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour
{
    [SerializeField] protected GameObject enemy;
    [SerializeField] protected GameObject target;
    [SerializeField] protected float speed = 2.0f;
    [SerializeField] protected float rSpeed = 1.0f;
    [SerializeField] protected float acc = 3.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        target = GameObject.FindGameObjectWithTag("Player");
    }

}
