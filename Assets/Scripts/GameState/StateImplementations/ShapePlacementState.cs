using System.Collections.Generic;
using AmuzoBounce.Data;
using AmuzoBounce.Mechanics;
using AmuzoBounce.UI;
using UnityEngine;

namespace AmuzoBounce.GameState.StateImplementations
{
    public class ShapePlacementState : StateHandler
    {
        public override State State => State.ShapePlacement;

        private const string PLACE_HINT = "Place beam";
        private const string TOO_CLOSE_HINT = "Beams need space";

        [Header("References")]
        [SerializeField] private Beam beamPrefab;
        [SerializeField] private Transform beamParent;

        [Header("UI")]
        [SerializeField] private Beam beamPreview;
        [SerializeField] private HintDisplay hintDisplay;

        [Header("Restrictions")]
        [SerializeField] private Vector2 minSpacing = Vector2.one;

        private Camera mainCamera;
        private List<Vector2> placedBeamPositions;

        private Beam currentRoundBeam;
        private BeamType beamType;
        private Vector3 beamRotation;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public override void OnStateEnter(StateContext ctx)
        {
            base.OnStateEnter(ctx);

            placedBeamPositions = ctx.PlacedBeamPositions;

            var roundType = ctx.RoundIndex % 2;
            beamType = (BeamType)roundType;

            var maxRotation = Mathf.Min(7, ctx.RoundIndex + 2);
            beamRotation.z = Random.Range(1, maxRotation) * 10;
            if (roundType % 2 == 0)
                beamRotation.z *= -1;
            
            beamPreview.Initialise(beamType);
            beamPreview.transform.eulerAngles = beamRotation;
            beamPreview.gameObject.SetActive(true);
            
            currentRoundBeam = Instantiate(beamPrefab, beamParent);
            currentRoundBeam.transform.eulerAngles = beamRotation;
            currentRoundBeam.Initialise(beamType);
            currentRoundBeam.gameObject.SetActive(false);

            hintDisplay.gameObject.SetActive(true);
            hintDisplay.UpdateText(PLACE_HINT);
        }

        public override void HandleClick()
        {
            base.HandleClick();
            
            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            
            currentRoundBeam.transform.position = worldPosition;
            currentRoundBeam.gameObject.SetActive(true);

            if (!IsValidPlacement(worldPosition))
            {
                hintDisplay.UpdateText(TOO_CLOSE_HINT);
                return;
            }

            InvokeStateChange(State.Play);
        }

        private bool IsValidPlacement(Vector2 position)
        {
            foreach (var beamPosition in placedBeamPositions)
            {
                var xDistance = Mathf.Abs(position.x - beamPosition.x);
                var yDistance = Mathf.Abs(position.y - beamPosition.y);
                Debug.Log($"{xDistance}, {yDistance}");

                if (xDistance < minSpacing.x && yDistance < minSpacing.y)
                    return false;
            }

            return true;
        }

        public override void OnStateExit(StateContext ctx)
        {
            base.OnStateExit(ctx);
            
            ctx.PlacedBeamPositions.Add(currentRoundBeam.transform.position);

            hintDisplay.gameObject.SetActive(false);
            beamPreview.gameObject.SetActive(false);
        }
    }
}