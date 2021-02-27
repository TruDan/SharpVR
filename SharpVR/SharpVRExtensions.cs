using Microsoft.Xna.Framework;
using Valve.VR;

namespace SharpVR
{
    public static class SharpVrExtensions
    {
        public static Matrix ToMg(this HmdMatrix34_t mat)
        {
            var m = new Matrix(
                mat.M11, mat.M21, mat.M31, 0.0f,
                mat.M12, mat.M22, mat.M32, 0.0f,
                mat.M13, mat.M23, mat.M33, 0.0f,
                mat.M14, mat.M24, mat.M34, 1.0f);
            // var m = new Matrix(
            //     mat.m0, mat.m1, mat.m2, mat.m3,
            //     mat.m4, mat.m5, mat.m6, mat.m7,
            //     mat.m8, mat.m9, mat.m10, mat.m11,
            //     0.0f, 0.0f, 0.0f, 1.0f);

            return Matrix.Invert(m);
            return m;
        }

        public static Matrix ToMg(this HmdMatrix44_t mat)
        {
            var m = new Matrix(
                mat.M11, mat.M21, mat.M31, mat.M41,
                mat.M12, mat.M22, mat.M32, mat.M42,
                mat.M13, mat.M23, mat.M33, mat.M43,
                mat.M14, mat.M24, mat.M34, mat.M44);
            
            // var m = new Matrix(
            //     mat.m0, mat.m1, mat.m2, mat.m3,
            //     mat.m4, mat.m5, mat.m6, mat.m7,
            //     mat.m8, mat.m9, mat.m10, mat.m11,
            //     mat.m12, mat.m13, mat.m14, mat.m15);

            return m;
        }

        public static Vector3 ToMg(this HmdVector3_t vec)
        {
            var m = new Vector3(vec.X, vec.Y, vec.Z);
            return m;
        }

    }
}
