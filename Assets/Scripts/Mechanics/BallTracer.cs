using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    public class BallTracer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LineRenderer line;

        [Header("Tracing")]
        [SerializeField] private int pointCount = 30;
        [SerializeField] private float recordingOffset = 0.05f;

        private readonly Queue<Vector3> points = new();
        private Transform target;
        private bool record;
        private float currentTime;

        private void FixedUpdate()
        {
            currentTime += Time.fixedDeltaTime;
            if (!record || currentTime < recordingOffset)
                return;

            AddPoint();
            currentTime = 0f;
        }

        public void StartRecording(Transform newTarget)
        {
            target = newTarget;
            points.Clear();
            currentTime = 0f;
            record = true;
        }

        public void StopRecording()
        {
            record = false;
        }

        public void SetLineActive(bool active)
        {
            var showLine = active && points.Count > 0;
            if (showLine)
            {
                line.positionCount = points.Count;
                line.SetPositions(points.ToArray());
            }

            line.enabled = showLine;
        }

        private void AddPoint()
        {
            points.Enqueue(target.position);
            if (points.Count > pointCount)
                points.Dequeue();
        }
    }
}