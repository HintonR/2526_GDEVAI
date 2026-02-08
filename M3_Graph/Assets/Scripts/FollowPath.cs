using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5f;
    float acc = 1f;
    float rSpeed = 2f;

    [SerializeField] WaypointManager _wp;
    [SerializeField] TextMeshProUGUI _text;

    List<GameObject> wps;

    GameObject cNode;

    int cwIndex = 0;

    Graph graph;


    void Start()
    {
        wps = _wp.Waypoints;
        graph = _wp.Graph;
        cNode = wps[0];

    }

    void Update()
    {
        OnKeyPress();

        if (graph.getPathLength() == 0 || cwIndex == graph.getPathLength())
        {
            return;
        }

        Movement();


    }

    void Movement()
    {
        cNode = graph.getPathPoint(cwIndex);

        if (Vector3.Distance(
                    graph.getPathPoint(cwIndex).transform.position, 
                    transform.position) < acc)
            cwIndex++;

        if (cwIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(cwIndex).transform;
            Vector3 target = new Vector3(
                            goal.position.x, 
                            transform.position.y, 
                            goal.position.z);
            Vector3 direction = target - transform.position;

            transform.rotation = Quaternion.Slerp(
                            transform.rotation,
                            Quaternion.LookRotation(direction),
                            rSpeed * Time.deltaTime);

            transform.position += transform.forward * speed * Time.deltaTime;   
        }        
    }

    void OnKeyPress()
    {
    
        if (Input.GetKeyDown(KeyCode.Alpha1)) GoToLocation(0); //Helipad
        if (Input.GetKeyDown(KeyCode.Alpha2)) GoToLocation(2); //Mountains 
        if (Input.GetKeyDown(KeyCode.Alpha3)) GoToLocation(13); //CC
        if (Input.GetKeyDown(KeyCode.Alpha4)) GoToLocation(4); //Pumps
        if (Input.GetKeyDown(KeyCode.Alpha5)) GoToLocation(5); //Tankers
        if (Input.GetKeyDown(KeyCode.Alpha6)) GoToLocation(6); //Radar
        if (Input.GetKeyDown(KeyCode.Alpha7)) GoToLocation(7); //CP
        if (Input.GetKeyDown(KeyCode.Alpha8)) GoToLocation(8); //Middle
        if (Input.GetKeyDown(KeyCode.Alpha9)) GoToLocation(10); //Ruins
        if (Input.GetKeyDown(KeyCode.Alpha0)) GoToLocation(9); //Refinery
    }


    void GoToLocation(int i)
    {
        graph.AStar(cNode, wps[i]);
        cwIndex = 0;

        string text = "";

        switch (i)
        {
            case 0  : text = "Helipad"; break;
            case 2  : text = "Mountains"; break;
            case 13 : text = "Command Center"; break;
            case 4  : text = "Pumps"; break;
            case 5  : text = "Tankers"; break;
            case 6  : text = "Radar"; break;
            case 7  : text = "Command Post"; break;
            case 8  : text = "Middle of Map"; break;
            case 10 : text = "Ruins"; break;
            case 9  : text = "Refinery"; break;
        }

        _text.text = text;
    }


}
