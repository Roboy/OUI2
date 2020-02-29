using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorTransform : MonoBehaviour
{
    public GameObject[] buttons;
    Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindGameObjectsWithTag("Button3D");
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.Equals(oldPos))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.GetChild(0).GetComponent<SpringJoint>().massScale = 10f;
            }
        } else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                SpringJoint spring = buttons[i].transform.GetChild(0).GetComponent<SpringJoint>();
                spring.massScale = 0.00001f;
                spring.connectedAnchor = buttons[i].transform.GetChild(0).GetComponent<SpringJoint>().connectedAnchor + (this.transform.position - oldPos);
            }
        }
        
        oldPos = this.transform.position;
    }
}
