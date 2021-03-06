// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTKController")]
	[Tooltip("Get touchpad touch angle between 0 and 360. Compare to see if it is within or out of range.")]

	public class  GetTouchpadAngleCompare : FsmStateAction

	{
		[RequiredField]
		[Tooltip("VRTK Controller game object.")]
		[Title("Controller.")]
		[CheckForComponent(typeof(VRTK_ControllerEvents))]    
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("This should be the lowest number. No lower than zero.")]
		public FsmFloat startRange;
		[RequiredField]
		[Tooltip("This should be the highest number. No higher than 360.")]
		public FsmFloat endRange;
		
		[ActionSection("Optional Second Range")]
		
		[Tooltip("This should be the lowest number. No lower than zero.")]
		public FsmFloat startRangeTwo;
		[Tooltip("This should be the highest number. No higher than 360.")]
		public FsmFloat endRangeTwo;

		[ActionSection("Options")]
		
		[Title("Require Click")]
		[Tooltip("If set to true, the event will not fire until the touchpad is clicked fully down.")]
		public FsmBool requireTrigger;

		
		[ActionSection("Output")]
		
		[Tooltip("Within set number Range")]
		public FsmEvent inRange;
		[Tooltip("Outside of set number range.")]
		public FsmEvent outOfRange;

		
		[UIHint(UIHint.Variable)]
		public FsmFloat touchpadAngle;

		VRTK.VRTK_ControllerEvents theScript;

		public override void Reset()
		{

			touchpadAngle = new FsmFloat {UseVariable = true};
			gameObject = null;
			inRange = null;
			outOfRange = null;
			startRange = 0;
			endRange = 0;
			startRangeTwo = new FsmFloat {UseVariable = true};
			endRangeTwo = new FsmFloat {UseVariable = true};
			
			
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			theScript = go.GetComponent<VRTK.VRTK_ControllerEvents>();
		}

		public override void OnUpdate()
		{
			if(!startRangeTwo.IsNone && !endRangeTwo.IsNone)
			{
				doubleRange();
			}
			
			else
			{
				singleRange();
			}
		}

// Check a single range of numbers
		void singleRange()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				Finish();
			}
			
			// get touch pad script
			touchpadAngle.Value = theScript.GetTouchpadAxisAngle();
			
				// check if touchpad is within the range
				if (touchpadAngle.Value >= startRange.Value && touchpadAngle.Value <= endRange.Value)
				{
					
					if(!requireTrigger.Value)
					{
						Fsm.Event(inRange);
					}
					
					if(requireTrigger.Value)
					{
						if(theScript.touchpadPressed)
						{
							Fsm.Event(inRange);
						}
					}
				}
			
				// else if not in range, then do out of range.
				else
				{
					if(!requireTrigger.Value)
					{
						Fsm.Event(outOfRange);
					}
					
					if(requireTrigger.Value)
					{
						if(theScript.touchpadPressed)
						{
							Fsm.Event(outOfRange);
						}
					}				
				}
		}
		
		// If use double number range, run this method.
		void doubleRange()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				Finish();
			}
			
			touchpadAngle.Value = theScript.GetTouchpadAxisAngle();
			
			// check first range. If so, do in range event.
			if (touchpadAngle.Value >= startRange.Value && touchpadAngle.Value <= endRange.Value)
			{
				
				if(!requireTrigger.Value)
				{
					Fsm.Event(inRange);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(inRange);
					}
				}
				
				Debug.Log("Range One " + touchpadAngle.Value);
				return;
			}
			
			// if not the first range, then check the second range.
			else if (touchpadAngle.Value >= startRangeTwo.Value && touchpadAngle.Value <= endRangeTwo.Value)
			{
				if(!requireTrigger.Value)
				{
					Fsm.Event(inRange);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(inRange);
					}
				}
				Debug.Log("Range Two " + touchpadAngle.Value);
				return;
				
			}
			
			// finally if not found, then send to out of range.
			else
			{
				if(!requireTrigger.Value)
				{
					Fsm.Event(outOfRange);
				}
				
				if(requireTrigger.Value)
				{
					if(theScript.touchpadPressed)
					{
						Fsm.Event(outOfRange);
					}
				}
				
				Debug.Log("Out of Range " + touchpadAngle.Value);
				return;
				
			}
		}

	}
}