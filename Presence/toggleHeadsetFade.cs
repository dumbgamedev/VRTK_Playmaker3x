// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRTK Locomotion")]
	[Tooltip("Manually trigger head to fade or unfade.")]

	public class  toggleHeadsetFade : FsmStateAction

	{
		[RequiredField]
		[CheckForComponent(typeof(VRTK.VRTK_HeadsetFade))]    
		[Title("Headset Gameobject")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Start fade or unfade.")]
		public FsmBool startFade;
		
		[ActionSection("Options")]
		
		[RequiredField]
		public FsmColor fadeColor;
		
		[RequiredField]
		public FsmFloat fadeDuration;

		public FsmBool everyFrame;

		VRTK.VRTK_HeadsetFade headset;

		public override void Reset()
		{
			startFade = true;
			gameObject = null;
			everyFrame = false;
			fadeDuration = 1f;
			fadeColor = Color.black;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			headset = go.GetComponent<VRTK.VRTK_HeadsetFade>();

			doFade();
			
			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				doFade();
			}
		}


		void doFade()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			if(startFade.Value)
			{
				headset.Fade(fadeColor.Value, fadeDuration.Value);
			}
			
			if(!startFade.Value)
			{
				headset.Unfade(fadeDuration.Value);
			}
		}
	}
}
