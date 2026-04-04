using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAway : MonoBehaviour
{
    [SerializeField] GameObject prefab, prefab2;
    GameObject[] agents;

    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(prefab, hit.point, prefab.transform.rotation);
                foreach (GameObject a in agents)
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point, true);
            }

        }
        
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(prefab2, hit.point, prefab.transform.rotation);
                foreach (GameObject a in agents)
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point, false);
            }

        }
    }
}
