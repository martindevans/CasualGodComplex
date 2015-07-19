using System;
using System.Threading.Tasks;
using CasualGodComplex.Galaxies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CasualGodComplex.Test
{
    [TestClass]
    public class Playground
    {
        [TestMethod]
        public async Task Spiral()
        {
            var g = await Galaxy.Generate(new Spiral(), new Random());
            Console.WriteLine(g.To3JS());
        }

        [TestMethod]
        public void Name()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(StarName.Generate(r));
            }
        }

        [TestMethod]
        public void UniqueNames()
        {
            var names = String.Join(",\n", StarName.Generate(new Random(), 1000));
        }
    }
}
