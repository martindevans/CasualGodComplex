using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CasualGodComplex
{
    public class Galaxy
    {
        private readonly IEnumerable<Star> _stars;
        public IEnumerable<Star> Stars
        {
            get
            {
                return _stars;
            }
        }

        public Galaxy(IEnumerable<Star> stars)
        {
            _stars = stars;
        }

        public string To3JS()
        {
            StringBuilder b = new StringBuilder();
            b.AppendLine("var stars = [");
            foreach (var star in _stars)
            {
                var c = StarColor.ConvertTemperature(star.Temperature);
                b.AppendLine(string.Format("  {{name:\"{0}\",x:{1},y:{2},z:{3},r:{4},g:{5},b:{6}}},",
                    star.Name,
                    star.Position.X, star.Position.Y, star.Position.Z,
                    c.X, c.Y, c.Z
                ));
            }
            b.AppendLine("];");

            b.AppendLine();

            var n = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CasualGodComplex.ThreeJs.js"))
            using (StreamReader reader = new StreamReader(stream))
            {
                b.Append(reader.ReadToEnd());
            }

            return b.ToString();
        }

        public static async Task<Galaxy> Generate(BaseGalaxySpec spec, Random random)
        {
            var s = await Task.Factory.StartNew(() => spec.Generate(random));

            return new Galaxy(s);
        }
    }
}
