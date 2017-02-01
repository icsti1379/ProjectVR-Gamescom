using UnityEngine;
using System.Collections;

public class SpawnBorder : MonoBehaviour {

    [SerializeField]
    GameObject gBorderPrefab;

    public static GameObject gBorder1;

    public static GameObject gBorder2;

    public static GameObject gBorder3;

    public static GameObject gBorder4;

    /// <summary>
    /// Variable used for Input of iMapScale in Unity, iMapScale needs to be static
    /// </summary>
    [SerializeField]
    [Range(10, 30)]
    int iTetrisMapScale;
    public static int iMapScale;


    /// <summary>
    /// Variable used for Input of iSpawnPosY in Unity, iSpawnPosY needs to be static
    /// </summary>
    [SerializeField]
    [Range(10, 50)]
    int iTetroSpawnPosY;
    public static int iSpawnPosY;

    /// <summary>
    /// Spawns the 4 Borders depending on iMapScale and iSpawnPosY
    /// </summary>
    void Start ()
    {
        iMapScale = iTetrisMapScale;
        iSpawnPosY = iTetroSpawnPosY + 3;
        SpawnTetromino.iMapScale = iTetrisMapScale;
        SpawnTetromino.iSpawnPosY = iTetroSpawnPosY;

        gBorder1 = (GameObject)Instantiate(gBorderPrefab, new Vector3(-iMapScale / 2 + 0.5f, iSpawnPosY / 2, -iMapScale / 2 + 0.5f), new Quaternion(), transform);
        gBorder1.transform.localScale = new Vector3(1, iSpawnPosY, 1);
        gBorder1.name = "Border 1";

        gBorder2 = (GameObject)Instantiate(gBorderPrefab, new Vector3(-iMapScale / 2 + 0.5f, iSpawnPosY / 2, iMapScale / 2 - 0.5f), new Quaternion(), transform);
        gBorder2.transform.localScale = new Vector3(1, iSpawnPosY, 1);
        gBorder2.name = "Border 2";

        gBorder3 = (GameObject)Instantiate(gBorderPrefab, new Vector3(iMapScale / 2 - 0.5f, iSpawnPosY / 2, -iMapScale / 2 + 0.5f), new Quaternion(), transform);
        gBorder3.transform.localScale = new Vector3(1, iSpawnPosY, 1);
        gBorder3.name = "Border 3";

        gBorder4 = (GameObject)Instantiate(gBorderPrefab, new Vector3(iMapScale / 2 - 0.5f, iSpawnPosY / 2, iMapScale / 2 - 0.5f), new Quaternion(), transform);
        gBorder4.transform.localScale = new Vector3(1, iSpawnPosY, 1);
        gBorder4.name = "Border 4";
    }
}
