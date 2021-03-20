using System;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Valve.VR;

namespace SharpVR
{
    public interface IVrContext : IDisposable
    {
        HeadMountedDisplay Hmd             { get; }
        IVRController         LeftController  { get; }
        IVRController         RightController { get; }

        /// <summary>
        /// Indicates if OpenVR is initialized.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Attempt to initialize OpenVR.
        /// </summary>
        /// <exception cref="SharpVRException">If initialization fails.</exception>
        void Initialize(ILoggerFactory loggerFactory);

        void InitializeEmulation(int viewportWidth, int viewportHeight, float viewportFieldOfView);
        
        void Shutdown();
        void Update();

        /// <summary>
        /// Provides the game with the minimum size that it should use for its
        /// offscreen render target to minimize pixel stretching. This size is
        /// matched with the projection matrix and distortion function and will
        /// change from display to display depending on resolution, distortion,
        /// and field of view.
        /// </summary>
        void GetRenderTargetSize(out int width, out int height);

        /// <summary>
        /// Returns the projection matrix of the given eye.
        /// </summary>
        /// <param name="eye">Eye to get the projection matrix for.</param>
        /// <param name="zNear">Near plane distance.</param>
        /// <param name="zFar">Far plane distance.</param>
        /// <param name="matrix">Projection matrix; note that this is column major.</param>
        Matrix GetProjectionMatrix(Eye eye, float zNear, float zFar);

        /// <summary>
        /// Returns the view matrix transform between the view space and eye space. Eye space is the
        /// per-eye flavor of view space that provides stereo disparity. Instead of
        /// Model * View * Projection the model is Model * View * Eye * Projection. Normally
        /// View and Eye will be multiplied together and treated as View in your application.
        /// </summary>
        /// <param name="eye">Eye to get the matrix for.</param>
        /// <param name="matrix">Eye transform matrix.</param>
        /// <remarks>
        /// Matrix incorporates the user's interpupillary distance (IPD).
        /// </remarks>
        Matrix GetEyeMatrix(Eye eye);

        void ResetSeatedZeroPose();
        void Dispose();
        void Submit(Eye eye, ref Texture_t vrTexture, ref VRTextureBounds_t vrTextureBounds, EVRSubmitFlags submitDefault);
    }
}