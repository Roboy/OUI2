﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRoboy : MonoBehaviour
{
    Vector3 defaultPos;
    Quaternion defaultRot;
    SenseGlove_Grabable grab;
    AdditiveSceneManager additiveSceneManager;

    /// <summary>
    /// Set reference to instances
    /// </summary>
    private void Start()
    {
        additiveSceneManager = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
        defaultPos = this.transform.localPosition;
        defaultRot = this.transform.localRotation;
        grab = this.GetComponent<SenseGlove_Grabable>();
    }

    /// <summary>
    /// If this object has been moved and then released without triggering the transition, it is reset to its inital position.
    /// </summary>
    private void Update()
    {
        if(this.transform.localPosition != defaultPos && !grab.IsInteracting())
        {
            ResetTransform();
        }
    }

    /// <summary>
    /// If this object intersects its target zone, the transition to the next state is triggered.
    /// </summary>
    /// <param name="other">The collider of the intersected object</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "TargetZoneStartTransition")
        {
            if (grab.EndInteractAllowed())
            {
                grab.EndInteraction(grab.GrabScript, true);
            }
            StateManager.Instance.GoToNextState();
        }
    }

    /// <summary>
    /// Resets this object's position and rotation to the initial state.
    /// </summary>
    void ResetTransform()
    {
        this.transform.localPosition = defaultPos;
        this.transform.localRotation = defaultRot;
    }
}
