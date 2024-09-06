using System.Collections.Generic;
using UnityEngine;

namespace AmuzoBounce.GameState
{
    public class StateContext
    {
        public bool AssistMode;
        public int RoundIndex;
        public readonly List<Vector2> PlacedBeamPositions = new();
    }
}