using AmuzoBounce.Data;
using AmuzoBounce.Mechanics;
using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class ShapePlacementState : StateHandler
    {
        public override State State => State.ShapePlacement;

        [Header("References")]
        [SerializeField] private Beam beamPrefab;
        [SerializeField] private Transform beamParent;

        [Header("UI")]
        [SerializeField] private Beam beamPreview;
        [SerializeField] private HintDisplay hintDisplay;

        private Camera mainCamera;

        private BeamType beamType;
        private Vector3 beamRotation;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);

            var roundType = ctx.RoundIndex % 2;
            beamType = (BeamType)roundType;

            beamRotation.z = Random.Range(1, 7) * 10;
            if (roundType % 2 == 0)
                beamRotation.z *= -1;
            
            beamPreview.Initialise(beamType);
            beamPreview.transform.eulerAngles = beamRotation;
            beamPreview.gameObject.SetActive(true);

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
            beam.transform.eulerAngles = beamRotation;
            beam.Initialise(beamType);

            InvokeStateChange(State.Play);
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);

            hintDisplay.gameObject.SetActive(false);
            beamPreview.gameObject.SetActive(false);
        }
    }
}