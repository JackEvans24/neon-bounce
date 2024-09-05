using System;
using UnityEngine;

namespace AmuzoBounce.GameState
{
    public abstract class StateHandler : MonoBehaviour
    {
        public event Action<State> StateChangeRequested;

        public abstract State State { get; }
        
        public virtual void OnStateEnter(StateContext ctx) { }
        public virtual void HandleClick() { }
        public virtual void OnStateExit(StateContext ctx) { }

        protected void InvokeStateChange(State state) => StateChangeRequested?.Invoke(state);
    }
}