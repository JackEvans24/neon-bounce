using System.Collections.Generic;
using UnityEngine;

namespace AmuzoBounce.GameState
{
    public class StateContext
    {
        public int RoundIndex;
        public List<Vector2> PlacedBeamPositions = new();
    }
}