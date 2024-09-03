using AmuzoBounce.Mechanics;
using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Boundary boundary;
        [SerializeField] private GameObject ballPrefab;

        private Camera mainCamera;
        private GameObject ball;

        private void Awake()
        {
            mainCamera = Camera.main;

            ball = Instantiate(ballPrefab);
            ball.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                SpawnBall();
        }

        private void FixedUpdate()
        {
            if (!boundary.Contains(ball.transform.position))
                DisableBall();
        }

        private void SpawnBall()
        {
            var mousePosition = Input.mousePosition;
            var ballPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            ballPosition.z = 0f;

            ball.transform.position = ballPosition;
            ball.SetActive(true);
        }

        private void DisableBall()
        {
            ball.SetActive(false);
        }
    }
}