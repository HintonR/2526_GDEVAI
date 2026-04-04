using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : EnemyBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var dir = enemy.transform.position - target.transform.position;
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,
                                                    Quaternion.LookRotation(dir),
                                                    rSpeed * Time.deltaTime);
        enemy.transform.Translate(0, 0, speed * Time.deltaTime);
    }

}
