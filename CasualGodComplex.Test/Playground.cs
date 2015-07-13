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
        public async Task RandomGalaxy()
        {
            var g = await Galaxy.Generate(new Cluster(1000), new Random(150));
            Console.WriteLine(g.To3JS());
        }
    }
}
