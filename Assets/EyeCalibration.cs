using System;
using UnityEngine;
using ViveSR.anipal.Eye;

public class EyeCalibration : MonoBehaviour
{
    public void Execute()
    {
        SRanipal_Eye_API.LaunchEyeCalibration(IntPtr.Zero);
    }
}
