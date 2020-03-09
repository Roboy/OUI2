using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalsDemoScriptManager : MonoBehaviour
{
    public GameObject TrashBin;
    public GameObject TrashCube;
    public int NumberOfCubes;

    private RoomArea room;

    GameObject trashBin;
    // Start is called before the first frame update
    void Start()
    {
        room = GameObject.FindObjectOfType<RoomArea>();
        if(NumberOfCubes <= 0)
        {
            NumberOfCubes = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartQuest();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopQuest();
        }
    }

    public void StartQuest()
    {
        Transform roboyArms = GameObject.FindGameObjectWithTag("RoboyArms").transform;
        roboyArms.localEulerAngles = new Vector3(-60f, roboyArms.localEulerAngles.y, roboyArms.localEulerAngles.z);
        GameObject roboy = GameObject.FindGameObjectWithTag("Roboy");
        trashBin = Instantiate(TrashBin, roboy.transform);
        for(int i = 0; i < NumberOfCubes; i++)
        {
            Instantiate(TrashCube, GetRandomPositionInRoom(), Quaternion.identity);
        }
    }

    public void StopQuest()
    {
        Transform roboyArms = GameObject.FindGameObjectWithTag("RoboyArms").transform;
        GameObject[] allTrashCubes = GameObject.FindGameObjectsWithTag("Trash");
        foreach (GameObject trash in allTrashCubes)
        {
            Destroy(trash);
        }
        Destroy(trashBin);
        roboyArms.localEulerAngles = new Vector3(0f, roboyArms.localEulerAngles.y, roboyArms.localEulerAngles.z);
    }

    Vector3 GetRandomPositionInRoom()
    {
        float y = Random.Range(0.1f, 1.7f);
        Vector3 result = new Vector3(Random.Range(-4.5f, 34.5f), y, Random.Range(-44.5f, 4.5f));
        while (!room.validatePositionInRoom(result))
        {
            result = new Vector3(Random.Range(-4.5f, 34.5f), y, Random.Range(-44.5f, 4.5f));
        }
        return result;
    }
}
