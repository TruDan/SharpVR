using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Valve.VR;

namespace SharpVR
{
    public class VRController : TrackedDevice, IVRController
    {
        public Hand Hand { get; }

        public VRControllerState State
        {
            get => _currentState;
        }

        private TrackedDevicePose_t _currentPose;

        private VRControllerState _lastState;
        private VRControllerState _currentState;

        public Quaternion ControllerOffset { get; set; }
        public bool ButtonPressed(EVRButtonId button)
        {
            return (_lastState.ulButtonPressed & (1ul << (int) button)) == 0 &&
                   (_currentState.ulButtonPressed & (1ul << (int) button)) != 0;
        }

        public override void Update()
        {
            _lastState = _currentState;
            _currentPose = Context.ValidDevicePoses[Index];
            Context.System.GetControllerState((uint) Index, ref _currentState,
                (uint) Marshal.SizeOf<VRControllerState>());
            //Context.System.GetControllerStateWithPose(ETrackingUniverseOrigin.TrackingUniverseStanding, (uint) Index, ref _currentState, (uint) Marshal.SizeOf<VRControllerState_t>(), ref _currentPose);
            //throw new SharpVRException("Getting controller state failed");
            base.Update();
        }


        public void GetAxis(EVRButtonId buttonId, ref Vector2 result)
        {
            var axisId = (uint) buttonId - (uint) EVRButtonId.Axis0;
            switch (axisId)
            {
                case 0:
                    result.X = _currentState.rAxis0.x;
                    result.Y = _currentState.rAxis0.y;
                    break;

                case 1:
                    result.X = _currentState.rAxis1.x;
                    result.Y = _currentState.rAxis1.y;
                    break;
                case 2:
                    result.X = _currentState.rAxis2.x;
                    result.Y = _currentState.rAxis2.y;
                    break;
                case 3:
                    result.X = _currentState.rAxis3.x;
                    result.Y = _currentState.rAxis3.y;
                    break;

                case 4:
                    result.X = _currentState.rAxis4.x;
                    result.Y = _currentState.rAxis4.y;
                    break;

                default:
                    result.X = 0;
                    result.Y = 0;
                    break;
            }
        }

        public bool GetPress(ulong buttonMask) => (_currentState.ulButtonPressed & buttonMask) != 0;

        public bool GetPressDown(ulong buttonMask) => (_currentState.ulButtonPressed & buttonMask) != 0 &&
                                                      (_lastState.ulButtonPressed & buttonMask) == 0;

        public bool GetPressUp(ulong buttonMask) => (_currentState.ulButtonPressed & buttonMask) == 0 &&
                                                    (_lastState.ulButtonPressed & buttonMask) != 0;

        public bool GetPress(EVRButtonId     buttonId) => GetPress(1ul << (int) buttonId);
        public bool GetPressDown(EVRButtonId buttonId) => GetPressDown(1ul << (int) buttonId);
        public bool GetPressUp(EVRButtonId   buttonId) => GetPressUp(1ul << (int) buttonId);

        public bool GetTouch(ulong buttonMask) => (_currentState.ulButtonTouched & buttonMask) != 0;

        public bool GetTouchDown(ulong buttonMask) => (_currentState.ulButtonTouched & buttonMask) != 0 &&
                                                      (_lastState.ulButtonTouched & buttonMask) == 0;

        public bool GetTouchUp(ulong buttonMask) => (_currentState.ulButtonTouched & buttonMask) == 0 &&
                                                    (_lastState.ulButtonTouched & buttonMask) != 0;

        public bool GetTouch(EVRButtonId     buttonId) => GetTouch(1ul << (int) buttonId);
        public bool GetTouchDown(EVRButtonId buttonId) => GetTouchDown(1ul << (int) buttonId);
        public bool GetTouchUp(EVRButtonId   buttonId) => GetTouchUp(1ul << (int) buttonId);

        public Ray GetPointer()
        {
            var r         = LocalRotation; //Quaternion.Multiply(LocalRotation, ControllerOffset);
            var p         = Matrix.Identity * Matrix.CreateFromQuaternion(r) * Matrix.CreateTranslation(LocalPosition);
            var nearPoint = Vector3.Transform(Vector3.Zero, p);
            var farPoint  = Vector3.Transform(Vector3.Forward, p);
            var direction = farPoint - nearPoint;

            //var direction = Vector3.Transform(Vector3.Up, World);
            direction.Normalize();

            return new Ray(LocalPosition, direction);
        }

        public Ray GetNextPointer()
        {
            var r = NextLocalRotation; //Quaternion.Multiply(LocalRotation, ControllerOffset);
            var p = GetNextPose();//Matrix.Identity * Matrix.CreateFromQuaternion(r) * Matrix.CreateTranslation(LocalPosition);
            var nearPoint = Vector3.Transform(Vector3.Zero, p);
            var farPoint  = Vector3.Transform(Vector3.Forward, p);
            var direction = farPoint - nearPoint;

            //var direction = Vector3.Transform(Vector3.Up, World);
            direction.Normalize();

            return new Ray(NextLocalPosition, direction);
        }

        public void Vibrate()
        {
            Vibrate(TimeSpan.FromSeconds(0.255f));
        }
        
        public void Vibrate(TimeSpan duration)
        {
            Context.System.TriggerHapticPulse((uint)Index, 0, (ushort)(duration.TotalMilliseconds * 1000));
        }


        internal VRController(VrContext context, int index) : base(context, index)
        {
            var role = context.System.GetControllerRoleForTrackedDeviceIndex((uint) index);
            switch (role)
            {
                case ETrackedControllerRole.LeftHand:
                    Hand = Hand.Left;
                    break;
                case ETrackedControllerRole.RightHand:
                    Hand = Hand.Right;
                    break;
                default:
                    Hand = Hand.None;
                    break;
            }
            ControllerOffset = Quaternion.CreateFromYawPitchRoll(0f, -((float)Math.PI/2f), 0f);
        }
    }
}