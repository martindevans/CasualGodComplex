using System;
using System.Numerics;
using Litipk.ColorSharp.LightSpectrums;

namespace CasualGodComplex
{
    public static class StarColor
    {
        public static Vector3 ConvertTemperature(float colorTemperature)
        {
            var srgb = new BlackBodySpectrum(colorTemperature).ToSRGB();
            return new Vector3((float)srgb.R, (float)srgb.G, (float)srgb.B);
        }

        public static Vector3 GenerateStarColor(Random random)
        {
            var temp = random.NormallyDistributedSingle(7000, 6000, 1000, 40000);

            return ConvertTemperature(temp);
        }
    }
}
