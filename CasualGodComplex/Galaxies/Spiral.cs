
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CasualGodComplex.Galaxies
{
    public class Spiral
        : BaseGalaxySpec
    {
        private const float SIZE = 1000;
        private const float SPACING = 55;

        public Spiral()
        {
            
        }

        protected internal override IEnumerable<Star> Generate(Random random)
        {
            int count = (int)(SIZE / SPACING);

            for (int i = 0; i < 100; i++)
            {
                var center = new Vector3(random.NormallyDistributedSingle(0.2f * SIZE, 0), random.NormallyDistributedSingle(0.0125f * SIZE, 0), random.NormallyDistributedSingle(0.2f * SIZE, 0));
                var stars = new Cluster(random.Next((int)(SIZE / 50), (int)(SIZE / 15)), densityMean: 0.00025f, deviationX: 1, deviationY: 1, deviationZ: 1).Generate(random)
                    .Select(s => s
                        .Offset(center)
                        .Swirl(new Vector3(0, 1, 0), 12.25f)
                    );

                foreach (var star in stars)
                {
                    yield return star;
                }
            }
        }
    }
}
