
using System.Numerics;

namespace CasualGodComplex
{
    public class Star
    {
        public Vector3 Position { get; internal set; }

        public float Size { get; private set; }
        public string Name { get; private set; }

        public Star(float size, float x, float y, float z, string name)
        {
            Size = size;
            Name = name;
            Position = new Vector3(x, y, z);
        }

        public Star(float size, Vector3 position, string name)
        {
            Size = size;
            Name = name;
            Position = position;
        }
    }
}
