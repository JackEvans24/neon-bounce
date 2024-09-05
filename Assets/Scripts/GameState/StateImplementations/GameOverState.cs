using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class GameOverState : StateHandler
    {
        public override State State => State.GameOver;

        [Header("References")]
        [SerializeField] private Transform beamParent;

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
            InvokeStateChange(State.ShapePlacement);
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);

            gameOverUI.SetActive(false);
            
            foreach (Transform beam in beamParent)
                Destroy(beam.gameObject);
            ctx.PlacedBeamPositions.Clear();
        }
    }
}