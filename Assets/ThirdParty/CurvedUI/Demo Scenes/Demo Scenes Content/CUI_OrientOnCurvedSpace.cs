﻿using UnityEngine;
using System.Collections;

namespace CurvedUI
{
    public class CUI_OrientOnCurvedSpace : MonoBehaviour
    {
        public bool IsStatic;
        public CurvedUISettings mySettings;


        // Use this for initialization
        void Awake()
        {

            mySettings = GetComponentInParent<CurvedUISettings>();
            if(mySettings != null)
            {
                UpdateTransform();
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (mySettings == null)
            {
                mySettings = GetComponentInParent<CurvedUISettings>();
            }

            if (!IsStatic && mySettings != null)
            {
                UpdateTransform();
            }
        }

        void UpdateTransform()
        {
            Vector3 positionInCanvasSpace = mySettings.transform.worldToLocalMatrix.MultiplyPoint3x4(this.transform.parent.position);
            transform.position = mySettings.CanvasToCurvedCanvas(positionInCanvasSpace);
            transform.rotation = Quaternion.LookRotation(mySettings.CanvasToCurvedCanvasNormal(transform.parent.localPosition), transform.parent.up);
        }
    }
}
