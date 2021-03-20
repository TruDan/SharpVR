using System;
using Microsoft.Xna.Framework;
using Valve.VR;

namespace SharpVR
{
    public class EmulatedController : TrackedDevice, IVRController
    {
        private VRControllerState _currentState;
        private VRControllerState _lastState;
        public  Hand              Hand { get; }
        public VRControllerState State
        {
            get => _currentState;
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
            var p         = GetPose();
            var nearPoint = Vector3.Transform(Vector3.Zero, p);
            var farPoint  = Vector3.Transform(Vector3.Up, p);
            var direction = farPoint - nearPoint;

            //var direction = Vector3.Transform(Vector3.Up, World);
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public Ray GetNextPointer()
        {
            var p         = GetNextPose();
            var nearPoint = Vector3.Transform(Vector3.Zero, p);
            var farPoint  = Vector3.Transform(Vector3.Up, p);
            var direction = farPoint - nearPoint;

            //var direction = Vector3.Transform(Vector3.Up, World);
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public void Vibrate()
        {
        }

        public void Vibrate(TimeSpan duration)
        {
        }

        public EmulatedController(VrContext context, int index, Hand hand) : base(context, index)
        {
            Hand = hand;
            _currentState = new VRControllerState();
            _lastState = new VRControllerState();
        }

        public void GetAxis(EVRButtonId buttonId, ref Vector2 result)
        {
            result.X = 0;
            result.Y = 0;
        }
    }
}