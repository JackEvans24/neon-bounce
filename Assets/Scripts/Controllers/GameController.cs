using AmuzoBounce.Mechanics;
using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Boundary boundary;
        [SerializeField] private Ball ballPrefab;
        
        [Header("UI")]
        [SerializeField] private RoundDisplay roundDisplay;
        [SerializeField] private ScoreDisplay scoreDisplay;

        private Camera mainCamera;
        private Ball ball;

        private readonly RoundController rounds = new();
        private readonly ScoreController score = new();

        private bool ballIsActive;

        private void Awake()
        {
            mainCamera = Camera.main;

            ball = Instantiate(ballPrefab);
            DisableBall();
            
            roundDisplay.UpdateDisplay(rounds.RoundData);
            scoreDisplay.UpdateDisplay(score.ScoreData);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                HandleClick();
        }

        private void FixedUpdate()
        {
            if (BallHasLeftPlayArea())
                EndLife();
        }

        private bool BallHasLeftPlayArea() => ballIsActive && !boundary.Contains(ball.transform.position);

        private void HandleClick()
        {
            if (ballIsActive)
                return;
            StartNewLife();
        }

        private void StartNewLife()
        {
            SpawnBall();
            rounds.Lives -= 1;
        }

        private void SpawnBall()
        {
            var mousePosition = Input.mousePosition;
            var ballPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            ballPosition.z = 0f;

            ball.transform.position = ballPosition;
            ball.gameObject.SetActive(true);
            ball.Bounce += OnBounce;

            ballIsActive = true;
        }

        private void OnBounce()
        {
            score.AddCurrentScoreToTotal();
            score.CurrentScoreTicker += 1;
            
            scoreDisplay.UpdateDisplay(score.ScoreData);
        }

        private void EndLife()
        {
            DisableBall();

            score.ResetLifeScores();
            scoreDisplay.UpdateDisplay(score.ScoreData);

            if (score.CurrentScoreTotal >= rounds.CurrentTarget)
                StartNextRound();
            else if (rounds.Lives <= 0)
                ResetGame();
        }

        private void DisableBall()
        {
            ball.gameObject.SetActive(false);
            ball.Bounce -= OnBounce;

            ballIsActive = false;
        }

        private void StartNextRound()
        {
            score.ResetScore();
            scoreDisplay.UpdateDisplay(score.ScoreData);
            rounds.StartNextRound();
            roundDisplay.UpdateDisplay(rounds.RoundData);
        }

        private void ResetGame()
        {
            score.ResetScore();
            scoreDisplay.UpdateDisplay(score.ScoreData);
            rounds.ResetRounds();
            roundDisplay.UpdateDisplay(rounds.RoundData);
        }
    }
}