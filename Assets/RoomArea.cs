using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomArea : MonoBehaviour
{
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (validatePositionInRoom(this.transform.position))
        {
            oldPos = this.transform.position;
        } else
        {
            this.transform.position = oldPos;
        }
    }

    public bool validatePositionInRoom(Vector3 pos)
    {
        float x = pos.x;
        float z = pos.z;

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

        return false;
    }
}
