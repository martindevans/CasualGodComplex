using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CasualGodComplex
{
    static class StarColor
    {
        /// <summary>
        /// http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/
        /// </summary>
        /// <param name="colorTemperature"></param>
        /// <returns></returns>
        public static Vector3 ConvertTemperature(float colorTemperature)
        {
            colorTemperature = Math.Min(40000, Math.Max(1000, colorTemperature));

            float red, green, blue;

            if (colorTemperature <= 6600)
                red = 255;
            else
            {
                var x = colorTemperature / 100 - 55;
                red = 351.97690f + (0.11420645f * x) + -40.2536630f * (float)Math.Log(x);
            }

            if (colorTemperature <= 6600)
            {
                var x = colorTemperature / 100 - 2;
                green = -155.254855f + (-0.445969f * x) + 104.492161f * (float)Math.Log(x);
            }
            else
            {
                var x = colorTemperature / 100 - 50;
                green = 325.44941f + (0.0794345f * x) + -28.085296f * (float)Math.Log(x);
            }

            if (colorTemperature < 2000)
                blue = 0;
            else if (colorTemperature > 6600)
                blue = 255;
            else
            {
                var x = colorTemperature / 100 - 10;
                blue = -254.76935f + 0.8274096f * x + 115.67994f * (float)Math.Log(x);
            }

            return new Vector3(
                Math.Min(255, Math.Max(0, red)) / 255f,
                Math.Min(255, Math.Max(0, green)) / 255f,
                Math.Min(255, Math.Max(0, blue)) / 255f
            );
        }

        public static Vector3 GenerateStarColor(Random random)
        {
            var temp = random.NormallyDistributedSingle(7000, 6000, 1000, 40000);

            return ConvertTemperature(temp);
        }
    }
}
