using AmuzoBounce.Mechanics;
using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Boundary boundary;
        [SerializeField] private Ball ballPrefab;

        private Camera mainCamera;
        private Ball ball;

        private bool ballIsActive;

        private void Awake()
        {
            mainCamera = Camera.main;

            ball = Instantiate(ballPrefab);
            DisableBall();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                HandleClick();
        }

        private void FixedUpdate()
        {
            if (!boundary.Contains(ball.transform.position))
                EndRound();
        }

        private void HandleClick()
        {
            if (ballIsActive)
                return;
            StartNewRound();
        }

        private void StartNewRound()
        {
            SpawnBall();
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
            Debug.Log("Bounce at " + ball.transform.position);
        }

        private void EndRound()
        {
            DisableBall();
        }

        private void DisableBall()
        {
            ball.gameObject.SetActive(false);
            ball.Bounce -= OnBounce;

            ballIsActive = false;
        }
    }
}