using System.Collections;
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
        
        private const string TARGET_HINT = "Target updated";
        private const string LIVES_HINT = "Lives refilled";
        private const string DROP_HINT = "Click to drop ball";
        private const string WAIT_HINT = "Await Points";
        private const string COMPLETE_HINT = "Nice!";

        private readonly WaitForSeconds hintWait = new(1f);
        
        [Header("References")]
        [SerializeField] private BallController ballController;
        [SerializeField] private VolumeIntensifier volumeIntensifier;
        [SerializeField] private CameraShake cameraShake;
        [SerializeField] private ParticleSystem completeParticles;

        [Header("UI")]
        [SerializeField] private HintDisplay hintDisplay;
        [SerializeField] private RoundDisplay roundDisplay;
        [SerializeField] private ScoreDisplay scoreDisplay;
        [SerializeField] private LivesDisplay livesDisplay;

        private Camera mainCamera;
        private Coroutine setupCoroutine;

        private ScoreData score;
        private uint targetScore;
        private int lives;
        private bool canDropBall;
        private bool setupRunning;

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
            canDropBall = true;

            ballController.AssistMode = ctx.AssistMode;

            scoreDisplay.UpdateDisplay(score, animate: false);

            setupCoroutine = StartCoroutine(HandleRoundSetup());
        }

        public override void HandleClick()
        {
            base.HandleClick();

            if (setupRunning)
                CancelRoundSetup();

            if (canDropBall && ballController.CanDropBall)
                StartNewLife();
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);
            
            ctx.RoundIndex++;
            hintDisplay.gameObject.SetActive(false);
        }

        private IEnumerator HandleRoundSetup()
        {
            setupRunning = true;
            hintDisplay.gameObject.SetActive(true);
            
            roundDisplay.UpdateDisplay(targetScore);
            hintDisplay.UpdateText(TARGET_HINT);
            yield return hintWait;

            livesDisplay.SetLives(lives);
            hintDisplay.UpdateText(LIVES_HINT);
            yield return hintWait;

            hintDisplay.UpdateText(DROP_HINT);
            setupRunning = false;
        }

        private void CancelRoundSetup()
        {
            StopCoroutine(setupCoroutine);
            roundDisplay.UpdateDisplay(targetScore);
            livesDisplay.SetLives(lives);
            setupRunning = false;
        }

        private void StartNewLife()
        {
            scoreDisplay.UpdateDisplay(score, animate: false);
            hintDisplay.UpdateText(WAIT_HINT);

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ballController.SpawnBall(worldPosition.ToVector2());
            ballController.SetAssistItemsActive(false);

            lives -= 1;
            livesDisplay.SetLives(lives);
        }

        private void OnBounce(BeamType beamType)
        {
            score.BumpScore(beamType);
            scoreDisplay.UpdateDisplay(score);
            
            volumeIntensifier.IncreaseIntensity();
            cameraShake.AddImpulse();
        }

        private void EndLife()
        {
            scoreDisplay.UpdateDisplay(score, animate: false);
            hintDisplay.UpdateText(DROP_HINT);

            ballController.SetAssistItemsActive(true);

            if (score.Total >= targetScore)
                StartCoroutine(TriggerShapePlacement());
            else if (lives <= 0)
                GoToGameOver();
        }

        private IEnumerator TriggerShapePlacement()
        {
            canDropBall = false;
            
            completeParticles.Play();

            hintDisplay.UpdateText(COMPLETE_HINT);
            yield return hintWait;

            InvokeStateChange(State.ShapePlacement);
        }

        private void GoToGameOver()
        {
            ballController.SetAssistItemsActive(false);
            InvokeStateChange(State.GameOver);
        }
    }
}