using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    private StateMachine stateMachine;
    [SerializeField] private List<GameObject> HUDGameObjects;
    [SerializeField] private List<GameObject> advancedMenuGameObjects;

    delegate void MakeTransition();

    AdditiveSceneManager additiveSceneManager;
    TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        HUDGameObjects = new List<GameObject>();
        advancedMenuGameObjects = new List<GameObject>();

        FindHUDGameObjects();
        FindAdvancedMenuGameObjects();

        additiveSceneManager = GameObject.FindGameObjectWithTag("AdditiveSceneManager").GetComponent<AdditiveSceneManager>();
        transitionManager = GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<TransitionManager>();

        // Deactivate AdvancedMenu GameObjects right away
        DeactivateAdvancedMenuGameObjects();

        stateMachine = new StateMachine(new HUDState(this));
    }

    void FindHUDGameObjects()
    {
        // HUDGameObjects.Add(GameObject.Find("Room"));
    }

    void FindAdvancedMenuGameObjects()
    {
        // advancedMenuGameObjects.Add(GameObject.Find("AdvancedMenu"));
    }

    void ActivateHUDGameObjects()
    {
        foreach (GameObject HUDGameObject in HUDGameObjects)
            HUDGameObject.SetActive(true);
    }

    void DeactivateHUDGameObjects()
    {
        foreach (GameObject HUDGameObject in HUDGameObjects)
            HUDGameObject.SetActive(false);
    }

    void ActivateAdvancedMenuGameObjects()
    {
        foreach (GameObject advancedMenuGameObject in advancedMenuGameObjects)
            advancedMenuGameObject.SetActive(true);
    }

    void DeactivateAdvancedMenuGameObjects()
    {
        foreach (GameObject advancedMenuGameObject in advancedMenuGameObjects)
            advancedMenuGameObject.SetActive(false);
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
            owner.ActivateHUDGameObjects();
            owner.additiveSceneManager.LoadScene(Scenes.HUD, null, null);
        }

        public void Execute()
        {
            print("HUD");
        }

        public void Exit()
        {
            owner.DeactivateHUDGameObjects();
            owner.additiveSceneManager.UnloadScene(null, null);
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
            owner.ActivateAdvancedMenuGameObjects();
            owner.additiveSceneManager.LoadScene(Scenes.CONSTRUCT, null, null);
        }

        public void Execute()
        {
            print("Advanced Menu");
        }

        public void Exit()
        {
            owner.DeactivateAdvancedMenuGameObjects();
            owner.additiveSceneManager.UnloadScene(null, null);
        }

        public IState GoToNextState()
        {
            return new HUDState(owner);
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
