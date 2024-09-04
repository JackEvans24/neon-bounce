using UnityEngine;

namespace AmuzoBounce.Data
{
    [CreateAssetMenu(menuName = "BeamConfig")]
    public class BeamData : ScriptableObject
    {
        public enum BeamType { Pink, Green }

        public BeamType Type;
    }
}