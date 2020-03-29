﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.Events;

public class StateManager : Singleton<StateManager> {
    //For debugging: if true, disables all construct functionality in this script
    public bool KillConstruct;
    private States currentState;

    AdditiveSceneManager additiveSceneManager;
    ConstructFXManager constructFXManager;
    TransitionManager transitionManager;
    GameObject leftSenseGlove;
    GameObject rightSenseGlove;

    /// <summary>
    /// The states the operator can be in
    /// </summary>
    public enum States
    {
        HUD, Construct
    }

    /// <summary>
    /// Set reference to instances.
    /// Load construct as initial state.
    /// </summary>
    void Start() {
        constructFXManager = GameObject.FindGameObjectWithTag("ConstructFXManager").GetComponent<ConstructFXManager>();
        additiveSceneManager = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
        transitionManager = GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>();

        leftSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveLeft");
        rightSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveRight");
        leftSenseGlove.SetActive(false);
        rightSenseGlove.SetActive(false);
        additiveSceneManager.ChangeScene(Scenes.CONSTRUCT, null, null, DelegateBeforeConstructLoad, DelegateAfterConstructLoad);
        currentState = States.Construct;
    }

    /// <summary>
    /// Public method initiating state transition.
    /// Change from current state to next state (fixed order).
    /// </summary>
    public void GoToNextState() {
        switch (currentState)
        {
            case States.HUD:
                transitionManager.StartTransition(false);
                additiveSceneManager.ChangeScene(Scenes.CONSTRUCT, null, null, DelegateBeforeConstructLoad, DelegateAfterConstructLoad);
                currentState = States.Construct;
                break;
            case States.Construct:
                transitionManager.StartTransition(true);
                additiveSceneManager.ChangeScene(Scenes.HUD, null, DelegateOnConstructUnload, null, null);
                currentState = States.HUD;
                break;
            default:
                Debug.LogWarning("Unhandled State: Please specify the next State after " + currentState);
                break;
        }
    }

    /// <summary>
    /// For debugging.
    /// Initiate transition to next state by pressing space on your keyboard while in play mode in Unity.
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            GoToNextState();
    }

    #region Delegates
    /// <summary>
    /// Logic that is executed right before the construc scene is loaded.
    /// Enables both sense gloves and the OpenMenuButton
    /// </summary>
    void DelegateBeforeConstructLoad()
    {
        if (!KillConstruct)
        {
            leftSenseGlove.SetActive(true);
            rightSenseGlove.SetActive(true);

            Transform openMenuButton = leftSenseGlove.transform.GetChild(1);
            openMenuButton.gameObject.SetActive(true);
            openMenuButton.GetChild(0).GetComponent<ButtonRigidbodyConstraint>().InitialState();
            openMenuButton.GetChild(1).GetComponent<FrameClickDetection>().highlightOff();
        }
    }

    /// <summary>
    /// Logic that is executed right after the construct scene has been loaded.
    /// Set roboy model to the position of the operator.
    /// Hook up quest button (what to do if pressed).
    /// Attach menu to the camera.
    /// Activate construct visual effects.
    /// </summary>
    void DelegateAfterConstructLoad()
    {
        if (!KillConstruct)
        {
            Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
            Transform constructObjects = GameObject.FindGameObjectWithTag("ConstructObjects").transform;
            Transform roboy = GameObject.FindGameObjectWithTag("Roboy").transform;
            
            roboy.SetParent(GameObject.FindGameObjectWithTag("FinalScenePlaceholder").transform);
            roboy.position = cameraOrigin.position + new Vector3(0f, 1.4f, 0f);
            roboy.localEulerAngles = cameraOrigin.transform.localEulerAngles;//Quaternion.Euler(roboy.rotation.eulerAngles + cameraOrigin.rotation.eulerAngles);//cameraOrigin.GetChild(1).rotation;//roboy.rotation * cameraOrigin.GetChild(1).rotation;

            FrameClickDetection questButton = constructObjects.GetChild(0).GetComponentInChildren<FrameClickDetection>();
            questButton.onPress[0].AddListener(GameObject.FindGameObjectWithTag("FinalsDemoScriptManager").GetComponent<FinalsDemoScriptManager>().StartQuest);
            questButton.onPress[1].AddListener(GameObject.FindGameObjectWithTag("FinalsDemoScriptManager").GetComponent<FinalsDemoScriptManager>().StopQuest);
            constructObjects.GetChild(0).SetParent(cameraOrigin, false);
            
            constructFXManager.ToggleEffects(true);
        }
    }

    /// <summary>
    /// Logic that is executed after the construct scene has been destroyed.
    /// Destroy the menu, which belongs to the construct scene but was attached to the camera (final scene).
    /// Release sense gloves in case they were interacting when the transition was initiated.
    /// Disable Sense gloves.
    /// Deactivate construct visual effects.
    /// </summary>
    void DelegateOnConstructUnload()
    {
        if (!KillConstruct)
        {
            Destroy(GameObject.FindGameObjectWithTag("MainMenu"));
            leftSenseGlove.GetComponentInChildren<SenseGlove_Object>().StopBrakes();
            rightSenseGlove.GetComponentInChildren<SenseGlove_Object>().StopBrakes();
            leftSenseGlove.SetActive(false);
            rightSenseGlove.SetActive(false);

            constructFXManager.ToggleEffects(false);
        }
    }
    #endregion
}