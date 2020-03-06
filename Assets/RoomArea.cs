using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomArea : MonoBehaviour
{
    bool IsDirty;
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        IsDirty = false;
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        validatePositionInRoom(this.transform.position, true);
    }

    public bool validatePositionInRoom(Vector3 pos)
    {
        return validatePositionInRoom(pos, false);
    }

    private bool validatePositionInRoom(Vector3 pos, bool internalUse)
    {
        bool lockx = false;
        bool lockz = false;
        bool result = true;
        float x = pos.x;
        float z = pos.z;

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
        }
        
        
        /*if((z <= -34.5f && (x <= 15.5f || x > 24.5f)) 
            || ((z <= -25.5f && z > -34.5f) && (x <= 4.5f || x > 34.5f))
            || (((z <= -14.5f && z > -25.5f) || z > -5.5f) && (x <= -4.5f || x > 4.5f))
            || ((z <= -5.5f && z > -14.5f) && (x <= -4.5f || x > 14.5f)))
        {
            lockx = true;
        }*/

        //this.transform.position = new Vector3((lockx ? oldPos.x : this.transform.position.x), this.transform.position.y, (lockz ? oldPos.z : this.transform.position.z));
        //oldPos = this.transform.position;


        /*
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
        }*/

        return result; //!(lockx || lockz);
    }
}
