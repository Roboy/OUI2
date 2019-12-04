using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayView : MonoBehaviour
{
    [SerializeField] public string[] data;
    
    public void Init(int size)
    {
        data = new string[size];
    }
}
