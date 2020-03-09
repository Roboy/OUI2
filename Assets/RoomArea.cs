using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RoomArea : MonoBehaviour
{
    public float speedRot;
    public float speedPos;

    bool moveForward;
    bool rotateLeft;
    bool rotateRight;

    Transform cameraOrigin;

    void Start()
    {
        moveForward = false;
        rotateRight = false;
        rotateLeft = false;

        IsDirty = false;
        oldPos = this.transform.position;

        cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
    }

    private void Update()
    {
        

        if (rotateRight)
        {
            cameraOrigin.localEulerAngles = new Vector3(0f, cameraOrigin.localEulerAngles.y + Time.deltaTime * speedRot, 0f);
        }
        else if (rotateLeft)
        {
            cameraOrigin.localEulerAngles = new Vector3(0f, cameraOrigin.localEulerAngles.y - Time.deltaTime * speedRot, 0f);
        }
        else if (moveForward)
        {
            Vector3 newPos = transform.position + transform.forward * Time.deltaTime * speedPos;
            if (validatePositionInRoom(newPos, false))
            {
                //cameraOrigin.position = newPos;//this.transform.position = newPos;
            }
        }
        /*else
        {
            Vector3 updatedPos = cameraOrigin.transform.position + (this.transform.position - oldPos);
            if (validatePositionInRoom(updatedPos))
            {
                cameraOrigin.transform.position = updatedPos;
            }
            oldPos = this.transform.position;
        }*/

        
    }

    #region move by controller
    public SteamVR_Action_Boolean TriggerClick;
    public SteamVR_Action_Boolean LeftClick;
    public SteamVR_Action_Boolean RightClick;
    private SteamVR_Input_Sources inputSource;

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
        moveForward = true;
    }

    private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        moveForward = false;
    }

    private void LeftDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rotateLeft = true;
    }
    private void LeftUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rotateLeft = false;
    }
    private void RightDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rotateRight = true;
    }
    private void RightUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rotateRight = false;
    }
#endregion

    #region room scale
    bool IsDirty;
    Vector3 oldPos;

    /*void LateUpdate()
    {
        validatePositionInRoom(this.transform.position, true);
    }*/

    public bool validatePositionInRoom(Vector3 pos)
    {
        return validatePositionInRoom(pos, false);
    }

    private bool validatePositionInRoom(Vector3 pos, bool internalUse)
    {
        float x = pos.x;
        float z = pos.z;


        /* This is the half-working bull shit, if KatVR would work
        bool lockx = false;
        bool lockz = false;
        bool result = true;

        //wall front back; z axis
        if(x <= 4.5f && z <= -34.5f)
        {
            result = false;
            if(internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -34f);
        }

        if (x <= 4.5f && z > 4.5f) {
            result = false;
            if(internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 4f);
        }
        if ((4.5f < x && x <= 14.5f) && z <= -34.5f) {
            result = false;
            if(internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -34f);
        }
        if ((4.5f < x && x <= 14.5f) && z > -5.5f) {
            result = false;
            if(internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -6f);
        }
        if((4.5f < x && x <= 14.5f) && (z <= -14.5 && z > -25.5f))
        {
            result = false;
            if (internalUse) {
                if (Mathf.Abs(this.transform.position.z + 14.5f) < Mathf.Abs(this.transform.position.z + 25.5f)) {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -14f);
                }
                else {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -26f);
                } 
            }
        }

        if (((x <= 15.5f && x > 14.5f) || x > 24.5) && z <= -34.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -34f);
        }

        if (((x <= 15.5f && x > 14.5f) || x > 24.5) && z > -25.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -26f);
        }

        if ((x <= 24.5f && x > 15.5f) && z <= 44.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -44f);
        }

        if ((x <= 24.5f && x > 15.5f) && z > -25.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -26f);
        }
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        

        //wall left right; x axis 
        if (z <= -34.5f && x <= 15.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(16f, this.transform.position.y, this.transform.position.z);
        }

        if (z <= -34.5f && x > 24.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(24f, this.transform.position.y, this.transform.position.z);
        }

        if ((z <= -25.5f && z > -34.5f) && x <= 4.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(5f, this.transform.position.y, this.transform.position.z);
        }

        if ((z <= -25.5f && z > -34.5f) && x > 34.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(34f, this.transform.position.y, this.transform.position.z);
        }

        if (((z <= -14.5f && z > -25.5f) || z > -5.5f) && x <= -4.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(-4f, this.transform.position.y, this.transform.position.z);
        }

        if (((z <= -14.5f && z > -25.5f) || z > -5.5f) && x > 4.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(4f, this.transform.position.y, this.transform.position.z);
        }

        if ((z <= -5.5f && z > -14.5f) && x <= -4.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(-4f, this.transform.position.y, this.transform.position.z);
        }

        if ((z <= -5.5f && z > -14.5f) && x > 14.5f) {
            result = false;
            if (internalUse) this.transform.position = new Vector3(14f, this.transform.position.y, this.transform.position.z);
        }*/


        /*if((z <= -34.5f && (x <= 15.5f || x > 24.5f)) 
            || ((z <= -25.5f && z > -34.5f) && (x <= 4.5f || x > 34.5f))
            || (((z <= -14.5f && z > -25.5f) || z > -5.5f) && (x <= -4.5f || x > 4.5f))
            || ((z <= -5.5f && z > -14.5f) && (x <= -4.5f || x > 14.5f)))
        {
            lockx = true;
        }*/

        //this.transform.position = new Vector3((lockx ? oldPos.x : this.transform.position.x), this.transform.position.y, (lockz ? oldPos.z : this.transform.position.z));
        //oldPos = this.transform.position;


        if (x > -4.5f && x < 4.5f && z > -34.5 && z < 4.5)
        {
            return true;
        }

        if (x > 4.5f && x < 34.5f && z > -34.5 && z < -25.5)
        {
            return true;
        }

        if (x > 4.5f && x < 14.5f && z > -14.5 && z < -5.5)
        {
            return true;
        }

        if (x > 15.5f && x < 24.5f && z > -44.5 && z < -34.5)
        {
            return true;
        }

        return false;//This for KatVR: result; //!(lockx || lockz);
    }
    #endregion
}
