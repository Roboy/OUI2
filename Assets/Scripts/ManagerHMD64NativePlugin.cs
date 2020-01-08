using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ManagerHMD64NativePlugin : MonoBehaviour
{
    [DllImport("hmd64")]
    private static extern int getVersion();
    [DllImport("hmd64")]
    private static extern float startHMDEyeTrackingCalibration(int id, ref float q);
    [DllImport("hmd64")]
    private static extern char resolveErrorCode(int err_code);

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Native Plugin Manager starts now.");
        float q = 0f;
        Debug.LogError(startHMDEyeTrackingCalibration(0, ref q));
        Debug.LogWarning(q);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(getVersion());
        float q = 0f;
        float result = startHMDEyeTrackingCalibration(0, ref q);
        Debug.LogError(result);
        Debug.LogWarning(q);
        //Debug.LogError(resolveErrorCode(result));
    }
}
