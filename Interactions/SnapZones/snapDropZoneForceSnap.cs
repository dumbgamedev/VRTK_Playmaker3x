 //Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Interaction")]
	[Tooltip("Force snap a gameobject to a snap zone.")]

	public class  snapDropZoneForceSnap : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK.VRTK_SnapDropZone))]    
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmGameObject objectToSnap;
		
		public FsmBool everyFrame;

		private VRTK.VRTK_SnapDropZone snapzone;

		public override void Reset()
		{

			gameObject = null;
			everyFrame = false;
			objectToSnap = new FsmGameObject {UseVariable = true};
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			snapzone = go.GetComponent<VRTK.VRTK_SnapDropZone>();

			doSnappy();
			
			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				doSnappy();
			}
		}


		void doSnappy()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			snapzone.ForceSnap(objectToSnap.Value);
		}

	}
}