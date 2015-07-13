
namespace CasualGodComplex
{
    public class Planet
    {
        public string Name { get; private set; }

        public float OrbitalRadius { get; private set; }

        public Planet(string name, float orbitalRadius, float planetaryRadius)
        {
            OrbitalRadius = orbitalRadius;
            Name = name;
        }
    }
}
