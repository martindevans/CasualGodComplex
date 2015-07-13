using System;
using System.Collections.Generic;
using System.Globalization;

namespace CasualGodComplex.Galaxies
{
    public class Cube
        : BaseGalaxySpec
    {
        private readonly float _size;

        public Cube(float size)
        {
            _size = size;
        }

        protected internal override IEnumerable<Star> Generate(Random random)
        {
            var density = Math.Max(0, random.NormallyDistributedSingle(0.000001f, 0.0000125f));
            Console.WriteLine("//Density: " + density);
            var count = random.Next((int)(_size * _size * _size * density));

            for (int i = 0; i < count; i++)
            {
                yield return new Star(1, (float) (random.NextDouble() - 0.5) * _size, (float) (random.NextDouble() - 0.5) * _size, (float) (random.NextDouble() - 0.5) * _size, i.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
