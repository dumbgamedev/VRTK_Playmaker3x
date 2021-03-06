// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTKController")]
	[Tooltip("Get touchpad touch angle between 0 and 360 in a float and trigger an event when the set range is reached. This action has 4 positions up, right, down and left.")]

	public class  GetTouchpadAngleExtended4 : FsmStateAction

	{
		[RequiredField]
		[Tooltip("VRTK Controller game object.")]
		[Title("Controller.")]
		[CheckForComponent(typeof(VRTK_ControllerEvents))]    
		public FsmOwnerDefault gameObject;

		[ActionSection("Options")]
		
		[Title("Require Click")]
		[Tooltip("If set to true, the event will not fire until the touchpad is clicked fully down.")]
		public FsmBool requireTrigger;
		[Tooltip("Exclude the default position of exactly 90 degrees.")]
		public FsmBool excludeDefaultPos;
		
		[ActionSection("Output")]
		
		[Tooltip("Up.")]
		public FsmEvent position1;
		[Tooltip("Right.")]
		public FsmEvent position2;
		[Tooltip("Down.")]
		public FsmEvent position3;
		[Tooltip("Left.")]
		public FsmEvent position4;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat touchpadAngle;

		VRTK.VRTK_ControllerEvents theScript;

		public override void Reset()
		{

			touchpadAngle = new FsmFloat {UseVariable = true};
			gameObject = null;
			position1 = null;
			position2 = null;
			position3 = null;
			position4 = null;
			excludeDefaultPos = true;
			
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			theScript = go.GetComponent<VRTK.VRTK_ControllerEvents>();
		}

		public override void OnUpdate()
		{
				MakeItSo();
		}


		void MakeItSo()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				Finish();
			}

			touchpadAngle.Value = theScript.GetTouchpadAxisAngle();
			
			// position 1 up A
			if (touchpadAngle.Value >= 0 && touchpadAngle.Value <= 45)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(position1);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(position1);
					}
				}
			}
			
			// position 1 up B
			if (touchpadAngle.Value >= 315 && touchpadAngle.Value <= 360.1)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(position1);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(position1);
					}
				}
			}
			
			// position 2 right - up. NOT excluding 90
			
			if(!excludeDefaultPos.Value){
				if (touchpadAngle.Value >= 45.1 && touchpadAngle.Value <= 135)
				{
					
					if(!requireTrigger.Value)
					{
						Fsm.Event(position2);
					}
					
					if(requireTrigger.Value)
					{
						if(theScript.touchpadPressed)
						{
							Fsm.Event(position2);
						}
					}
				}
			}
			
			// position 2 right - up. Excluding 90
			
			if(excludeDefaultPos.Value){
				if (touchpadAngle.Value >= 45.1 && touchpadAngle.Value <= 135 && touchpadAngle.Value != 90)
				{
					
					if(!requireTrigger.Value)
					{
						Fsm.Event(position2);
					}
					
					if(requireTrigger.Value)
					{
						if(theScript.touchpadPressed)
						{
							Fsm.Event(position2);
						}
					}
				}
			}
			
			// position 3 right - down
			if (touchpadAngle.Value >= 135.1 && touchpadAngle.Value <= 225)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(position3);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(position3);
					}
				}
			}
			
			// position 4 - down
			if (touchpadAngle.Value >= 225.1 && touchpadAngle.Value <= 315)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(position4);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(position4);
					}
				}
			}
				
		}

	}
}