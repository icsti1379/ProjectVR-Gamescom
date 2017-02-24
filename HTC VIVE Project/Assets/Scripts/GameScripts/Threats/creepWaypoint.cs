using UnityEngine;
using System.Collections;

/// <summary>
/// old script, saved for backup
/// </summary>
public class creepWaypoint : MonoBehaviour
{
    GameObject[] waypointList;
    int num = 0;
    float distance;

    [SerializeField]
    float minDistance;

    [SerializeField]
    private float speed;

    // Use this for initialization
    void Start ()
    {
        waypointList = GameObject.FindGameObjectsWithTag("Waypoint");
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(gameObject.transform.position, waypointList[num].transform.position);

        if (distance > minDistance)
            move();

        else
        {
            if (num + 1 == waypointList.Length)
                num = 0;

            else
                num++;
        }
	}

    void move()
    {
        gameObject.transform.LookAt(waypointList[num].transform.position);
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }
}
