using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSenseGloveConnection : MonoBehaviour
{
    SenseGlove_Object left;
    // Start is called before the first frame update
    void Start()
    {
        left = this.transform.GetChild(0).GetComponent<SenseGlove_Object>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning(left.GloveReady);
        if (Input.GetKeyDown(KeyCode.N))
        {
            left.LinkToGlove(0);
        }
    }
}
