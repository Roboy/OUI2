using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCurver : MonoBehaviour
{
    public CurvedUI.CurvedUISettings curvedUI;

    // Start is called before the first frame update
    void Start()
    {
        curvedUI = GetComponent<CurvedUI.CurvedUISettings>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        curvedUI.AddEffectToChildren();
    }
}
