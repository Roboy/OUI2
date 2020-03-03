using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzManager : Singleton<BuzzManager>
{
    public bool RightHand;
    public bool LeftHand;
    private int[] fingersRight;
    private int[] fingersLeft;
    private SenseGlove_Object senseGloveObjectRight;
    private SenseGlove_Object senseGloveObjectLeft;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (RightHand)
        {
            senseGloveObjectRight = GameObject.FindGameObjectWithTag("SenseGloveRight").transform.GetChild(0).GetComponent<SenseGlove_Object>();
            fingersRight = new int[] { 0, 0, 0, 0, 0 };
        }
        if (LeftHand)
        {
            senseGloveObjectLeft = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform.GetChild(0).GetComponent<SenseGlove_Object>();
            fingersLeft = new int[] { 0, 0, 0, 0, 0 };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RightHand)
        {
            senseGloveObjectRight.SendBuzzCmd(fingersRight, 500);
            fingersRight = new int[] { 0, 0, 0, 0, 0 };
        }
        if (LeftHand)
        {
            senseGloveObjectLeft.SendBuzzCmd(fingersLeft, 500);
            fingersLeft = new int[] { 0, 0, 0, 0, 0 };
        }
    }

    public void ActivateFinger(bool rightHand, int fingerindex, int buzzintensity)
    {
        if(fingerindex < 5 && fingerindex >= 0)
        {
            if (rightHand)
            {
                fingersRight[fingerindex] = buzzintensity;
            }
            else
            {
                fingersLeft[fingerindex] = buzzintensity;
            }
        }
    }
}
