using AmuzoBounce.Controllers;
using AmuzoBounce.Data;
using AmuzoBounce.Effects;
using AmuzoBounce.Extensions;
using AmuzoBounce.Services;
using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class PlayState : StateHandler
    {
        public override State State => State.Play;

        private const int STARTING_LIVES = 3;
        
        [Header("References")]
        [SerializeField] private BallController ballController;
        [SerializeField] private VolumeIntensifier volumeIntensifier;

        [Header("UI")]
        [SerializeField] private RoundDisplay roundDisplay;
        [SerializeField] private ScoreDisplay scoreDisplay;
        [SerializeField] private LivesDisplay livesDisplay;

        private Camera mainCamera;

        private ScoreData score;
        private uint targetScore;
        private int lives;

        private void Start()
        {
            mainCamera = Camera.main;

            ballController.Bounce += OnBounce;
            ballController.BallLeftArea += EndLife;
        }

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);

            score.Reset();
            targetScore = RoundTargetService.GetTargetScore(ctx.RoundIndex);
            lives = STARTING_LIVES;

            scoreDisplay.UpdateDisplay(score, animate: false);
            roundDisplay.UpdateDisplay(targetScore);
            livesDisplay.SetLives(lives);
        }

        public override void HandleClick()
        {
            base.HandleClick();
            if (ballController.CanDropBall)
                StartNewLife();
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);
            ctx.RoundIndex++;
        }

        private void StartNewLife()
        {
            scoreDisplay.UpdateDisplay(score, animate: false);

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ballController.SpawnBall(worldPosition.ToVector2());

            lives -= 1;
            livesDisplay.SetLives(lives);
        }

        private void OnBounce(BeamData beamData)
        {
            score.BumpScore(beamData);
            scoreDisplay.UpdateDisplay(score);
            
            volumeIntensifier.IncreaseIntensity();
        }

        private void EndLife()
        {
            scoreDisplay.UpdateDisplay(score, animate: false);

            if (score.Total >= targetScore)
                InvokeStateChange(State.ShapePlacement);
            else if (lives <= 0)
                InvokeStateChange(State.GameOver);
        }
    }
}