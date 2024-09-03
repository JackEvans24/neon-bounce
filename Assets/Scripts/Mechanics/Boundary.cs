using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    public class Boundary : MonoBehaviour
    {
        [SerializeField] private float margin = 2f;
        private Camera mainCamera;

        private Rect boundary;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            var cameraDepth = mainCamera.transform.position.z;
            var min = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, cameraDepth));
            var max = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraDepth));

            boundary = new Rect(min.x - margin, min.y - margin, max.x - min.x + margin, max.y - min.y + margin);
        }

        public bool Contains(Vector2 point)
        {
            return boundary.Contains(point);
        }
    }
}