using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class StateManager : Singleton<StateManager> {
    //private StateMachine stateMachine;
    private States currentState;

    delegate void MakeTransition(); // What is this for?

    AdditiveSceneManager additiveSceneManager;
    TransitionManager transitionManager;

    public enum States
    {
        HUD, Construct
    }

    // Start is called before the first frame update
    void Start() {
        additiveSceneManager = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
        transitionManager = GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>();

        currentState = States.HUD;
        //stateMachine = new StateMachine(new HUDState(this));
    }

    public void GoToNextState() {
        switch (currentState)
        {
            case States.HUD:
                additiveSceneManager.ChangeScene(Scenes.CONSTRUCT, null, null, null, DelegateAfterConstructLoad);
                currentState = States.Construct;
                break;
            case States.Construct:
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
    void DelegateAfterConstructLoad()
    {
        Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
        Transform leftSenseGlove = GameObject.FindGameObjectWithTag("SenseGloveLeft").transform;
        leftSenseGlove.SetParent(cameraOrigin, false);
        leftSenseGlove.GetChild(1).GetComponent<ShowOpenMenuButton>().CompareObject = cameraOrigin;
        leftSenseGlove.GetComponent<SteamVR_TrackedObject>().enabled = true;
        GameObject.FindGameObjectWithTag("SenseGloveRight").transform.SetParent(cameraOrigin, false);

        GameObject constructObjects = GameObject.FindGameObjectWithTag("ConstructObjects");
        GameObject roboy = GameObject.FindGameObjectWithTag("Roboy");

        roboy.transform.position = cameraOrigin.position + new Vector3(0f, 1.4f, 0f);
        roboy.transform.rotation = Quaternion.Euler(roboy.transform.rotation.eulerAngles + cameraOrigin.rotation.eulerAngles);

        GameObject.FindGameObjectWithTag("SubMenu3D").transform.SetParent(cameraOrigin, false);
        /*Transform constructObjects = GameObject.FindGameObjectWithTag("ConstructObjects").transform;
        Transform roboy = constructObjects.GetChild(0);
        roboy.position = cameraOrigin.position + new Vector3(0f, 1.5f, 0f);
        roboy.rotation = Quaternion.Euler(roboy.rotation.eulerAngles + cameraOrigin.rotation.eulerAngles);
        constructObjects.GetChild(1).SetParent(cameraOrigin, false);*/
    }

    void DelegateOnConstructUnload()
    {
        Destroy(GameObject.FindGameObjectWithTag("SenseGloveLeft"));
        Destroy(GameObject.FindGameObjectWithTag("SenseGloveRight"));
        Transform cameraOrigin = GameObject.FindGameObjectWithTag("CameraOrigin").transform;
        for(int i = 0; i < cameraOrigin.childCount; i++)
        {
            if (cameraOrigin.GetChild(i).CompareTag("SubMenu3D"))
            {
                Destroy(cameraOrigin.GetChild(i).gameObject);
            }
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