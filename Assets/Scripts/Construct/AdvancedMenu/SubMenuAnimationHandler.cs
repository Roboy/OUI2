﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class SubMenuAnimationHandler : MonoBehaviour
{
    struct InteractionPrefab
    {
        public string TagName;
        public UnityAction SafeModeOnAction;
        public UnityAction SafeModeOffAction;
        public List<GameObject> FoundObjects;

        public InteractionPrefab(string tag, UnityAction on, UnityAction off) {
            TagName = tag;
            SafeModeOnAction = on;
            SafeModeOffAction = off;
            FoundObjects = new List<GameObject>();
        }
    }

    public bool MButtonTransition;
    public bool IsNested = false;
    public bool AcivateOnFadeOut;
    public GameObject ElementToActivate;
    private int currentState;
    private bool newRequest;
    private bool fadeIn;

    List<InteractionPrefab> interactionPrefabs;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = -1;
        newRequest = false;
        fadeIn = true;
        animator = transform.parent.GetComponent<Animator>();
        interactionPrefabs = new List<InteractionPrefab>();

        interactionPrefabs.Add(new InteractionPrefab("Button3D", safemodeOnButton3D, safemodeOffButton3D));
        interactionPrefabs.Add(new InteractionPrefab("Slider3D", safemodeOnSlider3D, safemodeOffSlider3D));

        //foundObjects = new List<List<GameObject>>();
        if (!IsNested)
        {
            foreach (InteractionPrefab prefab in interactionPrefabs)
            {
                List<GameObject> currentList = new List<GameObject>();
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).CompareTag(prefab.TagName))
                        {
                            currentList.Add(transform.GetChild(i).gameObject);
                        }
                    }
                prefab.FoundObjects.AddRange(currentList);
            }
        }
        else
        {
            foreach (InteractionPrefab prefab in interactionPrefabs)
            {
                    prefab.FoundObjects.AddRange(findObjectsWithTagInAllChildren(prefab.TagName, transform));
            }
        }

        safeModeOn();
        animator.SetTrigger("Go");
    }

    private void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState != state.fullPathHash)
        {
            currentState = state.fullPathHash;
            if (state.IsName("Visible"))
            {
                safeModeOff();
                animator.SetBool("FadeIn", false);
            } else if(state.IsName("Invisible")) {
                deActivate(false);
                animator.SetBool("FadeOut", false);
            }
        }

        if (newRequest && (state.IsName("Visible") || state.IsName("Invisible")))
        {
            newRequest = false;
            if (fadeIn && state.IsName("Invisible"))
            {
                deActivate(true);
                animator.SetBool("FadeIn", true);
            }
            else if(!fadeIn && state.IsName("Visible")) {
                safeModeOn();
                animator.SetBool("FadeOut", true);
            } 
        }

        if(MButtonTransition && Input.GetKeyDown(KeyCode.M))
        {
            if (fadeIn)
            {
                FadeOut();
            }
            else
            {
                FadeIn();
            }
        }
    }

    List<GameObject> findObjectsWithTagInAllChildren(string tag, Transform parent)
    {
        List<GameObject> list = new List<GameObject>();
        Transform currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i);
            if (currentChild.CompareTag(tag))
            {
                list.Add(currentChild.gameObject);
            }
            list.AddRange(findObjectsWithTagInAllChildren(tag, currentChild));
        }
        return list;
    }

    private void deActivate(bool activate)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(activate);
        }

        int myIndex = transform.GetSiblingIndex();
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (i != myIndex)
            {
                parent.GetChild(i).gameObject.SetActive(activate);
            }
        }
    }

    public void FadeIn()
    {
        newRequest = true;
        fadeIn = true;
    }
    public void FadeOut()
    {
        newRequest = true;
        fadeIn = false;
        if (AcivateOnFadeOut)
        {
            Assert.IsNotNull(ElementToActivate, "If you set 'ActivateOnFadeOut' to true, then you must specify the 'ElementToActivate'.");
            ElementToActivate.SetActive(true);
            if(ElementToActivate.CompareTag("Button3D"))
            {
                ElementToActivate.transform.GetChild(0).GetComponent<ButtonRigidbodyConstraint>().InitialState();
                ElementToActivate.transform.GetChild(1).GetComponent<FrameClickDetection>().highlightOff();
            }
        }
    }

    private void safeModeOn()
    {
        foreach (InteractionPrefab prefab in interactionPrefabs)
        {
            prefab.SafeModeOnAction.Invoke();
        }
    }
    private void safeModeOff()
    {
        foreach (InteractionPrefab prefab in interactionPrefabs)
        {
            prefab.SafeModeOffAction.Invoke();
        }
    }

    #region safe mode methods for each interactionPrefab
    private void safemodeOnButton3D()
    {
        List<GameObject> allButtons = interactionPrefabs.Find(x => x.TagName.Equals("Button3D")).FoundObjects;
        foreach (GameObject obj in allButtons)
        {
            Transform frame = obj.transform.GetChild(1);
            frame.GetComponent<FrameClickDetection>().highlightOff();
            frame.GetComponent<Collider>().enabled = false;
            frame.GetComponent<FrameClickDetection>().enabled = false;

            Transform activeArea = obj.transform.GetChild(2);
            activeArea.GetComponent<Collider>().enabled = false;

            /*Transform pressurePlate = obj.transform.GetChild(0);
            pressurePlate.gameObject.SetActive(false);*/
            //pressurePlate.GetComponent<Collider>().enabled = false;
        }
    }
    private void safemodeOffButton3D()
    {
        List<GameObject> allButtons = interactionPrefabs.Find(x => x.TagName.Equals("Button3D")).FoundObjects;
        foreach (GameObject obj in allButtons)
        {
            Transform frame = obj.transform.GetChild(1);
            frame.GetComponent<Collider>().enabled = true;
            frame.GetComponent<FrameClickDetection>().enabled = true;

            /*Transform activeArea = obj.transform.GetChild(2);
            activeArea.GetComponent<Collider>().enabled = true;*/

            /*Transform pressurePlate = obj.transform.GetChild(0);
            pressurePlate.gameObject.SetActive(true);*/
            /*Rigidbody rigidbody = pressurePlate.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.centerOfMass = new Vector3(0, 0, 0.01f);*/
            /*pressurePlate.transform.localPosition = new Vector3(0, 0.75f, -0.325f);
            pressurePlate.GetComponent<Collider>().enabled = true;*/
        }
    }

    private void safemodeOnSlider3D()
    {
        List<GameObject> allSliders = interactionPrefabs.Find(x => x.TagName.Equals("Slider3D")).FoundObjects;
        foreach (GameObject obj in allSliders)
        {
            obj.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            obj.transform.GetChild(0).GetComponent<CustomSlider>().enabled = false;
        }
    }
    private void safemodeOffSlider3D()
    {
        List<GameObject> allSliders = interactionPrefabs.Find(x => x.TagName.Equals("Slider3D")).FoundObjects;
        foreach (GameObject obj in allSliders)
        {
            Transform full = obj.transform.GetChild(0);
            CustomSlider customslider = full.GetComponent<CustomSlider>();
            customslider.ReturnToDefaultPos();
            full.GetComponent<Collider>().enabled = true;
            customslider.enabled = true;
        }
    }
    #endregion
}