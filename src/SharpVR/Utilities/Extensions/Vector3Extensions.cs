using Microsoft.Xna.Framework;
using Valve.VR;

namespace SharpVR
{
    public static class Vector3Extensions 
    {
        
        public static Vector3 ToMg(this HmdVector3_t vec)
        {
            var m = new Vector3(vec.X, vec.Y, vec.Z);
            return m;
        }

    }
}