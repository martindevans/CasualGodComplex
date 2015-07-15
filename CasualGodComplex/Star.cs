
using System.Numerics;

namespace CasualGodComplex
{
    public class Star
    {
        public Vector3 Position { get; internal set; }

        public float Size { get; private set; }
        public string Name { get; private set; }

        public Vector3 Color { get; private set; }

        public Star(float x, float y, float z, string name, Vector3 color)
        {
            Name = name;
            Position = new Vector3(x, y, z);
            Color = color;
        }

        public Star(Vector3 position, string name, Vector3 color)
        {
            Name = name;
            Position = position;
            Color = color;
        }
    }
}
