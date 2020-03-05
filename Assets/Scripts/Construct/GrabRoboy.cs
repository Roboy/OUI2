using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRoboy : MonoBehaviour
{
    Vector3 defaultPos;
    Quaternion defaultRot;
    SenseGlove_Grabable grab;
    AdditiveSceneManager additiveSceneManager;

    private void Start()
    {
        additiveSceneManager = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
        defaultPos = this.transform.localPosition;
        defaultRot = this.transform.localRotation;
        grab = this.GetComponent<SenseGlove_Grabable>();
    }

    private void Update()
    {
        if(this.transform.localPosition != defaultPos && !grab.IsInteracting())
        {
            ResetTransform();
        }
    }

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

    void ResetTransform()
    {
        this.transform.localPosition = defaultPos;
        this.transform.localRotation = defaultRot;
    }
}
