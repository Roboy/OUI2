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
        ism = this.transform.parent.GetComponent<IconStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        //ism.Focus(hasFocus);
        glow(hasFocus);
    }

    public void glow(bool glow)
    {
        if (glow)
        {
            ism.ShowNotification();
        } else
        {
            ism.StopNotification();
        }
    }
}
