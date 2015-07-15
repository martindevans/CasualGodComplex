using System;
using System.Collections.Generic;
using System.Globalization;

namespace CasualGodComplex.Galaxies
{
    public class Sphere
        : BaseGalaxySpec
    {
        private readonly float _size;

        private readonly float _densityMean;
        private readonly float _densityDeviation;

        private readonly float _deviationX;
        private readonly float _deviationY;
        private readonly float _deviationZ;

        public Sphere(
            float size,
            float densityMean = 0.0000025f, float densityDeviation = 0.000001f,
            float deviationX = 0.0000025f,
            float deviationY = 0.0000025f,
            float deviationZ = 0.0000025f
        )
        {
            _size = size;

            _densityMean = densityMean;
            _densityDeviation = densityDeviation;

            _deviationX = deviationX;
            _deviationY = deviationY;
            _deviationZ = deviationZ;
        }

        protected internal override IEnumerable<Star> Generate(Random random)
        {
            var density = Math.Max(0, random.NormallyDistributedSingle(_densityDeviation, _densityMean));
            var countMax = Math.Max(0, (int)(_size * _size * _size * density));
            if (countMax <= 0)
                yield break;

            var count = random.Next(countMax);

            for (int i = 0; i < count; i++)
                yield return new Star(1, random.NormallyDistributedSingle(_deviationX * _size, 0), random.NormallyDistributedSingle(_deviationY * _size, 0), random.NormallyDistributedSingle(_deviationZ * _size, 0), i.ToString(CultureInfo.InvariantCulture));
        }
    }
}
