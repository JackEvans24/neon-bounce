using System.Collections.Generic;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class LivesDisplay : MonoBehaviour
    {
        private readonly List<Transform> children = new();

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                children.Add(child);
            }
        }

        public void SetLives(int lives)
        {
            var index = 0;
            foreach (var child in children)
            {
                child.gameObject.SetActive(index < lives);
                index++;
            }
        }
    }
}