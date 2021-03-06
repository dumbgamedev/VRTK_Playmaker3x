// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Pointer")]
	[Tooltip("Checks what the pointer renderer is hitting.")]

	public class  getStraightPointerHitObject : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK_StraightPointerRenderer))]   
		[Tooltip("Alias controller with straight pointer script")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Output")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("This bool is true when the pointer is hitting a game object")]
		public FsmBool objectHit;
		[UIHint(UIHint.Variable)]
		public FsmVector3 hitNormal;
		[UIHint(UIHint.Variable)]
		public FsmVector3 hitPoint;
		[UIHint(UIHint.Variable)]
		public FsmFloat hitDistance;
		[UIHint(UIHint.Variable)]
		public FsmGameObject hitObject;
		
		[ActionSection("Options")]

		public FsmBool everyFrame;

		VRTK_BasePointerRenderer pointer;
		RaycastHit rayHit;

		public override void Reset()
		{

			objectHit = false;
			hitNormal = null;
			hitPoint = null;
			hitDistance = null;
			gameObject = null;
			everyFrame = false;
			hitObject = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			pointer = go.GetComponent<VRTK_StraightPointerRenderer>();
			
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

			rayHit = pointer.GetDestinationHit();

			if(rayHit.transform != null)
			{
				hitObject.Value = rayHit.transform.gameObject;
				hitNormal.Value = rayHit.normal;
				hitPoint.Value = rayHit.point;
				hitDistance.Value = rayHit.distance;
				
			}
				

			else
			{
				hitObject.Value = null;
				hitNormal.Value = new Vector3(0,0,0);
				hitPoint.Value = new Vector3(0,0,0);
				hitDistance.Value = 0;
			}
						
				
		}

	}
}