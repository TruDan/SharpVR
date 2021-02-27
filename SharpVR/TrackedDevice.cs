using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using TruSaber.Utilities.Extensions;
using Valve.VR;

namespace SharpVR
{
    public abstract class TrackedDevice : ITrackedDevice
    {
        protected readonly VrContext Context;
        public             int       Index { get; }

        public bool IsConnected => Context.System.IsTrackedDeviceConnected((uint) Index);

        private Vector3    _position;
        private Vector3    _scale;
        private Quaternion _rotation;
        private Vector3    _nextPosition;
        private Vector3    _nextScale;
        private Quaternion _nextRotation;

        public void GetRelativePosition(ref Vector3    position) => position = _position;
        public void GetRelativeRotation(ref Quaternion rotation) => rotation = _rotation;
        public void GetRelativeScale(ref    Vector3    scale) => scale = _scale;
        
        public Vector3    LocalPosition => _position;
        public Quaternion LocalRotation => _rotation;
        public Vector3    LocalScale    => _scale;

        public Vector3    NextLocalPosition => _nextPosition;
        public Quaternion NextLocalRotation => _nextRotation;
        public Vector3    NextLocalScale    => _nextScale;

        
        private TrackedDevicePose_t Pose
        {
            get => Context.ValidDevicePoses[Index];
        }

        private TrackedDevicePose_t NextPose
        {
            get => Context.ValidNextDevicePoses[Index];
        }

        internal TrackedDevice(VrContext context, int index)
        {
            Context = context;
            Index = index;
        }

        public Matrix GetPose()
        {
            return Pose.mDeviceToAbsoluteTracking;
        }

        public Matrix GetNextPose()
        {
            return NextPose.mDeviceToAbsoluteTracking;
        }

        public Vector3 GetVelocity()
        {
            return Pose.vVelocity;
        }

        public Vector3 GetAngularVelocity()
        {
            return Pose.vAngularVelocity;
        }

        public virtual void Update()
        {
            if (Pose.bPoseIsValid)
            {
                var transform = GetPose();
                transform = Matrix.Invert(transform);
                if (transform.Decompose(out var scale, out var rotation, out var position))
                {
                    _scale = scale;
                    _rotation = rotation;
                    _position = position;
                }
            }
            
            if (NextPose.bPoseIsValid)
            {
                var transform = GetNextPose();
                transform = Matrix.Invert(transform);
                if (transform.Decompose(out var scale, out var rotation, out var position))
                {
                    _nextScale = scale;
                    _nextRotation = rotation;
                    _nextPosition = position;
                }
            }
        }

        internal static TrackedDevice Create(VrContext context, int index)
        {
            var cls = context.System.GetTrackedDeviceClass((uint) index);
            switch (cls)
            {
                case ETrackedDeviceClass.Invalid:
                    return null;
                case ETrackedDeviceClass.HMD:
                    return new HeadMountedDisplay(context, index);
                case ETrackedDeviceClass.Controller:
                    return new VRController(context, index);
                case ETrackedDeviceClass.GenericTracker:
                    return new GenericTracker(context, index);
                case ETrackedDeviceClass.TrackingReference:
                    return new TrackingReference(context, index);
                case ETrackedDeviceClass.DisplayRedirect:
                    return new DisplayRedirect(context, index);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected Exception CreateException(ETrackedPropertyError error)
        {
            var str = Context.System.GetPropErrorNameFromEnum(error);
            return new SharpVRException("Get property error: " + str, (int) error);
        }
    }
}