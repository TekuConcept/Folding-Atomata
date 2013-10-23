using System;

namespace FoldingAtomata.Modeling.NMesh
{
    public class Quad
    {
        uint a, b, c, d;

        public Quad(uint aa = 0, uint bb = 0, uint cc = 0, uint dd = 0)
        {
            a = aa;
            b = bb;
            c = cc;
            d = dd;
        }
    }
}
