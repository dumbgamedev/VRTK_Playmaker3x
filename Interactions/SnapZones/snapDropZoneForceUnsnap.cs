 //Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Interaction")]
	[Tooltip("Force drop / unsnap object from a snap zone.")]

	public class  snapDropZoneForceUnsnap : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK.VRTK_SnapDropZone))]    
		public FsmOwnerDefault gameObject;
		
		public FsmBool everyFrame;

		VRTK.VRTK_SnapDropZone snapzone;

		public override void Reset()
		{

			gameObject = null;
			everyFrame = false;
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

			snapzone.ForceUnsnap();

		}

	}
}