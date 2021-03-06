// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Pointer")]
	[Tooltip("Activates and deactivates teleporting and pointers for VRTK Pointer by a Playmaker Action.")]

	public class  activateTeleporting : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK_Pointer))]
		[Title("Controller")]
		public FsmOwnerDefault gameObject;

		public FsmBool enableHeightAdjustTele;
		public FsmBool enablePointer;
		public FsmBool enableBezierRenderer;
		public FsmBool enableStraightRenderer;
		
		public FsmGameObject bodyPhysicsGO;
		public FsmBool enableBodyTeleport;

		private VRTK_Pointer pointer;
		private VRTK_BezierPointerRenderer bezier;
		private VRTK_HeightAdjustTeleport heightTeleport;
		private VRTK_StraightPointerRenderer straight;
		private VRTK_BodyPhysics bodyPhysics;
		

		public override void Reset()
		{

			enablePointer = new FsmBool{UseVariable = true};
			enableHeightAdjustTele = new FsmBool{UseVariable = true};
			enableStraightRenderer = new FsmBool{UseVariable = true};
			enableBezierRenderer = new FsmBool{UseVariable = true};
			enableBodyTeleport = new FsmBool{UseVariable = true};
			bodyPhysicsGO = new FsmGameObject{UseVariable = true};
			gameObject = null;
			
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			MakeItSo();
			Finish();

		}


		void MakeItSo()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			if(!enablePointer.IsNone)
			{
				pointer = go.GetComponent<VRTK_Pointer>();
				pointer.enabled = enablePointer.Value;
			}
			
			// set pointer renderer to be beizer
			if(enableBezierRenderer.Value)
			{
				bezier = go.GetComponent<VRTK_BezierPointerRenderer>();
				pointer.pointerRenderer = bezier;
			}
			
			// set pointer renderer to be straight
			if(enableStraightRenderer.Value)
			{
				straight = go.GetComponent<VRTK_StraightPointerRenderer>();
				pointer.pointerRenderer = straight;
			}
			
			if(!enableBezierRenderer.IsNone)
			{
				bezier = go.GetComponent<VRTK_BezierPointerRenderer>();
				bezier.enabled = enableBezierRenderer.Value;
			}	
			
			if(!enableHeightAdjustTele.IsNone)
			{
				heightTeleport = go.GetComponent<VRTK_HeightAdjustTeleport>();
				heightTeleport.enabled = enableHeightAdjustTele.Value;
			}	
			
			if(!enableStraightRenderer.IsNone)
			{
				straight = go.GetComponent<VRTK_StraightPointerRenderer>();
				straight.enabled = enableStraightRenderer.Value;
			}
			
			if(!enableBodyTeleport.IsNone)
			{
				bodyPhysics = bodyPhysicsGO.Value.GetComponent<VRTK_BodyPhysics>();
				bodyPhysics.enableTeleport = enableBodyTeleport.Value;
			}
		}

	}
}