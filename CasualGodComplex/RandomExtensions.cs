using System;
using System.Collections.Generic;

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
            // *****************************************************
            // Intentionally duplicated to avoid IEnumerable overhead
            // *****************************************************
            var u1 = random.NextDouble(); //these are uniform(0,1) random doubles
            var u2 = random.NextDouble();
            
            var x1 = Math.Sqrt(-2.0 * Math.Log(u1));
            var x2 = 2.0 * Math.PI * u2;
            var z1 = x1 * Math.Sin(x2); //random normal(0,1)
            return (float)(z1 * standardDeviation + mean);
        }

        /// <summary>
        /// Generates a sequence of normal values clamped within min and max
        /// </summary>
        /// <remarks>
        /// Originally used inverse phi method, but this method, found here:
        /// http://arxiv.org/pdf/0907.4010.pdf
        /// is significantly faster
        /// </remarks>
        /// <param name="random">A (uniform) random number generator</param>
        /// <param name="standardDeviation">The standard deviation of the distribution</param>
        /// <param name="mean">The mean of the distribution</param>
        /// <param name="min">The minimum allowed value (does not bias)</param>
        /// <param name="max">The max allowed value (does not bias)</param>
        /// <returns>A sequence of samples from a normal distribution, clamped to within min and max in an unbiased manner.</returns>
        public static IEnumerable<float> NormallyDistributedSingles(this Random random, float standardDeviation, float mean, float min, float max)
        {
            // sharing computation doesn't save us much, it's all lost to IEnumerable overhead
            while (true)
                yield return random.NormallyDistributedSingle(standardDeviation, mean, min, max);
        }

        /// <summary>
        /// Generates a single normal value clamped within min and max
        /// </summary>
        /// <remarks>
        /// Originally used inverse phi method, but this method, found here:
        /// http://arxiv.org/pdf/0907.4010.pdf
        /// is significantly faster
        /// </remarks>
        /// <param name="random">A (uniform) random number generator</param>
        /// <param name="standardDeviation">The standard deviation of the distribution</param>
        /// <param name="mean">The mean of the distribution</param>
        /// <param name="min">The minimum allowed value (does not bias)</param>
        /// <param name="max">The max allowed value (does not bias)</param>
        /// <returns>A single sample from a normal distribution, clamped to within min and max in an unbiased manner.</returns>
        public static float NormallyDistributedSingle(this Random random, float standardDeviation, float mean, float min, float max)
        {
            var nMax = (max - mean) / standardDeviation;
            var nMin = (min - mean) / standardDeviation;
            var nRange = nMax - nMin;
            var nMaxSq = nMax * nMax;
            var nMinSq = nMin * nMin;
            var subFrom = nMinSq;
            if (nMin < 0 && 0 < nMax) subFrom = 0;
            else if (nMax < 0) subFrom = nMaxSq;

            var sigma = 0.0;
            double u;
            float z;
            do
            {
                z = nRange * (float)random.NextDouble() + nMin; // uniform[normMin, normMax]
                sigma = Math.Exp((subFrom - z * z) / 2);
                u = random.NextDouble();
            } while (u > sigma);

            return z * standardDeviation + mean;
        }
    }
}
