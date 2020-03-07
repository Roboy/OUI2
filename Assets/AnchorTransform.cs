using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorTransform : MonoBehaviour
{
    public GameObject[] buttons;

    public bool Follow;
    public Transform FollowObject;
    SpringJoint spring;
    Vector3 defaultLocalPos;
    Vector3 oldPos;
    Quaternion oldRot;

    void Start()
    {
        spring = this.GetComponent<SpringJoint>();
        if(Follow && FollowObject == null)
        {
            Debug.LogError("FollowObject not specified.");
            //FollowObject = GameObject.FindGameObjectWithTag("KatVRWalker").transform;
        }
        oldPos = FollowObject.position;
        defaultLocalPos = this.transform.localPosition;
        ResetAnchor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ResetAnchor();
        }
    }

    void FixedUpdate()
    {
        if (Follow)
        {
            bool posChanged = !FollowObject.position.Equals(oldPos);
            bool rotChanged = !FollowObject.rotation.Equals(oldRot);
            if (!posChanged && !rotChanged)
            {
                /*for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].transform.GetChild(0).GetComponent<SpringJoint>().massScale = 10f;
                }*/
                spring.massScale = 10f;
            }
            else
            {
                if (rotChanged)
                {
                    /*for (int i = 0; i < buttons.Length; i++)
                    {
                        SpringJoint spring = buttons[i].GetComponentInChildren<SpringJoint>();
                        spring.massScale = 0.00001f;
                        Quaternion rotDiff = this.transform.rotation * Quaternion.Inverse(oldRot);
                        spring.connectedAnchor = oldPos + rotDiff * (spring.connectedAnchor - oldPos);
                    }*/
                    spring.massScale = 0.00001f;
                    Quaternion rotDiff = FollowObject.rotation * Quaternion.Inverse(oldRot);
                    spring.connectedAnchor = oldPos + rotDiff * (spring.connectedAnchor - oldPos);
                }
                if (posChanged)
                {
                    /*for (int i = 0; i < buttons.Length; i++)
                    {
                        SpringJoint spring = buttons[i].transform.GetChild(0).GetComponent<SpringJoint>();
                        
                    }*/
                    spring.massScale = 0.00001f;
                    spring.connectedAnchor = spring.connectedAnchor + (FollowObject.position - oldPos);
                }
            }
            oldRot = FollowObject.rotation;
            oldPos = FollowObject.position;
        }
    }

    public void ResetAnchor()
    {
        this.transform.localPosition = defaultLocalPos;
        spring.connectedAnchor = this.transform.position;
    }
}
