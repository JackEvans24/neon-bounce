using System;
using AmuzoBounce.Data;
using AmuzoBounce.Mechanics;
using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class BallController : MonoBehaviour
    {
        public event Action<BeamType> Bounce;
        public event Action BallLeftArea;

        public bool CanDropBall => !ballIsActive;

        [SerializeField] private Boundary boundary;
        [SerializeField] private Ball ballPrefab;

        private Ball ball;
        private bool ballIsActive;

        private void Awake()
        {
            ball = Instantiate(ballPrefab);
        }
        
        private void Start()
        {
            DisableBall();
        }
        
        private void FixedUpdate()
        {
            if (!BallHasLeftPlayArea())
                return;

            BallLeftArea?.Invoke();
            DisableBall();
        }

        private bool BallHasLeftPlayArea() => ballIsActive && !boundary.Contains(ball.transform.position);

        public void SpawnBall(Vector2 screenPosition)
        {
            var ballPosition = screenPosition;

            ball.transform.position = ballPosition;
            ball.Bounce += OnBounce;
            ball.enabled = true;

            ballIsActive = true;
        }

        private void DisableBall()
        {
            ball.Bounce -= OnBounce;
            ball.enabled = false;

            ballIsActive = false;
        }

        private void OnBounce(BeamType data) => Bounce?.Invoke(data);
    }
}