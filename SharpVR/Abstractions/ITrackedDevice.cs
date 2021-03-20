using Microsoft.Xna.Framework;

namespace SharpVR
{
    public interface ITrackedDevice
    {
        int        Index             { get; }
        bool       IsConnected       { get; }
        Vector3    LocalPosition     { get; }
        Quaternion LocalRotation     { get; }
        Vector3    LocalScale        { get; }
        Vector3    NextLocalPosition { get; }
        Quaternion NextLocalRotation { get; }
        Vector3    NextLocalScale    { get; }
        void GetRelativePosition(ref Vector3    position);
        void GetRelativeRotation(ref Quaternion rotation);
        void GetRelativeScale(ref    Vector3    scale);
        Matrix GetPose();
        Matrix GetNextPose();
        Vector3 GetVelocity();
        Vector3 GetAngularVelocity();
        void Update();
    }
}