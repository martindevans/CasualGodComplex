
namespace CasualGodComplex
{
    public class Star
    {
        public float X { get; internal set; }
        public float Y { get; internal set; }
        public float Z { get; internal set; }

        public float Size { get; private set; }
        public string Name { get; private set; }

        public Star(float size, float x, float y, float z, string name)
        {
            Size = size;
            Name = name;
            Z = z;
            Y = y;
            X = x;
        }
    }
}
