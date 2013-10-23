using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.NTrajectory
{
    public class Trajectory
    {
        public Trajectory(Topology topology)
        {
            _topology = topology;
        }
        public Topology GetTopology()
        {
            return _topology;
        }
        public void AddSnapshot(Snapshot newSnapshot)
        {
            _snapshots.Add(newSnapshot);
        }
        public Snapshot GetSnapshot(int index)
        {
            return _snapshots[index];
        }
        public int CountSnapshots()
        {
            return _snapshots.Count;
        }

        Topology _topology;
        List<Snapshot> _snapshots = new List<Snapshot>();
    }
}
