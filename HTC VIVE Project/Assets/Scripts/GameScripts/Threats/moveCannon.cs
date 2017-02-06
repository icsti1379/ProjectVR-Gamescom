using UnityEngine;
using System.Collections;

public class moveCannon : MonoBehaviour {

    [SerializeField]
    [Range(0f, 100f)]
    private float speed;

    [SerializeField]
    private GameObject centerObject;

    private float timePassed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centerObject.transform.position, Vector3.down, speed * Time.deltaTime);
    }
}
