using FoldingAtomata.NTrajectory;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XNA;

namespace FoldingAtomata.PyON
{
    public class TrajectoryParser
    {
        public TrajectoryParser(string pyon, bool removeSpaces = true)
        {
            _pyon = pyon;

            if(removeSpaces) _pyon = _pyon.Replace(" ", "");
        }
        public Trajectory Parse(bool filter = true)
        {
            Console.WriteLine("Parsing trajectory PyON... ");
            Console.Clear();
            var top = ParseTopology();
            Trajectory trajectory = new Trajectory(top);

            ParsePositions(trajectory);

            Console.WriteLine("... done parsing trajectory.");

            return trajectory;
        }

        private Topology ParseTopology()
        {
            string BEGIN = "PyON1topology\n{\n", END = "\n}\n---";
            var FULL = new Pair<int, int>(0, _pyon.Length);
            var TOPOLOGY_SPAN = StringManip.Between(_pyon, FULL, BEGIN, END);

            var atoms = ParseAtoms(TOPOLOGY_SPAN);
            var bonds = ParseBonds(TOPOLOGY_SPAN);
            return new Topology(atoms, bonds);
        }
        private List<Atom> ParseAtoms(Pair<int, int> topologySpan)
        {
            string BEGIN = "\"atoms\":[\n", END = "]\n],\n";
            var SPAN = StringManip.Between(_pyon, topologySpan, BEGIN, END);

            List<Atom> atoms = new List<Atom>();
            int lineStart = SPAN.First;
            while (lineStart < SPAN.Second)
            {
                int lineEnd = _pyon.IndexOf('\n', lineStart);
                atoms.Add(ParseAtom(new Pair<int, int>(lineStart, lineEnd)));
                lineStart = lineEnd + 1;
            }

            return atoms;
        }
        private Atom ParseAtom(Pair<int, int> atomSpan)
        {
            var range = StringManip.Between(_pyon, atomSpan, "[", "]");
            var isolated = _pyon.Substring(range.First, range.Second - range.First);
            var tokens = StringManip.ExplodeAndTrim(isolated, ',', " \"");

            int number;
            float charge, radius, mass;

            number = int.Parse(tokens[4]);
            charge = float.Parse(tokens[3]);
            radius = float.Parse(tokens[2]);
            mass = float.Parse(tokens[1]);

            return new Atom(tokens[0], number, charge, radius, mass);
        }
        private List<Pair<int, int>> ParseBonds(Pair<int, int> topologySpan)
        {
            string BEGIN = "\"bonds\":[\n", END = "]\n";
            var bondDataRange = StringManip.Between(_pyon, topologySpan, BEGIN, END);

            List<Pair<int, int>> bonds = new List<Pair<int,int>>();
            int lineStart = bondDataRange.First;
            while (lineStart < bondDataRange.Second)
            {
                int lineEnd = _pyon.IndexOf("\n", lineStart);
                var bond = ParseBond(new Pair<int, int>(lineStart, lineEnd));
                bonds.Add(new Pair<int, int>(bond.First, bond.Second));
                lineStart = lineEnd + 1;
            }

            return bonds;
        }
        private Pair<int, int> ParseBond(Pair<int, int> bondline)
        {
            var range = StringManip.Between(_pyon, bondline, "[", "]");
            var isolated = _pyon.Substring(range.First, range.Second - range.First);
            var tokens = StringManip.ExplodeAndTrim(isolated, ',', " ");

            int atomIndexA, atomIndexB;
            atomIndexA = int.Parse(tokens[0]);
            atomIndexB = int.Parse(tokens[1]);

            return new Pair<int, int>(atomIndexA, atomIndexB);
        }
        private void ParsePositions(Trajectory trajectory)
        {
            Console.WriteLine("Parsing all snapshots... ");
            Console.Clear();

            string BEGIN = "PyON1positions\n[", END = "]\n---";
            int index = _pyon.IndexOf(BEGIN, 0);
            while (index != -1)
            {
                var range = new Pair<int, int>(index, _pyon.Length);
                var snapshotRange = StringManip.Between(_pyon, range, BEGIN, END);
                var snap = ParseSnapshot(snapshotRange, trajectory.GetTopology());
                trajectory.AddSnapshot(snap);
                index = _pyon.IndexOf(BEGIN, index + 1);
            }

            Console.WriteLine("... done parsing snapshots.");
            Console.Clear();
        }
        private Snapshot ParseSnapshot(Pair<int, int> snapshotRange, Topology topology)
        {
            Snapshot snapshot = new Snapshot();
            Console.Write("Parsing snapshot... ");

            int index = _pyon.IndexOf("[\n", snapshotRange.First);
            String snap = _pyon.Substring(index, snapshotRange.Second - snapshotRange.First);
            MatchCollection tokens = Regex.Matches(snap, @"(?<=\[)(([^\[]*?))(?=\])");

            char[] split = new char[]{','};
            foreach (Match token in tokens)
            {
                String[] lines = token.Value.Replace("\r", "").Replace("\n", "").Split(split, StringSplitOptions.RemoveEmptyEntries);
                snapshot.AddPosition(new Vector3(float.Parse(lines[0]), float.Parse(lines[1]), float.Parse(lines[2])));
            }

            /*
            int count = 0;
            while (index < snapshotRange.Second)
            {
                float x = float.Parse(_pyon.Substring(index + 2));//(float)atof(_pyon + index + 2);
                index = _pyon.IndexOf(",\n", index) + 2;
                float y = float.Parse(_pyon.Substring(index));//(float)atof(_pyon + index);
                index = _pyon.IndexOf(",\n", index) + 2;
                float z = float.Parse(_pyon.Substring(index));//(float)atof(_pyon + index);
                index = _pyon.IndexOf("[\n", index);

                snapshot.AddPosition(new Vector3(x, y, z));
                count++;
            }*/

            Console.WriteLine("done.");
            return snapshot;
        }

        string _pyon;
    }
}
