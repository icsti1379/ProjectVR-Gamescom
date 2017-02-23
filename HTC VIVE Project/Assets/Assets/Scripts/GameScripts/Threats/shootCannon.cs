using UnityEngine;
using System.Collections;

public class shootCannon : MonoBehaviour {

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    [Range(0f, 100f)]
    private float spawnTime;

    [SerializeField]
    string primaryTargetTag;

    [SerializeField]
    string secondaryTargetTag;

    private bool targets;

    private float tTimer;

    GameObject newInstance;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tTimer += Time.deltaTime;

        if (tTimer >= spawnTime && CheckForTargets())
        {
            SpawnBullet();
            tTimer = 0;
        }
        //SpawnBullet();
    }

    void SpawnBullet()
    {
        newInstance = (GameObject)Instantiate(Bullet, transform);
        newInstance.transform.position = transform.position;
    }

    bool CheckForTargets()
    {
        GameObject[] primaryTargetList;
        primaryTargetList = GameObject.FindGameObjectsWithTag(primaryTargetTag);    //liste mit allen primären zielen
        GameObject[] secondaryTargetList;
        secondaryTargetList = GameObject.FindGameObjectsWithTag(secondaryTargetTag);    //liste mit allen sekundären zielen

        if (primaryTargetList != null || secondaryTargetList != null)
            targets = true;
        else
            targets = false;

        return targets;
    }
}
