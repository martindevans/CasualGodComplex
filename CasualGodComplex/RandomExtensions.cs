using System;
using System.Collections.Generic;
using System.Linq;

namespace CasualGodComplex
{
    static class RandomExtensions
    {
        /// <summary>
        /// Generates a sequence of values from a normal distribution, using Box-Muller
        /// https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
        /// </summary>
        /// <param name="random">A (uniform) random number generator</param>
        /// <param name="standardDeviation">The standard deviation of the distribution</param>
        /// <param name="mean">The mean of the distribution</param>
        /// <returns>A normally distributed value</returns>
        public static IEnumerable<float> NormallyDistributedSingles(this Random random, float standardDeviation, float mean)
        {
            while(true)
            {
                var u1 = random.NextDouble(); //these are uniform(0,1) random doubles
                var u2 = random.NextDouble();
                
                var x1 = Math.Sqrt(-2.0 * Math.Log(u1));
                var x2 = 2.0 * Math.PI * u2;
                var z1 = x1 * Math.Sin(x2); //random normal(0,1)
                yield return (float)(z1 * standardDeviation + mean);
                var z2 = x1 * Math.Cos(x2);
                yield return (float)(z2 * standardDeviation + mean);
            }
        }
        
        /// <summary>
        /// Generates a single value from a normal distribution, using Box-Muller
        /// https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
        /// </summary>
        /// <param name="random">A (uniform) random number generator</param>
        /// <param name="standardDeviation">The standard deviation of the distribution</param>
        /// <param name="mean">The mean of the distribution</param>
        /// <returns>A normally distributed value</returns>
        public static float NormallyDistributedSingle(this Random random, float standardDeviation, float mean)
        {
            return random.NormallyDistributedSingles(standardDeviation, mean).First();
        }

        public static float NormallyDistributedSingle(this Random random, float standardDeviation, float mean, float min, float max)
        {
            float v;
            do
            {
                v = NormallyDistributedSingle(random, standardDeviation, mean);
            } while (v < min || v > max);

            return v;
        }
    }
}
