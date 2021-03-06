// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using VRTK;

namespace HutongGames.PlayMaker.Actions


{
	[ActionCategory("VRTKController")]
	[Tooltip("Get device game object for right controller, left controller or headset.")]

	public class  getDeviceGameobject : FsmStateAction

	{
		[ObjectType(typeof(VRTK.VRTK_DeviceFinder.Devices))]
		public FsmEnum device;

		[UIHint(UIHint.Variable)]
		public FsmGameObject deviceObject;

		public FsmBool everyFrame;

		public override void Reset()
		{

			everyFrame = false;
			deviceObject = null;
			device = VRTK.VRTK_DeviceFinder.Devices.Headset;

		}

		public override void OnEnter()
		{

			findDevice();

			if (!everyFrame.Value)
			{
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				findDevice();
			}
		}


		void findDevice()
		{

			Transform _transform = VRTK_DeviceFinder.DeviceTransform((VRTK.VRTK_DeviceFinder.Devices)device.Value);
			deviceObject.Value = _transform.gameObject;

		}

	}
}

