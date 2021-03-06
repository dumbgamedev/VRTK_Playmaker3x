// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTKController")]
	[Tooltip("Get vector two from touchpad and check within a range.")]

	public class  GetTouchpadAxisBool : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK.VRTK_ControllerEvents))]    
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Y Axis")]
		
		public FsmFloat startYAxis;
		public FsmFloat endYAxis;
		
		[ActionSection("X Axis")]
		
		public FsmFloat startXAxis;
		public FsmFloat endXAxis;
		
		[ActionSection("Options")]
		
		public FsmBool requirePressed;
		[Tooltip("Exclude the absolute center at x & y 0,0, to avoid triggering the bool when in a default position.")]
		public FsmBool excludeAbsoluteCenter;
		public FsmBool everyFrame;
		
		[ActionSection("Output")]
		
		[UIHint(UIHint.Variable)]
		public FsmVector2 touchpadAxis;
		
		[Tooltip("Bool set to true when touched within the set range")]
		[UIHint(UIHint.Variable)]
		public FsmBool areaTouched;

		private float y;
		private float x;
		
		VRTK.VRTK_ControllerEvents theScript;

		public override void Reset()
		{

			touchpadAxis = null;
			gameObject = null;
			everyFrame = true;
			startXAxis = 0;
			startYAxis = 0;
			endXAxis = 0;
			endYAxis = 0;
			areaTouched = false;
			excludeAbsoluteCenter = true;
			requirePressed = true;
			
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			theScript = go.GetComponent<VRTK.VRTK_ControllerEvents>();

			if(excludeAbsoluteCenter.Value)
			{
				CheckExcludeCenter();
			}
			
			if(!excludeAbsoluteCenter.Value)
			{
				CheckIncludeCenter();
			}
			
			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				if(excludeAbsoluteCenter.Value)
				{
					CheckExcludeCenter();
				}
				
				if(!excludeAbsoluteCenter.Value)
				{
					CheckIncludeCenter();
				}			}
		}


		void CheckExcludeCenter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			touchpadAxis.Value = theScript.GetTouchpadAxis();
			x = touchpadAxis.Value.x;
			y = touchpadAxis.Value.y;
			

				if (y >= startYAxis.Value && y <= endYAxis.Value)
				{
					if (x >= startXAxis.Value && x <= endXAxis.Value)
					{
						if(y != 0 && x != 0)
						{
							
							if(!requirePressed.Value)
							{
								areaTouched.Value = true;
							}
							
							if(requirePressed.Value)
							{
								if(theScript.touchpadPressed)
								{
									areaTouched.Value = true;
								}
								
								else
								{
									areaTouched.Value = false;
								}
							}

						}
						
						else
						{
							areaTouched.Value = false;
						}
					}
					
					else
					{
						areaTouched.Value = false;
					}
					
				}
			
				else
				{
					areaTouched.Value = false;
				}
		}
		
		void CheckIncludeCenter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			touchpadAxis.Value = theScript.GetTouchpadAxis();
			x = touchpadAxis.Value.x;
			y = touchpadAxis.Value.y;
			

				if (y >= startYAxis.Value && y <= endYAxis.Value)
				{
					if (x >= startXAxis.Value && x <= endXAxis.Value)
					{
						if(!requirePressed.Value)
						{
							areaTouched.Value = true;
						}
						
						if(requirePressed.Value)
						{
							if(theScript.touchpadPressed)
							{
								areaTouched.Value = true;
							}
							
							else
							{
								areaTouched.Value = false;
							}
						}
					
					}
					
					else
					{
						areaTouched.Value = false;
					}
					
				}
			
				else
				{
					areaTouched.Value = false;
				}
		}
	}
}