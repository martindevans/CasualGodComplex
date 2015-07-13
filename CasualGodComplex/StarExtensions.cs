
namespace CasualGodComplex
{
    static class StarExtensions
    {
        public static Star Offset(this Star s, float x, float y, float z)
        {
            s.X += x;
            s.Y += y;
            s.Z += z;

            return s;
        }
    }
}
