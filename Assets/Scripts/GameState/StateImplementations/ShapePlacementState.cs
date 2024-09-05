using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class ShapePlacementState : StateHandler
    {
        public override State State => State.ShapePlacement;

        [Header("References")]
        [SerializeField] private GameObject[] beamPrefabs;
        [SerializeField] private Transform beamParent;

        [Header("UI")]
        [SerializeField] private HintDisplay hintDisplay;

        private Camera mainCamera;
        private GameObject beamPrefab;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);

            beamPrefab = beamPrefabs[ctx.RoundIndex % beamPrefabs.Length];

            hintDisplay.gameObject.SetActive(true);
            hintDisplay.UpdateText("Place beam");
        }

        public override void HandleClick()
        {
            base.HandleClick();
            
            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            
            var beam = Instantiate(beamPrefab, beamParent);
            beam.transform.position = worldPosition;
            
            InvokeStateChange(State.Play);
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);
            hintDisplay.gameObject.SetActive(false);
        }
    }
}