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
        private const string DROP_HINT = "Drop Ball";
        private const string WAIT_HINT = "Await Points";
        
        [Header("References")]
        [SerializeField] private BallController ballController;
        [SerializeField] private VolumeIntensifier volumeIntensifier;

        [Header("UI")]
        [SerializeField] private HintDisplay hintDisplay;
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

            hintDisplay.gameObject.SetActive(true);
            hintDisplay.UpdateText(DROP_HINT);

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
            hintDisplay.gameObject.SetActive(false);
        }

        private void StartNewLife()
        {
            scoreDisplay.UpdateDisplay(score, animate: false);
            hintDisplay.UpdateText(WAIT_HINT);

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ballController.SpawnBall(worldPosition.ToVector2());

            lives -= 1;
            livesDisplay.SetLives(lives);
        }

        private void OnBounce(BeamType beamType)
        {
            score.BumpScore(beamType);
            scoreDisplay.UpdateDisplay(score);
            
            volumeIntensifier.IncreaseIntensity();
        }

        private void EndLife()
        {
            scoreDisplay.UpdateDisplay(score, animate: false);
            hintDisplay.UpdateText(DROP_HINT);

            if (score.Total >= targetScore)
                InvokeStateChange(State.ShapePlacement);
            else if (lives <= 0)
                InvokeStateChange(State.GameOver);
        }
    }
}