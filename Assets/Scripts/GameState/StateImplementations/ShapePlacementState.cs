using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class ShapePlacementState : StateHandler
    {
        public override State State => State.ShapePlacement;

        [Header("References")]
        [SerializeField] private Transform beamParent;
        [SerializeField] private GameObject beamPrefab;

        [Header("UI")]
        [SerializeField] private HintDisplay hintDisplay;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);

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