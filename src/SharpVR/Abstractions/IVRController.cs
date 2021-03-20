using System;
using Microsoft.Xna.Framework;
using Valve.VR;

namespace SharpVR
{
    public interface IVRController : ITrackedDevice
    {
        Hand Hand { get; }

        VRControllerState State { get; }

        void GetAxis(EVRButtonId      buttonId, ref Vector2 axis);
        bool GetPress(EVRButtonId     buttonId);
        bool GetPressDown(EVRButtonId buttonId);
        bool GetPressUp(EVRButtonId   buttonId);
        bool GetTouch(EVRButtonId     buttonId);
        bool GetTouchDown(EVRButtonId buttonId);
        bool GetTouchUp(EVRButtonId   buttonId);
        Ray GetPointer();
        Ray GetNextPointer();
        void Vibrate();
        void Vibrate(TimeSpan duration);
    }
}