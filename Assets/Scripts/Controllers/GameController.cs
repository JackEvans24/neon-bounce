using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
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

        private void SpawnBall()
        {
            var mousePosition = Input.mousePosition;
            var ballPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            ballPosition.z = 0f;

            ball.transform.position = ballPosition;
            ball.SetActive(true);
        }
    }
}