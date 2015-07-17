using System;
using System.Collections.Generic;
using System.Numerics;

namespace CasualGodComplex.Galaxies
{
    public class Grid
        : BaseGalaxySpec
    {
        private readonly float _size;
        private readonly float _spacing;

        public Grid(float size, float spacing)
        {
            _size = size;
            _spacing = spacing;
        }

        protected internal override IEnumerable<Star> Generate(Random random)
        {
            int count = (int)(_size / _spacing);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < count; k++)
                    {
                        yield return new Star(new Vector3(
                                i * _spacing,
                                j * _spacing,
                                k * _spacing
                            ),
                            StarName.Generate(random)
                        ).Offset(new Vector3(-_size / 2, -_size / 2, -_size / 2));
                    }
                }
            }
        }
    }
}
