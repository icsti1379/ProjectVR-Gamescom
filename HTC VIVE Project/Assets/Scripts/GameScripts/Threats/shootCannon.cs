using UnityEngine;
using System.Collections;

public class shootCannon : MonoBehaviour {

    [SerializeField]
    string primaryTargetTag;
    
    [SerializeField]
    string secondaryTargetTag;

    private bool targets;
    private GameObject newInstance;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBullet(GameObject projectile)
    {
        if (CheckForTargets() && !TetroDismount.bDismountInProcess)
        {
            newInstance = (GameObject)Instantiate(projectile, transform);
            newInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        else
            return;
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
