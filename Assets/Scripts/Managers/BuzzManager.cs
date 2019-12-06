using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzManager : Singleton<BuzzManager>
{

    private int[] fingersRight;
    private int[] fingersLeft;
    private SenseGlove_Object senseGloveObjectRight;
    private SenseGlove_Object senseGloveObjectLeft;
    // Start is called before the first frame update
    void Start()
    {
        senseGloveObjectRight = GameObject.FindGameObjectWithTag("SenseGloveRight").transform.GetChild(0).GetComponent<SenseGlove_Object>();
        senseGloveObjectLeft = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform.GetChild(0).GetComponent<SenseGlove_Object>();
        fingersRight = new int[] { 0, 0, 0, 0, 0 };
        fingersLeft = new int[] { 0, 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        senseGloveObjectRight.SendBuzzCmd(fingersRight, 500);
        senseGloveObjectLeft.SendBuzzCmd(fingersLeft, 500);
        fingersRight = new int[] {0,0,0,0,0};
        fingersLeft = new int[] { 0, 0, 0, 0, 0 };
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
