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
        // private float beamRotation;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);

            beamType = (BeamType)(ctx.RoundIndex % 2);

            // beamRotation = Random.Range(1, 5) * 10;
            // if (Random.Range(0, 2) / 2 == 0)
            //     beamRotation *= -1;
            
            beamPreview.Initialise(beamType);
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
            // beam.transform.eulerAngles = new Vector3(0, 0, beamRotation);
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