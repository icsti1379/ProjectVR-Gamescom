﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBullet : MonoBehaviour
{

    [SerializeField]
    [Range(0, 10)]
    private float step;

    [SerializeField]
    string primaryTargetTag;

    [SerializeField]
    string secondaryTargetTag;

    public GameObject centerObject { get; set; }

    // Use this for initialization
    // holt sich sein ziel
    void Start()
    {
        centerObject = FindClosestTetromino();
    }

    /// <summary>
    /// bewegt das projektil auf das ziel zu
    /// </summary>
    void Update()
    {
        if (centerObject == null)
            centerObject = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector3.MoveTowards(transform.position, centerObject.transform.position, step);
    }

    /// <summary>
    /// löscht das Projektil wenn es mit einem GameObject kollidiert, welches ein ziel ist
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == primaryTargetTag || col.gameObject.tag == secondaryTargetTag || col.gameObject.tag == "Player")
            Destroy(gameObject);
    }


    /// <summary>
    /// sucht nach dem nächsten GameObject mit einem bestimmten tag
    /// </summary>
    /// <returns>gibt das nächste GameObject zurück</returns>
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
