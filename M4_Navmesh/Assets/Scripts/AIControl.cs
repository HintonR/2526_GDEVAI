using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{

    [SerializeField] UnityEngine.AI.NavMeshAgent agent;

    public UnityEngine.AI.NavMeshAgent Agent => agent;

}
