// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTKController")]
	[Tooltip("Get touchpad touch angle between 0 and 360 in a float and trigger an event when the set range is reached. This action has 6 positions up, right-up, right-down, down, left-down, left-up.")]

	public class  GetTouchpadAngleExtended6 : FsmStateAction

	{
		[RequiredField]
		[Tooltip("VRTK Controller game object.")]
		[Title("Controller.")]
		[CheckForComponent(typeof(VRTK.VRTK_ControllerEvents))]    
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
		[Tooltip("Right-Up.")]
		public FsmEvent position2;
		[Tooltip("Right-Down.")]
		public FsmEvent position3;
		[Tooltip("Down.")]
		public FsmEvent position4;
		[Tooltip("Left-Down.")]
		public FsmEvent position5;		
		[Tooltip("Left-Up.")]
		public FsmEvent position6;
		
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
			position5 = null;
			position6 = null;
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
			if (touchpadAngle.Value >= 0 && touchpadAngle.Value <= 30)
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
			if (touchpadAngle.Value >= 330.1 && touchpadAngle.Value <= 360.1)
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
				if (touchpadAngle.Value >= 30.1 && touchpadAngle.Value <= 90)
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
				if (touchpadAngle.Value >= 30.1 && touchpadAngle.Value <= 90 && touchpadAngle.Value != 90)
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
			if (touchpadAngle.Value >= 90.1 && touchpadAngle.Value <= 150)
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
			if (touchpadAngle.Value >= 150.1 && touchpadAngle.Value <= 210)
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
			
			// position 5 - left - down
			if (touchpadAngle.Value >= 210.1 && touchpadAngle.Value <= 270)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(position5);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(position5);
					}
				}
			}
			
			// position 6 - left - up
			if (touchpadAngle.Value >= 270.1 && touchpadAngle.Value <= 330)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(position6);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(position6);
					}
				}
			}
				
		}

	}
}