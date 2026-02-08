using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


public class WaypointManager : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints;
    [SerializeField] List<Link> links;
    Graph graph = new Graph();

    public List<GameObject> Waypoints => waypoints;
    public List<Link> Links => links;
    public Graph Graph => graph;

    void Start()
    {
        if (waypoints.Count > 0)
        {
            foreach (GameObject wp in waypoints)
                graph.AddNode(wp);

            foreach (Link l in links)
            {
                graph.AddEdge(l.n1, l.n2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.n2, l.n1);
            }

        }
    }

    void Update()
    {
        graph.debugDraw();   
    }
}

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI};
    public GameObject n1, n2;
    public direction dir;
}



