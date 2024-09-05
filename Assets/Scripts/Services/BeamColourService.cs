using AmuzoBounce.Data;
using UnityEngine;

namespace AmuzoBounce.Services
{
    public static class BeamColourService
    {
        private static Color BEAM_GREEN = new Color(0.2666667f, 0.8f, 0.2666667f);
        private static Color BEAM_PINK = new Color(0.8018868f, 0.2685564f, 0.7908309f);

        private static GradientAlphaKey[] ALPHA_KEYS = { new GradientAlphaKey(1f, 1f) };

        public static Color GetBeamColour(BeamType type) =>
            type == BeamType.Green ? BEAM_GREEN : BEAM_PINK;

        public static Gradient GetBeamGradient(BeamType type)
        {
            var beamColour = GetBeamColour(type);
            
            var gradient = new Gradient();
            gradient.SetKeys(new[]
            {
                new GradientColorKey(beamColour, 0f),
                new GradientColorKey(Color.white, 1f),
            }, ALPHA_KEYS);

            return gradient;
        }
    }
}