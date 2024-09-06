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
        public bool AssistMode;

        [SerializeField] private Boundary boundary;
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform lastDropPoint;
        [SerializeField] private BallTracer ballTracer;

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

            lastDropPoint.position = ballPosition;
            lastDropPoint.gameObject.SetActive(AssistMode);

            ballIsActive = true;
            
            ballTracer.StartRecording(ball.transform);
        }

        private void DisableBall()
        {
            ball.Bounce -= OnBounce;
            ball.enabled = false;

            ballIsActive = false;
            
            ballTracer.StopRecording();
        }

        public void ResetDropPointIndicator() => lastDropPoint.gameObject.SetActive(false);

        public void SetTracerActive(bool active) => ballTracer.SetLineActive(AssistMode && active);

        private void OnBounce(BeamType data) => Bounce?.Invoke(data);
    }
}