using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class StateManager : Singleton<StateManager>
{
    private StateMachine stateMachine;

    delegate void MakeTransition();

    AdditiveSceneManager additiveSceneManager;
    TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        additiveSceneManager = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
        transitionManager = GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>();
        
        stateMachine = new StateMachine(new HUDState(this));
    }

    public void GoToNextState()
    {
        stateMachine.GoToNextState();
    }

    void CheckForDebugInput()
    {
        // Go to next state when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.GoToNextState();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDebugInput();

        stateMachine.Update();
    }

    #region STATES

    private class HUDState : IState
    {
        private StateManager owner;

        public HUDState(StateManager owner)
        {
            this.owner = owner;
        }

        public void Enter()
        {
            owner.additiveSceneManager.TriggerLoadScene(Scenes.HUD, null, null);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
           //owner.additiveSceneManager.TriggerUnloadScene(null, null);
        }

        public IState GoToNextState()
        {
            return new AdvancedMenuState(owner);
        }
    }

    private class AdvancedMenuState : IState
    {
        private StateManager owner;

        public AdvancedMenuState(StateManager owner)
        {
            this.owner = owner;
        }

        public void Enter()
        {
            owner.additiveSceneManager.TriggerLoadScene(Scenes.CONSTRUCT, null, DelegateAfterConstructLoad);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
           // owner.additiveSceneManager.TriggerUnloadScene(null, null);
        }

        public IState GoToNextState()
        {
            return new HUDState(owner);
        }

        void DelegateAfterConstructLoad()
        {
            Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
            Transform leftSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform;
            leftSenseGlove.SetParent(cameraOrigin, false);
            leftSenseGlove.GetChild(1).GetComponent<ShowOpenMenuButton>().CompareObject = cameraOrigin;
            leftSenseGlove.GetComponent<SteamVR_TrackedObject>().enabled = true;
            GameObject.FindGameObjectWithTag("SenseGloveRight").transform.SetParent(cameraOrigin, false);
            Transform constructObjects = GameObject.FindGameObjectWithTag("ConstructObjects").transform;
            Transform roboy = constructObjects.GetChild(0);
            roboy.position = cameraOrigin.position + new Vector3(0f, 1.5f, 0f);
            roboy.rotation = Quaternion.Euler(roboy.rotation.eulerAngles + cameraOrigin.rotation.eulerAngles);
            constructObjects.GetChild(1).SetParent(cameraOrigin, false);
        }
    }

    #endregion

    #region STATEMACHINE LOGIC

    private interface IState
    {
        IState GoToNextState();
        void Enter();
        void Execute();
        void Exit();
    }

    private class StateMachine
    {
        private IState currentState;

        public StateMachine(IState startState)
        {
            currentState = startState;
            startState.Enter();
        }
        
        public void ChangeState(IState newState)
        {
            if (currentState != null)
                currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
            if (currentState != null) currentState.Execute();
        }

        public IState GetCurrentState()
        {
            return currentState;
        }

        public void GoToNextState()
        {
            ChangeState(currentState.GoToNextState());
        }
    }

    #endregion
}
