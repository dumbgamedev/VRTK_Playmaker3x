// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;
using VRTK.Controllables.PhysicsBased;
using VRTK.UnityEventHelper;
using VRTK.Controllables;
using VRTK.Examples;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Interaction")]
	[Tooltip("Get the angle of the physics lever with VRTK Physics Rotator script.")]

	public class  GetPhysicsRotatorAngle : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK_PhysicsRotator))]    
		public FsmOwnerDefault gameObject;

		[TitleAttribute("Lever Angle")]
		public FsmFloat angle;

		// Old Private
		//private	VRTK.UnityEventHelper.VRTK_Control_UnityEvents controlEvents;
		
		// New Private
		#pragma warning disable 0618
		protected VRTK_Control_UnityEvents controlEvents;
		#pragma warning restore 0618
		protected VRTK_BaseControllable controllableEvents;

		public override void Reset()
		{

			gameObject = null;
			angle = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			
			#pragma warning disable 0618
			if (go.GetComponent<VRTK_Control>() != null && go.GetComponent<VRTK_Control_UnityEvents>() == null)
			{
				controlEvents = go.AddComponent<VRTK_Control_UnityEvents>();
			}
			
			controlEvents = go.GetComponent<VRTK_Control_UnityEvents>();
		#pragma warning restore 0618
			controllableEvents = go.GetComponent<VRTK_BaseControllable>();
			
			ManageListeners(true);
			
		}
		
		public override void OnExit()
		{
			
			ManageListeners(false);
			
		}
		
		protected virtual void ManageListeners(bool state)
		{
			
			if (state)
			{
				if (controlEvents != null)
				{
					controlEvents.OnValueChanged.AddListener(HandleChange);
				}
				if (controllableEvents != null)
				{
					controllableEvents.ValueChanged += ValueChanged;
				}
			}
			else
			{
				if (controlEvents != null)
				{
					controlEvents.OnValueChanged.RemoveListener(HandleChange);
				}
				if (controllableEvents != null)
				{
					controllableEvents.ValueChanged -= ValueChanged;
				}
			}
		}
		
		protected virtual void ValueChanged(object sender, ControllableEventArgs e)
		{
			UpdateNum(e.value, (e.normalizedValue * 100f));
			
		}
		
		protected virtual void HandleChange(object sender, Control3DEventArgs e)
		{
			UpdateNum(e.value, (e.normalizedValue * 100f));
		}
		
		protected virtual void UpdateNum(float valueNum, float normalizedValueNum)
		{
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				//go.text = valueText;
				angle.Value = valueNum;
			}
		}
		
	}

}


