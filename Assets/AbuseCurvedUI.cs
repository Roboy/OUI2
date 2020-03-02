using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurvedUI;

public class AbuseCurvedUI : MonoBehaviour
{
    CurvedUISettings settings;

    // Start is called before the first frame update
    void Start()
    {
        settings = this.GetComponent<CurvedUISettings>();
        settings.ForceUseBoxCollider = true;
        this.GetComponent<CurvedUIRaycaster>().RebuildCollider();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(settings.ForceUseBoxCollider);
        this.GetComponent<CurvedUIRaycaster>().RebuildCollider();
    }
}
