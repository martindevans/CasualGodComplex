using System;
using System.Numerics;

namespace CasualGodComplex
{
    static class StarExtensions
    {
        public static Star Offset(this Star s, Vector3 offset)
        {
            s.Position += offset;

            return s;
        }

        public static Star Scale(this Star s, Vector3 scale)
        {
            s.Position *= scale;

            return s;
        }

        public static Star Swirl(this Star s, Vector3 axis, float amount)
        {
            var d = s.Position.Length();

            var a = (float)Math.Pow(d, 0.1f) * amount;
            s.Position = Vector3.Transform(s.Position, Quaternion.CreateFromAxisAngle(axis, a));

            return s;
        }
    }
}
