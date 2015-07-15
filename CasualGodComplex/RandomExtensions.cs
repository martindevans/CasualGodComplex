using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Generates a single value from a normal distribution, clamped to within min and max.
        /// </summary>
        /// <remarks>
        /// Based on low-error rational approximations, with help from A&S and http://www.johndcook.com/
        /// </remarks>
        /// <param name="random"> The seeded random to use for generation. </param>
        /// <param name="standardDeviation"> The standard deviation of the clamped normal distribution. </param>
        /// <param name="mean"> The mean value of the clamped normal distribution. </param>
        /// <param name="min"> The minimum value you wish to sample.  Does not bias to endpoints like naive clamping would. </param>
        /// <param name="max"> The maximum value you wish to sample.  Does not bias to endpoints like naive clamping would. </param>
        /// <returns> A normally distributed sample value, clamped to within min and max with no bias. </returns>
        public static float NormallyDistributedSingle(this Random random, float standardDeviation, float mean, float min, float max)
        {
            // Given a CDF function for any distribution
            // you can compute a truncated sample by choosing a uniformly distributed
            // *probability* p between CDF(min) and CDF(max)
            // and then use the inverse CDF function to transform back to your distribution.
            // TODO(pi): generalize this to take in an arbitrary CDF?
            var normalizedMin = (min - mean) / standardDeviation;
            var normalizedMax = (max - mean) / standardDeviation;
            
            var uniformSample = (float)random.NextDouble();
            var lowerProbability = NormalCDFApproximation(normalizedMin);
            var upperProbability = NormalCDFApproximation(normalizedMax);
            var probabilityRange = upperProbability - lowerProbability;
            var probability = lowerProbability + probabilityRange * uniformSample;
            return NormalInverseCDFApproximation(probability) * standardDeviation + mean;
        }
        
        /// <summary>
        /// A rational approximation for the CDF (Cumulative Distribution Function) for a normal
        /// distribution.
        /// </summary>
        /// <remarks>
        /// Absolute error below 2.5e-5
        /// </remarks>
        /// <param name="x">The value of a standard normal distribution.</param>
        /// <returns>The probability that a normal sample will fall below (or equal) x.</returns>
        private static float NormalCDFApproximation(float x)
        {
            // constants
            float[] a 			= {+0.254829592f, 
                                   -0.284496736f, 
                                   +1.421413741f, 
                                   -1.453152027f, 
                                   +1.061405429f};
                                   
            const float p       =  +0.3275911f;
            const float rootTwo =  +1.4142135623731f; // Same precisjion as Math.Sqrt(2.0);
            // Save the sign of x
            var sign = x < 0 ? -1 : 1;
            x = Math.Abs(x) / rootTwo;
                
            // A&S formula 7.1.26, page 299
            // http://people.math.sfu.ca/~cbm/aands/page_299.htm
            // and http://www.johndcook.com/blog/csharp_phi/
            var t = 1.0f / (1.0f + p*x);
            var y = 1.0f - (((((a[4]*t + a[3])*t) + a[2])*t + a[1])*t + a[0])*t * (float)Math.Exp(-x*x);
                
            return 0.5f * (1.0f + sign*y);
        }
        
        /// <summary>
        /// A rational approximation for the inverse CCDF (Complementary Cumulative Distribution Function)
        /// for a normal distribution.
        /// </summary>
        /// <remarks>
        /// Absolute error below 4.5e-4
        /// </remarks>
        /// <see>
        /// Taken from Abramowitz and Stegun, page 933, 26.2.23
        /// http://people.math.sfu.ca/~cbm/aands/page_933.htm
        /// </see>
        /// <param name="p">A probability, between 0 and 0.5</param>
        /// <returns>The value of x such that the probability that a normal sample being above x is p.</returns>
        /// <exception cref="ArgumentOutOfRangeException">p must be within 0.0 and 0.5</exception>
        private static float NormalInverseCCDFApproximation(float p)
        {
            Debug.Assert(p > 0f && p <= 0.5f);
            
            float[] c = {2.515517f, 0.802853f, 0.010328f};
            float[] d = {1.432788f, 0.189269f, 0.001308f};
            var t = (float)Math.Sqrt(-2.0f*Math.Log(p));
            // Compute, using Horners method
            return t - ((c[2]*t + c[1])*t + c[0]) /
            //      -------------------------------------
                    (((d[2]*t + d[1])*t + d[0])*t + 1.0f);
        }
        
        /// <summary>
        /// Computes the inverse of the Normal Distributions CDF function
        /// </summary>
        /// <remarks>
        /// Based on a rational approximation, absolute error should be under 4.5e-4
        /// <remarks>
        /// <param name="p"> A probability </param>
        /// <returns> The value of x such that the probability that a normal sample being below x is p. </returns> 
        /// <exception cref="ArgumentOutOfRangeException">p must be within 0.0 and 1.0</exception>
        private static float NormalInverseCDFApproximation(float p)
        {
            if (p <= 0.0f || p > 1.0f)
            {
                string msg = String.Format("Invalid input argument, p: {0}.  Must be between 0.0 and 1.0.", p);
                throw new ArgumentOutOfRangeException(msg);
            }
            // If p <= 0.5, then CDFInverse(p) = -CCDFInverse(p)
            // (by symmetric 
            // (The probability of being greater than x is the same as the probability of being less than -x)
            // If p > 0.5, then CDFInverse(p) = CCDFInverse(1 - p)
            // (By definition of CCDF)
            return (p <= 0.5f ?
                    -NormalInverseCCDFApproximation(p) :
                    NormalInverseCCDFApproximation(1.0f - p));
        }
    }
}
