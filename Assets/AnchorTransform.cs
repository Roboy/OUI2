using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorTransform : MonoBehaviour
{
    public GameObject[] buttons;
    Vector3 oldPos;
    Quaternion oldRot;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindGameObjectsWithTag("Button3D");
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool posChanged = !this.transform.position.Equals(oldPos);
        bool rotChanged = !this.transform.rotation.Equals(oldRot);
        if (!posChanged && !rotChanged)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.GetChild(0).GetComponent<SpringJoint>().massScale = 10f;
            }
        } else
        {
            if (rotChanged)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    SpringJoint spring = buttons[i].GetComponentInChildren<SpringJoint>();
                    spring.massScale = 0.00001f;
                    Quaternion rotDiff = this.transform.rotation * Quaternion.Inverse(oldRot);
                    spring.connectedAnchor = oldPos + rotDiff * (spring.connectedAnchor - oldPos);
                }
            }
            if (posChanged)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    SpringJoint spring = buttons[i].transform.GetChild(0).GetComponent<SpringJoint>();
                    spring.massScale = 0.00001f;
                    spring.connectedAnchor = buttons[i].transform.GetChild(0).GetComponent<SpringJoint>().connectedAnchor + (this.transform.position - oldPos);
                }
            }
        }
        oldRot = this.transform.rotation;
        oldPos = this.transform.position;
    }
}
