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
    Animator animator;
    AudioSource source;

    [SerializeField]
    AudioClip hornSound;

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
    float timer;

    [SerializeField]
    private float enrageCreep;

    private int i;
    private bool striking;

    // Use this for initialization
    void Start ()
    {
        waypointList = new List<GameObject>();
        sortedWaypointList = new List<GameObject>();
        rcTarget = GameObject.FindGameObjectWithTag("Player");
        FindWaypoints();
        agent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        striking = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        rayDirection = rcTarget.transform.position - transform.position;
        if (Physics.Raycast(transform.position , rayDirection, out hit))
        {
            if (hit.transform == rcTarget.transform && !TetroDismount.bDismountInProcess)
            {
                gameObject.transform.LookAt(rcTarget.transform.position);
                gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
                //source.PlayOneShot(hornSound);
                //agent.SetDestination(rcTarget.transform.position);
                //agent.speed = 20;
            }

            else if (timer > enrageCreep && !TetroDismount.bDismountInProcess)
            {
                gameObject.transform.LookAt(rcTarget.transform.position);
                gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            }

            else
                GetWaypoint();
        }

        if ((enrageCreep - 1) > timer && timer > (enrageCreep - 2))
            source.Play();

        Debug.DrawRay(transform.position, rayDirection, Color.red);
        animator.SetFloat("SkeletonSpeed", speed);
        animator.SetFloat("SkeletonSpeedNav", agent.speed);


        //Physics.Raycast(transform.position, rayDirection, out hit);
        //if (hit.transform == rcTarget.transform)
        //    i = 1;
        //else if (timer > enrageCreep)
        //    i = 2;
        //else
        //    i = 3;

        //switch(i)
        //{
        //    case 1:
        //        agent.SetDestination(rcTarget.transform.position);
        //        break;
        //    case 2:
        //        agent.SetDestination(rcTarget.transform.position);
        //        break;
        //    case 3:
        //        GetWaypoint
        //        break;
        //}
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
        //rayDirectionWP = sortedWaypointList[num].transform.position - transform.position;
        //Physics.Raycast(transform.position, rayDirectionWP, out hit2);

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
        //gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        agent.SetDestination(sortedWaypointList[num].transform.position);
        //animation.play
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

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Tetromino" || col.gameObject.tag == "damagedTetromino" || col.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
            speed = 0;
            agent.speed = 0;
            animator.SetBool("walking", false);
            striking = true;
            animator.SetBool("striking", striking);
            Destroy(gameObject, 3f);
        }
    }
}
