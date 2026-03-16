using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{

    List<GameObject> agents = new List<GameObject>();

    [SerializeField] GameObject player;

    float stopDistance = 5f;


    void Start()
    {
        agents.Clear();
        agents.AddRange(GameObject.FindGameObjectsWithTag("AI"));
    }

    void Update()
    {

        foreach (GameObject a in agents)
        {
            var ai = a.GetComponent<AIControl>();

            NavMeshAgent agent = ai.Agent;
            float distanceToPlayer = Vector3.Distance(a.transform.position, player.transform.position);

            if (distanceToPlayer > stopDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }   
            else
                agent.isStopped = true;
            
        }
            
        
    }
}
