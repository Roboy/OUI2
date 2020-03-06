﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.Events;

public class StateManager : Singleton<StateManager> {
    public bool KillConstruct;
    //private StateMachine stateMachine;
    private States currentState;

    AdditiveSceneManager additiveSceneManager;
    ConstructFXManager constructFXManager;
    TransitionManager transitionManager;
    GameObject leftSenseGlove;
    GameObject rightSenseGlove;

    public enum States
    {
        HUD, Construct
    }

    // Start is called before the first frame update
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

        //stateMachine = new StateMachine(new HUDState(this));
    }

    public void GoToNextState() {
        switch (currentState)
        {
            case States.HUD:
                GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().StartTransition(false);
                additiveSceneManager.ChangeScene(Scenes.CONSTRUCT, null, null, DelegateBeforeConstructLoad, DelegateAfterConstructLoad);
                currentState = States.Construct;
                break;
            case States.Construct:
                GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>().StartTransition(true);
                additiveSceneManager.ChangeScene(Scenes.HUD, null, DelegateOnConstructUnload, null, null);
                currentState = States.HUD;
                break;
            default:
                Debug.LogWarning("Unhandled State: Please specify the next State after " + currentState);
                break;
        }
        //stateMachine.GoToNextState();
    }

    void CheckForDebugInput() {
        // Go to next state when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
            GoToNextState();
    }

    // Update is called once per frame
    void Update() {
        CheckForDebugInput();
    }

    #region Delegates
    void DelegateBeforeConstructLoad()
    {
        if (!KillConstruct)
        {
            leftSenseGlove.SetActive(true);
            rightSenseGlove.SetActive(true);
        }
    }

    void DelegateAfterConstructLoad()
    {
        if (!KillConstruct)
        {
            Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
            Transform constructObjects = GameObject.FindGameObjectWithTag("ConstructObjects").transform;
            Transform roboy = GameObject.FindGameObjectWithTag("Roboy").transform;
            /*Transform leftSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform;
            Transform rightSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveRight").transform;

            rightSenseGlove.SetParent(cameraOrigin, false);
            if (!rightSenseGlove.GetComponentInChildren<SenseGlove_Object>().GloveReady)
            {
                rightSenseGlove.GetChild(0).GetComponent<SenseGlove_Object>().LinkToGlove(rightSenseGlove.GetChild(0).GetComponent<SenseGlove_Object>().GloveIndex);
            }
            leftSenseGlove.SetParent(cameraOrigin, false);
            if(!leftSenseGlove.GetComponentInChildren<SenseGlove_Object>().GloveReady)
            {
                leftSenseGlove.GetComponentInChildren<SenseGlove_Object>().LinkToGlove(leftSenseGlove.GetComponentInChildren<SenseGlove_Object>().GloveIndex);
            }
            leftSenseGlove.GetChild(1).GetComponent<ShowOpenMenuButton>().CompareObject = cameraOrigin;
            leftSenseGlove.GetComponent<SteamVR_TrackedObject>().enabled = true;
            */
            roboy.SetParent(GameObject.FindGameObjectWithTag("FinalScenePlaceholder").transform);
            roboy.position = cameraOrigin.position + new Vector3(0f, 1.4f, 0f);
            roboy.rotation = Quaternion.Euler(roboy.rotation.eulerAngles + cameraOrigin.rotation.eulerAngles);

            FrameClickDetection questButton = constructObjects.GetChild(0).GetComponentInChildren<FrameClickDetection>();
            questButton.onPress[0].AddListener(GameObject.FindGameObjectWithTag("FinalsDemoScriptManager").GetComponent<FinalsDemoScriptManager>().StartQuest);
            questButton.onPress[1].AddListener(GameObject.FindGameObjectWithTag("FinalsDemoScriptManager").GetComponent<FinalsDemoScriptManager>().StopQuest);
            constructObjects.GetChild(0).SetParent(cameraOrigin, false);
            //GameObject.FindGameObjectWithTag("SubMenu3D").transform.SetParent(cameraOrigin, false);
            /*Transform constructObjects = GameObject.FindGameObjectWithTag("ConstructObjects").transform;
            Transform roboy = constructObjects.GetChild(0);
            roboy.position = cameraOrigin.position + new Vector3(0f, 1.5f, 0f);
            roboy.rotation = Quaternion.Euler(roboy.rotation.eulerAngles + cameraOrigin.rotation.eulerAngles);
            */

            constructFXManager.ToggleEffects(true);
        }
    }

    void DelegateOnConstructUnload()
    {
        if (!KillConstruct)
        {
            /*Destroy(GameObject.FindObjectOfType<SenseGlove_DeviceManager>().gameObject);
            Destroy(GameObject.FindGameObjectWithTag("SenseGloveLeft"));
            Destroy(GameObject.FindGameObjectWithTag("SenseGloveRight"));*/
            Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
            for (int i = 0; i < cameraOrigin.childCount; i++)
            {
                if (cameraOrigin.GetChild(i).CompareTag("SubMenu3D"))
                {
                    cameraOrigin.GetChild(i).GetComponentInChildren<SubMenuAnimationHandler>().FadeOut();
                    Destroy(cameraOrigin.GetChild(i).gameObject);
                }
            }
            leftSenseGlove.GetComponentInChildren<SenseGlove_Object>().StopBrakes();
            rightSenseGlove.GetComponentInChildren<SenseGlove_Object>().StopBrakes();
            leftSenseGlove.SetActive(false);
            rightSenseGlove.SetActive(false);

            constructFXManager.ToggleEffects(false);
        }
    }
    #endregion


    /*
    #region STATES

    private class HUDState : IState {
        private StateManager owner;

        public HUDState(StateManager owner) {
            this.owner = owner;
        }

        public void Enter() {
            owner.additiveSceneManager.ChangeScene(Scenes.HUD, null, null, null, null);
        }

        public void Execute() {
        }

        public void Exit() {
        }

        public IState GoToNextState() {
            return new AdvancedMenuState(owner);
        }
    }

    private class AdvancedMenuState : IState {
        private StateManager owner;

        public AdvancedMenuState(StateManager owner) {
            this.owner = owner;
        }

        public void Enter() {
            owner.additiveSceneManager.ChangeScene(Scenes.CONSTRUCT, null, null, null, DelegateAfterConstructLoad);
        }

        public void Execute() {
        }

        public void Exit() {
        }

        public IState GoToNextState() {
            return new HUDState(owner);
        }


        }
    }

    #endregion

    #region STATEMACHINE LOGIC

    private interface IState {
        IState GoToNextState();
        void Enter();
        void Execute();
        void Exit();
    }

    private class StateMachine {
        private IState currentState;

        public StateMachine(IState startState) {
            currentState = startState;
            startState.Enter();
        }

        public void ChangeState(IState newState) {
            if (currentState != null)
                currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        public void Update() {
            if (currentState != null) currentState.Execute();
        }

        public IState GetCurrentState() {
            return currentState;
        }

        public void GoToNextState() {
            ChangeState(currentState.GoToNextState());
        }
    }

    #endregion
    */
}