using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ConstructManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.AltGr))
        {
            AdditiveSceneManager asm = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
            //asm.UnloadScene(null,null);
            StartCoroutine(asm.LoadScene(Scenes.CONSTRUCT, DelegateBeforeConstructLoad, DelegateAfterConstructLoad));
        }
    }

    void DelegateAfterConstructLoad()
    {
        Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
        Transform leftSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform;
        leftSenseGlove.SetParent(cameraOrigin, false);
        leftSenseGlove.GetChild(1).GetComponent<ShowOpenMenuButton>().CompareObject = cameraOrigin;
        Transform openMenuButtonPressurePlate = leftSenseGlove.GetChild(1).GetChild(0);
        //openMenuButtonPressurePlate.GetComponent<SpringJoint>().connectedAnchor = openMenuButtonPressurePlate.position;
        leftSenseGlove.GetComponent<SteamVR_TrackedObject>().enabled = true;
        GameObject.FindGameObjectWithTag("SenseGloveRight").transform.SetParent(cameraOrigin, false);
        GameObject.FindGameObjectWithTag("ConstructObjects").transform.GetChild(0).SetParent(cameraOrigin, false);
    }

    void DelegateBeforeConstructLoad()
    {

    }
}
