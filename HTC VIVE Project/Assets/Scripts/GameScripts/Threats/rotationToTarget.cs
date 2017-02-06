using UnityEngine;
using System.Collections;

public class rotationToTarget : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    float speed;

    [SerializeField]
    private string primaryTargetTag;

    [SerializeField]
    private string secondaryTargetTag;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        target = FindClosestTetromino();
        Vector3 targetDir = target.transform.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public GameObject FindClosestTetromino()
    {
        GameObject[] primaryTargetList;
        primaryTargetList = GameObject.FindGameObjectsWithTag(primaryTargetTag);    //liste mit allen primären zielen
        GameObject[] secondaryTargetList;
        secondaryTargetList = GameObject.FindGameObjectsWithTag(secondaryTargetTag);    //liste mit allen sekundären zielen
        GameObject closestGO = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        if (primaryTargetList != null)                                                  //sucht das nächste primäre Ziel
        {
            foreach (GameObject tetro in primaryTargetList)
            {
                Vector3 diff = tetro.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closestGO = tetro;
                    distance = curDistance;
                }
            }
        }

        if (closestGO == null && secondaryTargetList != null)                           //sucht das nächste sekundäre Ziel, falls noch keins vorhanden ist
        {
            foreach (GameObject tetro in secondaryTargetList)
            {
                Vector3 diff = tetro.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closestGO = tetro;
                    distance = curDistance;
                }
            }
        }
        return closestGO;
    }
}
