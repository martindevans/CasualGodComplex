
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CasualGodComplex.Galaxies
{
    public class Cluster
        : BaseGalaxySpec
    {
        private readonly float _size;
        private readonly bool _subClusters;

        private readonly float _densityMean;
        private readonly float _densityDeviation;

        private readonly float _deviationX;
        private readonly float _deviationY;
        private readonly float _deviationZ;

        public Cluster(float size, bool subClusters = true,
            float densityMean = 0.0000025f, float densityDeviation = 0.000001f,
            float deviationX = 0.0000025f,
            float deviationY = 0.0000025f,
            float deviationZ = 0.0000025f
        )
        {
            _size = size;
            _subClusters = subClusters;

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
            {
                var dev = _size / 8;

                yield return new Star(1, random.NormallyDistributedSingle(_deviationX * _size, 0), random.NormallyDistributedSingle(_deviationY * _size, 0), random.NormallyDistributedSingle(_deviationZ * _size, 0), i.ToString(CultureInfo.InvariantCulture));
            }

            if (_subClusters)
            {
                var clusterDensity = Math.Max(0, random.NormallyDistributedSingle(_densityDeviation / 10, _densityMean / 10));
                var clusterCount = random.Next((int)(_size * _size * _size * clusterDensity));

                for (int i = 0; i < clusterCount; i++)
                {
                    var dev = _size / 8;

                    var x = random.NormallyDistributedSingle(dev, 0);
                    var y = random.NormallyDistributedSingle(dev, 0);
                    var z = random.NormallyDistributedSingle(dev, 0);

                    var clusterSize = random.NormallyDistributedSingle(_size / 10, _size / 5);

                    foreach (var star in new Cluster(clusterSize, false).Generate(random))
                        yield return star.Offset(x, y, z);
                }
            }
        }
    }
}
