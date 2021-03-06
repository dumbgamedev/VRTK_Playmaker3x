// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;
using VRTK.Controllables.ArtificialBased;
//using VRTK.Controllables.ArtificialBased;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK")]
	[Tooltip("Get VRTK Artificial Rotator values.")]

	public class getArtificialRotator : FsmStateAction
	{
        // Playmaker variables

		[RequiredField]
		[CheckForComponent(typeof(VRTK_ArtificialRotator))]
		[Tooltip("Artificial Rotator Game Object.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Angle of rotator.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat returnedValue;
		[Tooltip("Normalize angle of rotator between 0 and 1f.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat normalizedValue;
		[Tooltip("Returns the GameObject that is generated to hold the rotator control.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject rotatorContainer;
		[Tooltip("Returns the current angle of the rotator based on the step value range.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat stepValue;

     	public FsmBool everyFrame;

		// private variables
		private VRTK_ArtificialRotator controllable;

        public override void Reset()
		{

			everyFrame = false;
			returnedValue = null;
			gameObject = null;
			stepValue = null;
			rotatorContainer = null;
			normalizedValue = null;

		}

		public override void OnEnter()
		{
			
			// get components from game objects
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			controllable = go.GetComponent<VRTK_ArtificialRotator>();

			checkRotator();
			
			if (!everyFrame.Value)
			{
				Finish();
			}

        }

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				checkRotator();
			}
		}

		void checkRotator()
		{
			returnedValue.Value = controllable.GetValue();
			normalizedValue.Value = controllable.GetNormalizedValue();
			rotatorContainer.Value = controllable.GetContainer();
			stepValue.Value = controllable.GetStepValue(returnedValue.Value);

		}

	}
}