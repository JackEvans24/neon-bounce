using AmuzoBounce.Data;
using AmuzoBounce.Effects;
using AmuzoBounce.Extensions;
using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BallController ballController;
        [SerializeField] private VolumeIntensifier volumeIntensifier;
        
        [Header("UI")]
        [SerializeField] private RoundDisplay roundDisplay;
        [SerializeField] private ScoreDisplay scoreDisplay;
        [SerializeField] private LivesDisplay livesDisplay;

        private Camera mainCamera;

        private readonly RoundController rounds = new();
        private readonly ScoreController score = new();

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            ballController.Bounce += OnBounce;
            ballController.BallLeftArea += EndLife;

            ResetGame();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                HandleClick();
        }

        private void HandleClick()
        {
            if (ballController.CanDropBall)
                StartNewLife();
        }

        private void StartNewLife()
        {
            scoreDisplay.UpdateDisplay(score.ScoreData, animate: false);

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ballController.SpawnBall(worldPosition.ToVector2());

            rounds.Lives -= 1;
            livesDisplay.SetLives(rounds.Lives);
        }

        private void OnBounce(BeamData beamData)
        {
            score.BumpScore(beamData);
            scoreDisplay.UpdateDisplay(score.ScoreData);
            
            volumeIntensifier.IncreaseIntensity();
        }

        private void EndLife()
        {
            scoreDisplay.UpdateDisplay(score.ScoreData, animate: false);

            if (score.Total >= rounds.RoundData.TargetScore)
                StartNextRound();
            else if (rounds.Lives <= 0)
                ResetGame();
        }

        private void StartNextRound()
        {
            score.ResetScore();
            scoreDisplay.UpdateDisplay(score.ScoreData, animate: false);
            rounds.StartNextRound();
            roundDisplay.UpdateDisplay(rounds.RoundData);
            livesDisplay.SetLives(rounds.Lives);
        }

        private void ResetGame()
        {
            score.ResetScore();
            scoreDisplay.UpdateDisplay(score.ScoreData, animate: false);
            rounds.ResetRounds();
            roundDisplay.UpdateDisplay(rounds.RoundData);
            livesDisplay.SetLives(rounds.Lives);
        }
    }
}