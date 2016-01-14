
using System;
using System.Collections.Generic;
using System.Numerics;

namespace CasualGodComplex.Galaxies
{
    public class Spiral
        : BaseGalaxySpec
    {
        /// <summary>
        /// Approximate physical size of the galaxy
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Approximate spacing between clusters
        /// </summary>
        public int Spacing { get; set; }

        /// <summary>
        /// Minimum number of arms
        /// </summary>
        public int MinimumArms { get; set; }

        /// <summary>
        /// Maximum number of arms
        /// </summary>
        public int MaximumArms { get; set; }

        public float ClusterCountDeviation { get; set; }
        public float ClusterCenterDeviation { get; set; }

        public float MinArmClusterScale { get; set; }
        public float ArmClusterScaleDeviation { get; set; }
        public float MaxArmClusterScale { get; set; }

        public float Swirl { get; set; }

        public float CenterClusterScale { get; set; }
        public float CenterClusterDensityMean { get; set; }
        public float CenterClusterDensityDeviation { get; set; }
        public float CenterClusterSizeDeviation { get; set; }

        public float CenterClusterPositionDeviation { get; set; }
        public float CenterClusterCountDeviation { get; set; }
        public float CenterClusterCountMean { get; set; }

        public float CentralVoidSizeMean { get; set; }
        public float CentralVoidSizeDeviation { get; set; }

        public Spiral()
        {
            Size = 750;
            Spacing = 5;

            MinimumArms = 3;
            MaximumArms = 7;

            ClusterCountDeviation = 0.35f;
            ClusterCenterDeviation = 0.2f;

            MinArmClusterScale = 0.02f;
            ArmClusterScaleDeviation = 0.02f;
            MaxArmClusterScale = 0.1f;

            Swirl = (float)Math.PI * 4;

            CenterClusterScale = 0.19f;
            CenterClusterDensityMean = 0.00005f;
            CenterClusterDensityDeviation = 0.000005f;
            CenterClusterSizeDeviation = 0.00125f;

            CenterClusterCountMean = 20;
            CenterClusterCountDeviation = 3;
            CenterClusterPositionDeviation = 0.075f;

            CentralVoidSizeMean = 25;
            CentralVoidSizeDeviation = 7;
        }

        protected internal override IEnumerable<Star> Generate(Random random)
        {
            var centralVoidSize = random.NormallyDistributedSingle(CentralVoidSizeDeviation, CentralVoidSizeMean);
            if (centralVoidSize < 0)
                centralVoidSize = 0;
            var centralVoidSizeSqr = centralVoidSize * centralVoidSize;

            foreach (var star in GenerateArms(random))
                if (star.Position.LengthSquared() > centralVoidSizeSqr)
                    yield return star;

            foreach (var star in GenerateCenter(random))
                if (star.Position.LengthSquared() > centralVoidSizeSqr)
                    yield return star;

            foreach (var star in GenerateBackgroundStars(random))
                if (star.Position.LengthSquared() > centralVoidSizeSqr)
                    yield return star;
        }

        private IEnumerable<Star> GenerateBackgroundStars(Random random)
        {
            return new Sphere(Size, 0.000001f, 0.0000001f, 0.35f, 0.125f, 0.35f).Generate(random);
        }

        private IEnumerable<Star> GenerateCenter(Random random)
        {
            //Add a single central cluster
            var sphere = new Sphere(
                size: Size * CenterClusterScale,
                densityMean: CenterClusterDensityMean,
                densityDeviation: CenterClusterDensityDeviation,
                deviationX: CenterClusterScale,
                deviationY: CenterClusterScale,
                deviationZ: CenterClusterScale
            );

            var cluster = new Cluster(sphere,
                CenterClusterCountMean, CenterClusterCountDeviation, Size * CenterClusterPositionDeviation, Size * CenterClusterPositionDeviation, Size * CenterClusterPositionDeviation
            );

            foreach (var star in cluster.Generate(random))
                yield return star.Swirl(Vector3.UnitY, Swirl * 5);
        }

        private IEnumerable<Star> GenerateArms(Random random)
        {
            int arms = random.Next(MinimumArms, MaximumArms);
            float armAngle = (float) ((Math.PI * 2) / arms);

            int maxClusters = (Size / Spacing) / arms;
            for (int arm = 0; arm < arms; arm++)
            {
                int clusters = (int) Math.Round(random.NormallyDistributedSingle(maxClusters * ClusterCountDeviation, maxClusters));
                for (int i = 0; i < clusters; i++)
                {
                    //Angle from center of this arm
                    float angle = random.NormallyDistributedSingle(0.5f * armAngle * ClusterCenterDeviation, 0) + armAngle * arm;

                    //Distance along this arm
                    float dist = Math.Abs(random.NormallyDistributedSingle(Size * 0.4f, 0));

                    //Center of the cluster
                    var center = Vector3.Transform(new Vector3(0, 0, dist), Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), angle));

                    //Size of the cluster
                    var clsScaleDev = ArmClusterScaleDeviation * Size;
                    var clsScaleMin = MinArmClusterScale * Size;
                    var clsScaleMax = MaxArmClusterScale * Size;
                    var cSize = random.NormallyDistributedSingle(clsScaleDev, clsScaleMin * 0.5f + clsScaleMax * 0.5f, clsScaleMin, clsScaleMax);

                    var stars = new Sphere(cSize, densityMean: 0.00025f, deviationX: 1, deviationY: 1, deviationZ: 1).Generate(random);
                    foreach (var star in stars)
                        yield return star.Offset(center).Swirl(Vector3.UnitY, Swirl);
                }
            }
        }
    }
}
