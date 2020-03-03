using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOpenMenuButton : MonoBehaviour
{
    bool menuOpen;
    public Transform CompareObject;

    void Start()
    {
        if (CompareObject == null)
        {
            CompareObject = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
        }
        menuOpen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!menuOpen)
        {
            if(Vector3.Dot(this.transform.forward, CompareObject.up) < 0)
            {
                for(int i = 0; i < this.transform.childCount; i++)
                {
                    this.transform.GetChild(i).gameObject.SetActive(true);
                }
            } else
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
