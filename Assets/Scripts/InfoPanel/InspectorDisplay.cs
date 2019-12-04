using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorDisplay : MonoBehaviour
{
    public string[] data;

    public void Init(int size)
    {
        data = new string[size];
    }
}
