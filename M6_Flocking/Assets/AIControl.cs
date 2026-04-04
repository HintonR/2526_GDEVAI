using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    GameObject[] goals;
    NavMeshAgent agent;
    Animator anim;
    float speedMod;
    float dRadius = 12;
    float fRadius = 8;

    void Start()
    {
        goals = GameObject.FindGameObjectsWithTag("goal");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        int randomGoal = Random.Range(0, goals.Length);
        agent.SetDestination(goals[randomGoal].transform.position);
        anim.SetFloat("wOffset", Random.Range(0.1f, 1f));
        ResetAgent();
    
    }

    void LateUpdate()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            int randomGoal = Random.Range(0, goals.Length);
            agent.SetDestination(goals[randomGoal].transform.position);
        }
    }

    void ResetAgent()
    {
        speedMod = Random.Range(1f, 1.5f);
        agent.speed = 2 * speedMod;
        agent.angularSpeed = 120;
        anim.SetFloat("speedMod", speedMod);
        anim.SetTrigger("isWalking");
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 location, bool flee)
    {
        if (flee)
        {
            if (Vector3.Distance(location, transform.position) < dRadius)
            {
                Vector3 fleeDirection = (transform.position - location).normalized;
                Vector3 newGoal = transform.position + fleeDirection * fRadius;

                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(newGoal, path);

                if(path.status != NavMeshPathStatus.PathInvalid)
                {
                    agent.SetDestination(path.corners[path.corners.Length - 1]);
                    anim.SetTrigger("isRunning");
                    agent.speed = 10;
                    agent.angularSpeed = 500;
                }
            }
        }
        else
            if (Vector3.Distance(location, transform.position) < dRadius)
            {
                Vector3 newGoal = location;

                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(newGoal, path);

                if(path.status != NavMeshPathStatus.PathInvalid)
                {
                    agent.SetDestination(path.corners[path.corners.Length - 1]);
                    anim.SetTrigger("isRunning");
                    agent.speed = 10;
                    agent.angularSpeed = 500;
                }
            }
    }
}
