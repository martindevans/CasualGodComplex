
using System;
using System.Collections.Generic;
using System.Numerics;

namespace CasualGodComplex.Galaxies
{
    public class Cluster
        : BaseGalaxySpec
    {
        private readonly BaseGalaxySpec _basis;

        private readonly float _countMean;
        private readonly float _countDeviation;

        private readonly float _deviationX;
        private readonly float _deviationY;
        private readonly float _deviationZ;

        public Cluster(BaseGalaxySpec basis,
            float countMean = 0.0000025f, float countDeviation = 0.000001f,
            float deviationX = 0.0000025f, float deviationY = 0.0000025f, float deviationZ = 0.0000025f
        )
        {
            _basis = basis;
            _countMean = countMean;
            _countDeviation = countDeviation;

            _deviationX = deviationX;
            _deviationY = deviationY;
            _deviationZ = deviationZ;
        }

        protected internal override IEnumerable<Star> Generate(Random random)
        {
            var count = Math.Max(0, random.NormallyDistributedSingle(_countDeviation, _countMean));
            if (count <= 0)
                yield break;

            for (int i = 0; i < count; i++)
            {
                Vector3 center = new Vector3(
                    random.NormallyDistributedSingle(_deviationX, 0),
                    random.NormallyDistributedSingle(_deviationY, 0),
                    random.NormallyDistributedSingle(_deviationZ, 0)
                );

                foreach (var star in _basis.Generate(random))
                    yield return star.Offset(center);
            }
        }
    }
}
