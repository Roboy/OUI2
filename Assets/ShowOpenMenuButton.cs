using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOpenMenuButton : MonoBehaviour
{
    bool menuOpen;
    Transform OpenMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        menuOpen = false;
        Transform leftSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform;
        for(int i = 0; i < leftSenseGlove.childCount; i++)
        {
            if (leftSenseGlove.GetChild(i).CompareTag("Button3D"))
            {
                OpenMenuButton = leftSenseGlove.GetChild(i);
                break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!menuOpen)
        {
            if(Vector3.Dot(OpenMenuButton.forward, this.transform.up) < 0)
            {
                OpenMenuButton.gameObject.SetActive(true);
            } else
            {
                OpenMenuButton.gameObject.SetActive(false);
            }
        }
    }
}
