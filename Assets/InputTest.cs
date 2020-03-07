using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputTest : MonoBehaviour
{
    public SteamVR_Action_Boolean TriggerClick;
    public SteamVR_Action_Boolean LeftClick;
    public SteamVR_Action_Boolean RightClick;
    private SteamVR_Input_Sources inputSource;

    private void Start() { } //Monobehaviours without a Start function cannot be disabled in Editor, just FYI

    private void OnEnable()
    {
        TriggerClick.AddOnStateDownListener(TriggerDown, inputSource);
        LeftClick.AddOnStateDownListener(LeftDown, inputSource);
        RightClick.AddOnStateDownListener(RightDown, inputSource);

        TriggerClick.AddOnStateUpListener(TriggerUp, inputSource);
        LeftClick.AddOnStateUpListener(LeftUp, inputSource);
        RightClick.AddOnStateUpListener(RightUp, inputSource);
    }

    private void OnDisable()
    {
        TriggerClick.RemoveOnStateDownListener(TriggerDown, inputSource);
        LeftClick.RemoveOnStateDownListener(LeftDown, inputSource);
        RightClick.RemoveOnStateDownListener(RightDown, inputSource);

        TriggerClick.RemoveOnStateUpListener(TriggerUp, inputSource);
        LeftClick.RemoveOnStateUpListener(LeftUp, inputSource);
        RightClick.RemoveOnStateUpListener(RightUp, inputSource);
    }

    private void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.LogError("TriggerDown");
    }

    private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.LogError("TriggerUp");
    }

    private void LeftDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.LogError("LeftDown");
    }
    private void LeftUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.LogError("LeftUp");
    }
    private void RightDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.LogError("RightDown");
    }
    private void RightUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.LogError("RightUp");
    }

}

