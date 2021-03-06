// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions

#if VRTK_VERSION_3_2_0_OR_NEWER

{
	[ActionCategory("VRTKController")]
	[Tooltip("Get UI Click pressed event for VRTK.")]

	public class  GetUiClickPressed : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK_UIPointer))]    
		public FsmOwnerDefault gameObject;

		[Tooltip("This will be true if the button aliased to the UI click is held down.")]
		public FsmBool uiClickPressed;

		public FsmBool everyFrame;

		VRTK_UIPointer theScript;

		public override void Reset()
		{

			uiClickPressed = false;
			gameObject = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			theScript = go.GetComponent<VRTK_UIPointer>();

			MakeItSo();

			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				MakeItSo();
			}
		}


		void MakeItSo()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			uiClickPressed.Value = theScript.IsSelectionButtonPressed();

		}

	}
}

#else

{
[ActionCategory("VRTKController")]
[Tooltip("Get UI Click pressed event for VRTK.")]

public class  GetUiClickPressed : FsmStateAction

{
[RequiredField]
[CheckForComponent(typeof(VRTK.VRTK_ControllerEvents))]    
public FsmOwnerDefault gameObject;

[Tooltip("This will be true if the button aliased to the UI click is held down.")]
public FsmBool uiClickPressed;

public FsmBool everyFrame;

VRTK.VRTK_ControllerEvents theScript;

public override void Reset()
{

uiClickPressed = false;
gameObject = null;
everyFrame = false;
}

public override void OnEnter()
{
var go = Fsm.GetOwnerDefaultTarget(gameObject);

theScript = go.GetComponent<VRTK.VRTK_ControllerEvents>();

if (!everyFrame.Value)
{
MakeItSo();
Finish();
}

}

public override void OnUpdate()
{
if (everyFrame.Value)
{
MakeItSo();
}
}


void MakeItSo()
{
var go = Fsm.GetOwnerDefaultTarget(gameObject);
if (go == null)
{
return;
}

uiClickPressed.Value = theScript.uiClickPressed;

}

}
}

#endif