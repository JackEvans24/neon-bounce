using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class GameOverState : StateHandler
    {
        public override State State => State.GameOver;

        [Header("UI")]
        [SerializeField] private GameObject gameOverUI;

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);
            ctx.RoundIndex = 0;
            gameOverUI.SetActive(true);
        }

        public override void HandleClick()
        {
            base.HandleClick();
            // TODO: Change to whatever the default first state is
            InvokeStateChange(State.Play);
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);
            gameOverUI.SetActive(false);
        }
    }
}