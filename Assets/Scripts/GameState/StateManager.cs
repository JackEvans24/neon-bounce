using System.Collections.Generic;
using UnityEngine;

namespace AmuzoBounce.GameState
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private State defaultState = State.Play;

        private readonly Dictionary<State, StateHandler> states = new();
        private State currentState;

        private readonly StateContext context = new();

        private void Awake()
        {
            foreach (Transform child in transform)
                AddState(child);
        }

        private void AddState(Transform child)
        {
            var stateHandler = child.GetComponent<StateHandler>();
            if (stateHandler == null)
            {
                Debug.LogError($"Trying to add unimplemented state: {child.name}");
                return;
            }

            if (states.ContainsKey(stateHandler.State))
            {
                Debug.LogError($"Trying to add duplicate state: {stateHandler.State}");
                return;
            }
            
            states.Add(stateHandler.State, stateHandler);
            stateHandler.StateChangeRequested += OnStateChangeRequested;
        }

        private void Start()
        {
            OnStateChangeRequested(defaultState);
        }

        private void OnDestroy()
        {
            foreach (var (_, stateHandler) in states)
                stateHandler.StateChangeRequested -= OnStateChangeRequested;
        }

        private void OnStateChangeRequested(State newState)
        {
            // Warn if moving to same state
            if (newState == currentState)
                Debug.LogWarning($"Trying to change to currently active state: {newState}");
            
            // Exit current state
            if (states.TryGetValue(currentState, out var currentStateHandler))
                currentStateHandler.OnStateExit(context);
            // Warn if this fails
            else
                Debug.LogWarning($"Unable to process state exit: {currentState}, state not defined");

            // Check new state is implemented
            if (!states.TryGetValue(newState, out var newStateHandler))
            {
                currentState = State.None;
                Debug.LogError($"Trying to change to unimplemented state: {newState}");
                return;
            }
            
            // Move to new state
            newStateHandler.OnStateEnter(context);
            currentState = newState;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (states.TryGetValue(currentState, out var stateHandler))
                    stateHandler.HandleClick();
            }
        }
    }
}