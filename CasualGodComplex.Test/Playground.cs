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
    }
}
