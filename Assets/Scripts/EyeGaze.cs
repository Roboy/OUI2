using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Widgets;

public class EyeGaze : MonoBehaviour, IGazeFocusable
{
    View view;

    // Start is called before the first frame update
    void Start()
    {
        view = this.GetComponentInParent<View>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            view.OnSelectionEnter();
        } else
        {
            view.OnSelectionExit();
        }
    }
}
