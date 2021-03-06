// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Interaction")]
	[Tooltip("Gets touched object information.")]

	public class  getTocuhedObject : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK_InteractTouch))]    
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Output")]
		
		[Tooltip("Get the currently touched object.")]
		public FsmGameObject touchedObject;
		[Tooltip("Object is being touched.")]
		public FsmBool isTouching;
		[Tooltip("Object is an interactable object according to VRTK.")]
		public FsmBool isInteractable;


		[ActionSection("Options")]
		public FsmBool everyFrame;

		VRTK_InteractTouch touch;
	

		public override void Reset()
		{

			gameObject = null;
			everyFrame = false;
			isTouching = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			touch = go.GetComponent<VRTK_InteractTouch>();

			doTouch();
			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				doTouch();
			}
		}


		void doTouch()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			touchedObject.Value = touch.GetTouchedObject();
			if(touchedObject.Value == null)
			{
				isTouching.Value = false;
				isInteractable.Value = false;
			}
			
			else
			{
				isTouching.Value = true;
				isInteractable.Value = touch.IsObjectInteractable(touchedObject.Value);

			}

		}

	}
} 