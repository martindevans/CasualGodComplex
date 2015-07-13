using System;
using System.Collections.Generic;

namespace CasualGodComplex
{
    public abstract class BaseGalaxySpec
    {
        protected internal abstract IEnumerable<Star> Generate(Random random);
    }
}
