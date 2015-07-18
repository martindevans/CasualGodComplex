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
        public IEnumerable<Star> Stars { get; private set; }

        private Galaxy(IEnumerable<Star> stars)
        {
            Stars = stars;
        }

        

        public static async Task<Galaxy> Generate(BaseGalaxySpec spec, Random random)
        {
            var s = await Task.Factory.StartNew(() => spec.Generate(random));

            return new Galaxy(s);
        }
    }
}
