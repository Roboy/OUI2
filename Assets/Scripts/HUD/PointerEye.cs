using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;
using ViveSR.anipal.Eye;
using Tobii.XR;

public class PointerEye : Pointer
{
#pragma warning disable 649
    [SerializeField] private bool _smoothMove = true;

    [SerializeField] [Range(1, 30)] private int _smoothMoveSpeed = 7;
#pragma warning restore 649
    private float _defaultDistance;

    private Camera _mainCamera;
    private Vector3 _lastGazeDirection;

    private const float OffsetFromFarClipPlane = 10f;
    private const float PrecisionAngleScaleFactor = 5f;

    private float ScaleFactor = 0.03f;

    private static EyeData_v2 eyeData = new EyeData_v2();
    private bool eye_callback_registered = false;

    public void Update()
    {
        GetPointerPosition();
    }

    public override void SubclassStart()
    {
        //sranipalStart();
        startGazeVisualizer();
    }

    public override void GetPointerPosition()
    {
        //sranipalUpdate();
        //tobiiUpdate();
        updateGazeVisualizer();
        
    }

    #region tobii 
    private void tobiiStart()
    {

    }

    private void tobiiUpdate()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

        // Check if gaze ray is valid
        if (eyeTrackingData.GazeRay.IsValid)
        {
            // The origin of the gaze ray is a 3D point
            var rayOrigin = eyeTrackingData.GazeRay.Origin;

            // The direction of the gaze ray is a normalized direction vector
            var rayDirection = eyeTrackingData.GazeRay.Direction;

            Debug.LogError("Found valid data");
            PushPointerPosition(rayOrigin, rayDirection);
        }
    }
    #endregion

    #region sranipal
    private void sranipalStart()
    {
        if (!SRanipal_Eye_Framework.Instance.EnableEye)
        {
            enabled = false;
            return;
        }
    }

    private void sranipalUpdate()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                        SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

        if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
        {
            SRanipal_Eye_v2.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = true;
        }
        else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
        {
            SRanipal_Eye_v2.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }

        Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal;

        if (eye_callback_registered)
        {
            if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
            else return;
        }
        else
        {
            if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
            else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
            else return;
        }

        Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocal);
        Debug.LogError(GazeDirectionCombined);
        PushPointerPosition(Camera.main.transform.position - Camera.main.transform.up * 0.05f, GazeDirectionCombined);
        //GazeRayRenderer.SetPosition(0, Camera.main.transform.position - Camera.main.transform.up * 0.05f);
        //GazeRayRenderer.SetPosition(1, Camera.main.transform.position + GazeDirectionCombined * LengthOfRay);
    }
    private void Release()
    {
        if (eye_callback_registered == true)
        {
            SRanipal_Eye_v2.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }
    }
    private static void EyeCallback(ref EyeData_v2 eye_data)
    {
        eyeData = eye_data;
    }
    #endregion

    #region GazeVisualizer
    private void startGazeVisualizer()
    {
        _mainCamera = CameraHelper.GetMainCamera();

        _defaultDistance = _mainCamera.farClipPlane - OffsetFromFarClipPlane;
    }

    private void updateGazeVisualizer()
    {
        var provider = TobiiXR.Internal.Provider;
        var eyeTrackingData = EyeTrackingDataHelper.Clone(provider.EyeTrackingDataLocal);
        var localToWorldMatrix = provider.LocalToWorldMatrix;
        var worldForward = localToWorldMatrix.MultiplyVector(Vector3.forward);
        EyeTrackingDataHelper.TransformGazeData(eyeTrackingData, localToWorldMatrix);

        var gazeRay = eyeTrackingData.GazeRay;
        if (!gazeRay.IsValid) return;
        SetPositionAndScale(gazeRay);
    }

    private void SetPositionAndScale(TobiiXR_GazeRay gazeRay)
    {
        RaycastHit hit;
        var distance = _defaultDistance;
        if (Physics.Raycast(gazeRay.Origin, gazeRay.Direction, out hit))
        {
            distance = hit.distance;
        }

        var interpolatedGazeDirection = Vector3.Lerp(_lastGazeDirection, gazeRay.Direction,
            _smoothMoveSpeed * Time.unscaledDeltaTime);

        var usedDirection = _smoothMove ? interpolatedGazeDirection.normalized : gazeRay.Direction.normalized;
        transform.position = gazeRay.Origin + usedDirection * distance;

        transform.localScale = Vector3.one * distance * ScaleFactor;

        transform.forward = usedDirection.normalized;

        _lastGazeDirection = usedDirection;

        PushPointerPosition(gazeRay.Origin, usedDirection);
    }
    #endregion
}
