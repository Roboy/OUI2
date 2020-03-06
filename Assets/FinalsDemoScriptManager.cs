using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalsDemoScriptManager : MonoBehaviour
{
    public GameObject TrashBin;
    public GameObject TrashCube;
    public int NumberOfCubes;
    GameObject trashBin;
    // Start is called before the first frame update
    void Start()
    {
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
        GameObject roboy = GameObject.FindGameObjectWithTag("Roboy");
        trashBin = Instantiate(TrashBin, roboy.transform);
        for(int i = 0; i < NumberOfCubes; i++)
        {
            Instantiate(TrashCube, GetRandomPositionInRoom(), Quaternion.identity);
        }
    }

    public void StopQuest()
    {
        GameObject[] allTrashCubes = GameObject.FindGameObjectsWithTag("Trash");
        foreach (GameObject trash in allTrashCubes)
        {
            Destroy(trash);
        }
        Destroy(trashBin);
    }

    Vector3 GetRandomPositionInRoom()
    {
        return Vector3.zero;
    }
}
