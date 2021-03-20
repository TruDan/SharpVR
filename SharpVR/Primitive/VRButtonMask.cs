using Valve.VR;

namespace SharpVR
{
    public class VRButtonMask
    {
        public const ulong System          = (1ul << (int) EVRButtonId.System); // reserved
        public const ulong ApplicationMenu = (1ul << (int) EVRButtonId.ApplicationMenu);
        public const ulong Grip            = (1ul << (int) EVRButtonId.Grip);
        public const ulong Axis0           = (1ul << (int) EVRButtonId.Axis0);
        public const ulong Axis1           = (1ul << (int) EVRButtonId.Axis1);
        public const ulong Axis2           = (1ul << (int) EVRButtonId.Axis2);
        public const ulong Axis3           = (1ul << (int) EVRButtonId.Axis3);
        public const ulong Axis4           = (1ul << (int) EVRButtonId.Axis4);
        public const ulong Touchpad        = (1ul << (int) EVRButtonId.SteamVRTouchpad);
        public const ulong Trigger         = (1ul << (int) EVRButtonId.SteamVRTrigger);
    }
}