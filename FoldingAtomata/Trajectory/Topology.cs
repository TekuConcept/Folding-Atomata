using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.NTrajectory
{
    public class Topology
    {
        public Topology(List<Atom> atoms, List<Pair<int, int>> bonds)
        {
            _atoms = atoms;
            _bonds = bonds;
        }
        public List<Atom> GetAtoms()
        {
            return _atoms;
        }
        public List<Pair<int, int>> GetBonds()
        {
            return _bonds;
        }

        List<Atom> _atoms = new List<Atom>();
        List<Pair<int, int>> _bonds = new List<Pair<int,int>>();
    }
}
