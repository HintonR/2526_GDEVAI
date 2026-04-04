using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : EnemyBaseFSM
{

    GameObject[] waypoints;
    int wIndex;

    void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        wIndex = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        if (waypoints.Length == 0) return;

        if (Vector3.Distance(waypoints[wIndex].transform.position,
                            enemy.transform.position) < acc)
        {
            wIndex++;
            if (wIndex >= waypoints.Length)
                wIndex = 0;
        }

        var dir = waypoints[wIndex].transform.position - enemy.transform.position;
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,
                                                    Quaternion.LookRotation(dir),
                                                    rSpeed * Time.deltaTime);
        
        enemy.transform.Translate(0, 0, speed * Time.deltaTime);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       base.OnStateExit(animator, stateInfo, layerIndex);

    }


}
