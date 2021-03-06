// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Pointer")]
	[Tooltip("Checks is the active pointer is hitting a valid object.")]

	public class  getPointerValidInfo : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK.VRTK_Pointer))]    
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("This bool is true when the pointer is hitting a valid target")]
		public FsmBool validHit;
		
		public FsmBool everyFrame;

		VRTK_Pointer pointer;

		public override void Reset()
		{

			validHit = null;
			gameObject = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			pointer = go.GetComponent<VRTK_Pointer>();
			
			checkPointer();

			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				checkPointer();
			}
		}


		void checkPointer()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			validHit.Value = pointer.IsStateValid();
		
				
		}

	}
}