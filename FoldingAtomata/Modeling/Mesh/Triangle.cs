using System;

namespace FoldingAtomata.Modeling.NMesh
{
    public struct Triangle
    {
        uint a, b, c;
        public Triangle(uint aa = 0, uint bb = 0, uint cc = 0)
        {
            a = aa;
            b = bb;
            c = cc;
        }
    }
}
