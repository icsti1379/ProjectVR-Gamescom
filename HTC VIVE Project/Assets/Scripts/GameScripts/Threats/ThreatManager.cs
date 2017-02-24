using UnityEngine;
using System.Collections;

public class ThreatManager : MonoBehaviour
{
    float timer;
    int shootCounter;
    shootCannon activeCannon;
    GameObject firingCannon;
    GameObject projectile;
    private float busterShotTimer;
    private float busterShotFrequency;

    [SerializeField]
    GameObject firstCannon;

    [SerializeField]
    GameObject secondCannon;

    [SerializeField]
    GameObject thirdCannon;

    [SerializeField]
    GameObject fourthCannon;

    [SerializeField]
    private float spawnTimer;

    [SerializeField]
    GameObject smallBullet;

    [SerializeField]
    GameObject busterBullet;

    [SerializeField]
    private int bigBulletEveryXShot;

    // Use this for initialization
    void Start ()
    {
        if (spawnTimer == 0)
            spawnTimer = 5;

        shootCounter = 1;
        busterShotTimer = 0;
        busterShotFrequency = bigBulletEveryXShot * spawnTimer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        projectile = smallBullet;
        timer += Time.deltaTime;
        busterShotTimer += Time.deltaTime;

        if (busterShotTimer % busterShotFrequency <= 2)
            projectile = busterBullet;


        shootCounter = Random.Range(1, 5);

	    if (timer > spawnTimer)
        {
            switch (shootCounter)
            {
                case 1:
                    activeCannon = firstCannon.GetComponent<shootCannon>();
                    activeCannon.SpawnBullet(projectile);
                    timer = 0;
                    break;
                case 2:
                    activeCannon = secondCannon.GetComponent<shootCannon>();
                    activeCannon.SpawnBullet(projectile);
                    timer = 0;
                    break;
                case 3:
                    activeCannon = thirdCannon.GetComponent<shootCannon>();
                    activeCannon.SpawnBullet(projectile);
                    timer = 0;
                    break;
                case 4:
                    activeCannon = fourthCannon.GetComponent<shootCannon>();
                    activeCannon.SpawnBullet(projectile);
                    timer = 0;
                    break;
            }
        }
	}
}
