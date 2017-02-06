using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// aktuelles script, dieses verwenden
/// </summary>
public class CreepNav2 : MonoBehaviour
{
    private Vector3 rayDirection;
    private GameObject rcTarget;

    [SerializeField]
    RaycastHit hit;
    RaycastHit hit2;

    NavMeshAgent agent;
    NavMeshPath navMeshPath;

    [SerializeField]
    private float speed;

    GameObject[] waypointArray;
    List<GameObject> waypointList;
    List<GameObject> sortedWaypointList;
    int num = 0;
    float distance;
    int nrWaypoints;
    string wpCounter;

    [SerializeField]
    float minDistance;
    private Vector3 rayDirectionWP;
    private string stringIdent;
    private string wpAdd;

    // Use this for initialization
    void Start ()
    {
        waypointList = new List<GameObject>();
        sortedWaypointList = new List<GameObject>();
        rcTarget = GameObject.FindGameObjectWithTag("Player");
        FindWaypoints();
        agent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
    }
	
	// Update is called once per frame
	void Update ()
    {
        rayDirection = rcTarget.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {
            if (hit.transform == rcTarget.transform)
                agent.SetDestination(rcTarget.transform.position);
            else
                GetWaypoint();
        }
    }

    //bool PlayerInLineOfSight(GameObject rcTarget)
    //{
    //    rayDirection = rcTarget.transform.position - transform.position;
    //    if (Physics.Raycast(transform.position, rayDirection, out hit))
    //    {
    //        if (hit.transform == rcTarget)
    //            return true;
    //        else
    //            return false;
    //    }

    //    else
    //        return false;

    //}

    void GetWaypoint()
    {
        distance = Vector3.Distance(gameObject.transform.position, sortedWaypointList[num].transform.position);
        rayDirectionWP = sortedWaypointList[num].transform.position - transform.position;
        Physics.Raycast(transform.position, rayDirectionWP, out hit2);

        if (distance > minDistance)
            move();

        else
        {
            if (num + 1 == sortedWaypointList.Count)
                num = 0;

            else
                num++;
        }
    }

    void move()
    {
        gameObject.transform.LookAt(sortedWaypointList[num].transform.position);
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }

    void FindWaypoints()
    {
        waypointArray = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (GameObject go in waypointArray)
            waypointList.Add(go);

        for (int i = 0; i < waypointList.Count; i++)
        {
            sortedWaypointList.Add(waypointList.Find(obj => obj.name == ("Waypoint" + (i + 1).ToString())));
        }

        //for (int i = 1; i < nrWaypoints; i++)
        //{
        //    sortedWaypointList.Add(GameObject.Find("Waypoint" + i.ToString()));
        //}
    }
}
