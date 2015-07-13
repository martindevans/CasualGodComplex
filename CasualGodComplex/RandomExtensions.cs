using System;

namespace CasualGodComplex
{
    static class RandomExtensions
    {
        /// <summary>
        /// Generates a single value from a normal distribution
        /// </summary>
        /// <param name="random">A (uniform) random number generator</param>
        /// <param name="standardDeviation">The standard deviation of the distribution</param>
        /// <param name="mean">The mean of the distribution</param>
        /// <returns>A normally distributed value</returns>
        public static float NormallyDistributedSingle(this Random random, float standardDeviation, float mean)
        {
            double u1 = random.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = random.NextDouble();

            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)

            return (float)(randStdNormal * standardDeviation + mean);
        }
    }
}
