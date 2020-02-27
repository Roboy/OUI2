using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class EyeGaze : MonoBehaviour, IGazeFocusable
{
    IconStateManager ism;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Started Script");
        //ism = this.GetComponent<IconStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        Debug.LogError(this.name + " changed focus");
        //ism.Focus(hasFocus);
    }
}
